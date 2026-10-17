using Turist.Tests.Fakes;
using static Turist.Tests.Helpers.RouteSystemTestHelper;

namespace Turist.Tests.RouteSystemTests;

public class GetBestRatedRouteTests
{
    // ============================================================
    // Caso trivial y estructura básica
    // ============================================================

    [Fact]
    public void GetBestRatedRoute_ConUnSoloLugar_DevuelveEseLugarYLaHoraInicio()
    {
        var sistema = Sistema([Lugar(0)], []);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([0], Ids(ruta));
        Assert.Equal(Hour(9), ruta.StartTime);
    }

    [Fact]
    public void GetBestRatedRoute_LineaDeTresLugares_VisitaLosTresEnOrden()
    {
        // 0 → 1 → 2
        var sistema = Sistema(
            [Lugar(0), Lugar(1), Lugar(2)],
            [T(0, 1, 5), T(1, 2, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([0, 1, 2], Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_SinVecinos_DevuelveSoloElOrigen()
    {
        var sistema = Sistema([Lugar(0)], []);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Single(ruta.visitablesId);
        Assert.Equal(0, ruta.visitablesId[0].Id);
    }

    [Fact]
    public void GetBestRatedRoute_PreservaElStartTimeEnElResultado()
    {
        var sistema = Sistema([Lugar(0)], []);
        var start = Hour(7).AddMinutes(30);

        var ruta = sistema.GetBestRatedRoute(
            start, Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal(start, ruta.StartTime);
    }

    [Fact]
    public void GetBestRatedRoute_RutaResultanteSoloContieneVisitablesDelGrafo()
    {
        var sistema = Sistema(
            [Lugar(0), Lugar(1), Lugar(2)],
            [T(0, 1, 5), T(1, 2, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        var idsPermitidos = new HashSet<int> { 0, 1, 2 };
        Assert.All(ruta.visitablesId, v => Assert.Contains(v.Id, idsPermitidos));
    }

    // ============================================================
    // Optimización por suma de Rate
    // ============================================================

    [Fact]
    public void GetBestRatedRoute_DosRamasDeIgualLongitud_EligeLaDeMayorRate()
    {
        // 0 → 1 → 3 (Rate: 5, 10, 5) = 20
        // 0 → 2 → 3 (Rate: 5,  2, 5) = 12
        var sistema = Sistema(
            [
                Lugar(0, rate: 5),
                Lugar(1, rate: 10),
                Lugar(2, rate: 2),
                Lugar(3, rate: 5),
            ],
            [
                T(0, 1, 5), T(1, 3, 5),
                T(0, 2, 5), T(2, 3, 5),
            ]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([0, 1, 3], Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_DosRamasDistintaLongitud_PrefiereLaDeMayorRateTotal()
    {
        // 0 → 1 → 2 → 3 (Rates: 5, 10, 10, 5) = 30, 4 nodos
        // 0 → 4         (Rates: 5, 20)       = 25, 2 nodos
        var sistema = Sistema(
            [
                Lugar(0, rate: 5), Lugar(1, rate: 10), Lugar(2, rate: 10),
                Lugar(3, rate: 5), Lugar(4, rate: 20),
            ],
            [
                T(0, 1, 5), T(1, 2, 5), T(2, 3, 5),
                T(0, 4, 5),
            ]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([0, 1, 2, 3], Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_SumaFavoreceTourLargoBienValoradoSobreTourCortoExcelente()
    {
        // Camino A (5 nodos): 0→1→2→3→4, rates 5,4,4,4,4 → suma 21
        // Camino B (2 nodos): 0→5,             rates 5,10   → suma 15
        // Con SUMA debe elegir A. Con PROMEDIO elegiría B (4.2 vs 7.5).
        var sistema = Sistema(
            [
                Lugar(0, rate: 5),
                Lugar(1, rate: 4), Lugar(2, rate: 4),
                Lugar(3, rate: 4), Lugar(4, rate: 4),
                Lugar(5, rate: 10),
            ],
            [
                T(0, 1, 2), T(1, 2, 2), T(2, 3, 2), T(3, 4, 2),
                T(0, 5, 2),
            ]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([0, 1, 2, 3, 4], Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_ValoracionesNegativas_RestanAlTotal()
    {
        // 0→1→2: rates 5, 5, -20 → suma -10
        // 0→3:   rates 5, 5      → suma 10
        var sistema = Sistema(
            [
                Lugar(0, rate: 5),
                Lugar(1, rate: 5),
                Lugar(2, rate: -20),
                Lugar(3, rate: 5),
            ],
            [
                T(0, 1, 5), T(1, 2, 5),
                T(0, 3, 5),
            ]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.DoesNotContain(2, Ids(ruta));
        Assert.Contains(3, Ids(ruta));
    }

    // ============================================================
    // Presupuesto de costo (acumulativo, constante, no se puede pasar)
    // ============================================================

    [Fact]
    public void GetBestRatedRoute_CuandoUnaAristaExcedeElPresupuesto_LaEvita()
    {
        var sistema = Sistema(
            [Lugar(0), Lugar(1, rate: 10), Lugar(2, rate: 3)],
            [T(0, 1, 5, cost: 10), T(0, 2, 5, cost: 3)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 5, categoriesFilter: SinFiltro());

        Assert.Contains(2, Ids(ruta));
        Assert.DoesNotContain(1, Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_CuandoElPresupuestoEsCero_SoloVisitaElOrigen()
    {
        var sistema = Sistema(
            [Lugar(0), Lugar(1, rate: 10)],
            [T(0, 1, 5, cost: 1)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 0, categoriesFilter: SinFiltro());

        Assert.Equal([0], Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_AcumulaCostoDeVariasAristas()
    {
        // 0→1→2 cuesta 3+3=6. maxCost=5 → NO alcanza 2
        var sistema = Sistema(
            [Lugar(0), Lugar(1, rate: 10), Lugar(2, rate: 10)],
            [T(0, 1, 5, cost: 3), T(1, 2, 5, cost: 3)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 5, categoriesFilter: SinFiltro());

        Assert.DoesNotContain(2, Ids(ruta));
    }

    // ============================================================
    // Filtro de categorías (true = permitido)
    // ============================================================

    [Fact]
    public void GetBestRatedRoute_CategoriasVacias_PermiteTodas()
    {
        var sistema = Sistema(
            [Lugar(0, categoryId: 1), Lugar(1, categoryId: 2)],
            [T(0, 1, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100,
            categoriesFilter: new Dictionary<int, bool>());

        Assert.Equal([0, 1], Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_SoloVisitaCategoriasPermitidas()
    {
        var sistema = Sistema(
            [
                Lugar(0, categoryId: 1),
                Lugar(1, categoryId: 2, rate: 10),
                Lugar(2, categoryId: 1, rate: 5),
            ],
            [T(0, 1, 5), T(1, 2, 5), T(0, 2, 20)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100,
            categoriesFilter: new Dictionary<int, bool> { [1] = true, [2] = false });

        Assert.DoesNotContain(1, Ids(ruta));
        Assert.Contains(2, Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_CategoriaTrueSeIncluye()
    {
        var sistema = Sistema(
            [Lugar(0, categoryId: 1), Lugar(1, categoryId: 7)],
            [T(0, 1, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100,
            categoriesFilter: new Dictionary<int, bool> { [1] = true, [7] = true });

        Assert.Equal([0, 1], Ids(ruta));
    }

    // ============================================================
    // Lugares bloqueados
    // ============================================================

    [Fact]
    public void GetBestRatedRoute_CuandoUnLugarEstaBloqueado_NoLoVisita()
    {
        var sistema = Sistema(
            [Lugar(0), Lugar(1, rate: 10, blocked: true)],
            [T(0, 1, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.DoesNotContain(1, Ids(ruta));
    }

    // ============================================================
    // Ventanas temporales (stopDt y horarios)
    // ============================================================

    [Fact]
    public void GetBestRatedRoute_CuandoNoAlcanzaElTiempo_VisitaMenosLugares()
    {
        var schedule = new FakeSchedule { VisitTimeSpan = TimeSpan.FromMinutes(30) };
        var sistema = Sistema(
            [
                Lugar(0, schedule: schedule),
                Lugar(1, schedule: schedule),
                Lugar(2, schedule: schedule),
            ],
            [T(0, 1, 30), T(1, 2, 30)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(9).AddMinutes(40), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([0], Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_CuandoUnLugarEstaCerradoAlLlegar_NoLoVisita()
    {
        var cerrado = new FakeSchedule
        {
            OpensAt = Hour(10),
            ClosesAt = Hour(18),
            VisitTimeSpan = TimeSpan.FromMinutes(10),
        };
        var sistema = Sistema(
            [Lugar(0), Lugar(1, rate: 10, schedule: cerrado)],
            [T(0, 1, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.DoesNotContain(1, Ids(ruta));
    }

    [Fact]
    public void GetBestRatedRoute_CuandoElOrigenEstaCerrado_DevuelveRutaVacia()
    {
        var cerrado = new FakeSchedule
        {
            OpensAt = Hour(14),
            ClosesAt = Hour(18),
            VisitTimeSpan = TimeSpan.FromMinutes(10),
        };
        var sistema = Sistema(
            [Lugar(0, schedule: cerrado), Lugar(1)],
            [T(0, 1, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Empty(Ids(ruta));
    }

    // ============================================================
    // UpdateGraph: semántica de reemplazo por Id
    // ============================================================

    [Fact]
    public void UpdateGraph_ConIdExistente_ActualizaElVisitableAsociadoAlNodo()
    {
        var sistema = Sistema(
            [Lugar(1, rate: 5)],
            []);

        sistema.UpdateGraph(
            [Lugar(1, rate: 20)],
            []);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal(20, ruta.visitablesId[0].Rate);
    }

    [Fact]
    public void UpdateGraph_ConCategoriaCambiada_RespetaElNuevoValorEnFiltros()
    {
        var sistema = Sistema(
            [Lugar(1, categoryId: 2)],
            []);

        sistema.UpdateGraph(
            [Lugar(1, categoryId: 3)],
            []);

        var rutaConCat2 = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100,
            categoriesFilter: new Dictionary<int, bool> { [2] = true, [3] = false });
        Assert.Empty(Ids(rutaConCat2));

        var rutaConCat3 = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100,
            categoriesFilter: new Dictionary<int, bool> { [2] = false, [3] = true });
        Assert.Single(Ids(rutaConCat3));
    }

    [Fact]
    public void UpdateGraph_ConBloqueadoCambiado_RespetaElNuevoValor()
    {
        var sistema = Sistema(
            [Lugar(1), Lugar(2, rate: 10)],
            [T(1, 2, 5)]);

        var antes = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100, categoriesFilter: SinFiltro());
        Assert.Contains(2, Ids(antes));

        sistema.UpdateGraph(
            [Lugar(2, rate: 10, blocked: true)],
            []);

        var despues = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100, categoriesFilter: SinFiltro());
        Assert.DoesNotContain(2, Ids(despues));
    }

    [Fact]
    public void UpdateGraph_ConNuevoLugar_LoIncluyeEnConsultasPosteriores()
    {
        var sistema = Sistema(
            [Lugar(0), Lugar(1)],
            [T(0, 1, 5)]);

        var rutaAntes = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());
        Assert.Equal([0, 1], Ids(rutaAntes));

        sistema.UpdateGraph(
            [Lugar(2, rate: 20)],
            [T(1, 2, 5)]);

        var rutaDespues = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());
        Assert.Contains(2, Ids(rutaDespues));
    }

    [Fact]
    public void UpdateGraph_ConNuevaRedAlternativa_LaUsaSiEsMejor()
    {
        var sistema = Sistema(
            [Lugar(0), Lugar(1)],
            [T(0, 1, 5)]);

        var rutaAntes = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());
        Assert.DoesNotContain(2, Ids(rutaAntes));

        sistema.UpdateGraph(
            [Lugar(2, rate: 10), Lugar(3, rate: 10)],
            [T(0, 2, 3), T(2, 3, 3)]);

        var rutaDespues = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());
        Assert.Contains(2, Ids(rutaDespues));
        Assert.Contains(3, Ids(rutaDespues));
    }

    [Fact]
    public void UpdateGraph_ConCategoriaProhibida_NoLaVisitaDespuesDelUpdate()
    {
        var sistema = Sistema(
            [Lugar(0, categoryId: 1), Lugar(1, categoryId: 2)],
            [T(0, 1, 5)]);

        sistema.UpdateGraph(
            [Lugar(2, categoryId: 2, rate: 20)],
            [T(1, 2, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100,
            categoriesFilter: new Dictionary<int, bool> { [1] = true, [2] = false });

        Assert.DoesNotContain(1, Ids(ruta));
        Assert.DoesNotContain(2, Ids(ruta));
    }

    [Fact]
    public void UpdateGraph_SinCambios_NoAlteraElResultado()
    {
        var visitables = new[] { Lugar(0), Lugar(1), Lugar(2) };
        var transportes = new[] { T(0, 1, 5), T(1, 2, 5) };
        var sistema = Sistema(visitables, transportes);

        var antes = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        sistema.UpdateGraph(visitables, transportes);

        var despues = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 0, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal(Ids(antes), Ids(despues));
    }

    [Fact]
    public void UpdateGraph_ConVariosNodosYManteniendoLosExistentes_NoRompeElDiccionario()
    {
        var sistema = Sistema(
            [Lugar(1), Lugar(2), Lugar(3)],
            [T(1, 2, 5), T(2, 3, 5)]);

        sistema.UpdateGraph(
            [Lugar(1), Lugar(2), Lugar(3), Lugar(4), Lugar(5)],
            [T(1, 2, 5), T(2, 3, 5), T(3, 4, 5), T(4, 5, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([1, 2, 3, 4, 5], Ids(ruta));
    }

    [Fact]
    public void UpdateGraph_ConAristaHaciaNodoInexistente_NoLoVisita()
    {
        var sistema = Sistema(
            [Lugar(1)],
            [T(1, 99, 5)]);

        var ruta = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100, categoriesFilter: SinFiltro());

        Assert.Equal([1], Ids(ruta));
    }

    [Fact]
    public void UpdateGraph_NoModificaRutasGeneradasAnteriormente()
    {
        var sistema = Sistema(
            [Lugar(1, rate: 5)],
            []);

        var rutaAntes = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100, categoriesFilter: SinFiltro());

        sistema.UpdateGraph(
            [Lugar(1, rate: 20)],
            []);

        Assert.Equal(5, rutaAntes.visitablesId[0].Rate);

        var rutaDespues = sistema.GetBestRatedRoute(
            Hour(9), Hour(18), origin: 1, maxCost: 100, categoriesFilter: SinFiltro());
        Assert.Equal(20, rutaDespues.visitablesId[0].Rate);
    }
}