import { useState, useEffect, useMemo } from 'react';
import RadarGraph from "../RadarGraph";
import { obtenerComparacionPaquetes } from "../../services/reporteService";
import "../../views/Reports.css";

/**
 * @typedef {Object} ReporteComparacionPaquetes
 * @property {string} nombrePaquete - Nombre del paquete turístico.
 * @property {number} totalItinerarios - Cantidad total de itinerarios del paquete.
 * @property {number} totalParadas - Cantidad total de paradas registradas.
 * @property {Object<string, number>} distribucionCategorias - Conteo por categoría.
 * @property {Object<string, number>} distribucionEsfuerzo - Conteo por esfuerzo ("Bajo", "Medio", "Alto").
 */

/**
 * Obtiene un conteo numérico desde un diccionario sin importar mayúsculas/minúsculas.
 *
 * @param {Object<string, number> | null | undefined} dict
 * @param {string} clave
 * @returns {number}
 */
function leerValor(dict, clave) {
  if (!dict || typeof dict !== 'object') return 0;
  const encontrada = Object.entries(dict).find(
    ([k]) => k.trim().toLowerCase() === clave.toLowerCase()
  );
  return encontrada && typeof encontrada[1] === 'number' ? encontrada[1] : 0;
}

/**
 * Devuelve la categoría con mayor cantidad dentro de distribucionCategorias.
 *
 * @param {Object<string, number> | null | undefined} dict
 * @returns {string}
 */
function obtenerCategoriaPrincipal(dict) {
  if (!dict || typeof dict !== 'object') return "N/A";
  const entradas = Object.entries(dict).filter(([, v]) => typeof v === 'number' && v > 0);
  if (entradas.length === 0) return "N/A";
  entradas.sort((a, b) => b[1] - a[1]);
  return entradas[0][0];
}

/**
 * Determina el nivel de esfuerzo predominante ("Bajo", "Medio" o "Alto").
 *
 * @param {Object<string, number> | null | undefined} distribucionEsfuerzo
 * @returns {"Bajo" | "Medio" | "Alto"}
 */
function obtenerEsfuerzoPrincipal(distribucionEsfuerzo) {
  const bajo = leerValor(distribucionEsfuerzo, "Bajo");
  const medio = leerValor(distribucionEsfuerzo, "Medio");
  const alto = leerValor(distribucionEsfuerzo, "Alto");

  if (alto >= medio && alto >= bajo && alto > 0) return "Alto";
  if (medio >= bajo && medio > 0) return "Medio";
  return "Bajo";
}

/**
 * Componente del reporte RF-05: comparación entre paquetes turísticos.
 *
 * @returns {JSX.Element}
 */
function RF05() {
  /** @type {[ReporteComparacionPaquetes[], Function]} */
  const [packagesData, setPackagesData] = useState([]);

  /** @type {[boolean, Function]} */
  const [loading, setLoading] = useState(true);

  /** @type {[string | null, Function]} */
  const [error, setError] = useState(null);

  const radarKeys = [
    { dataKey: "Low", name: "Low Effort (Bajo)" },
    { dataKey: "Medium", name: "Medium Effort (Medio)" },
    { dataKey: "High", name: "High Effort (Alto)" },
  ];

  const radarColors = ["#15803d", "#f8ec49", "#b91c1c"];

  const radarLeyenda = [
    "Each point on the graph represents a tour package.",
    "Larger areas toward the <strong>green</strong> indicate that the package has more low-effort activities.",
    "Areas toward <strong>yellow</strong> indicate moderate-intensity activities.",
    "Areas toward <strong>red</strong> indicate that the activity requires more physical effort.",
    "A more balanced chart means the route has a good variety of effort levels.",
  ];

  useEffect(() => {
    /**
     * Carga los datos de comparación de paquetes desde el servicio.
     * @async
     * @returns {Promise<void>}
     */
    const cargarPaquetes = async () => {
      try {
        const data = await obtenerComparacionPaquetes();
        setPackagesData(Array.isArray(data) ? data : []);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    cargarPaquetes();
  }, []);

  const radarData = useMemo(() => {
    return packagesData.map((pkg) => ({
      package: pkg.nombrePaquete,
      Low: leerValor(pkg.distribucionEsfuerzo, "Bajo"),
      Medium: leerValor(pkg.distribucionEsfuerzo, "Medio"),
      High: leerValor(pkg.distribucionEsfuerzo, "Alto"),
    }));
  }, [packagesData]);

  return (
    <div className="report-card">
      <h2>RF-05: Inter-package comparison</h2>
      <p style={{ color: "#666", marginBottom: "1rem" }}>
        Comparison of category balance and physical effort across different packages.
      </p>

      {loading && <p>Loading package comparison...</p>}
      {error && <p style={{ color: "#b91c1c" }}>Error: {error}</p>}

      {!loading && !error && (
        <>
          <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left" }}>
            <thead>
              <tr style={{ borderBottom: "2px solid #ddd" }}>
                <th style={{ padding: "10px" }}>ID</th>
                <th style={{ padding: "10px" }}>Package</th>
                <th style={{ padding: "10px" }}>Itineraries</th>
                <th style={{ padding: "10px" }}>Total Stops</th>
                <th style={{ padding: "10px" }}>Category</th>
                <th style={{ padding: "10px" }}>Effort</th>
              </tr>
            </thead>
            <tbody>
              {packagesData.length === 0 ? (
                <tr>
                  <td colSpan="6" style={{ padding: "10px", textAlign: "center" }}>
                    No package comparison records found.
                  </td>
                </tr>
              ) : (
                packagesData.map((row, index) => {
                  const effort = obtenerEsfuerzoPrincipal(row.distribucionEsfuerzo);
                  const category = obtenerCategoriaPrincipal(row.distribucionCategorias);
                  const badgeClass =
                    effort === "Alto"
                      ? "effort-high"
                      : effort === "Medio"
                      ? "effort-medium"
                      : "effort-low";

                  return (
                    <tr key={`${row.nombrePaquete}-${index}`} style={{ borderBottom: "1px solid #eee" }}>
                      <td style={{ padding: "10px" }}>{index + 1}</td>
                      <td style={{ padding: "10px" }}>{row.nombrePaquete}</td>
                      <td style={{ padding: "10px" }}>{row.totalItinerarios}</td>
                      <td style={{ padding: "10px" }}>{row.totalParadas}</td>
                      <td style={{ padding: "10px" }}>{category}</td>
                      <td style={{ padding: "10px" }}>
                        <span className={`effort-badge ${badgeClass}`}>
                          {effort}
                        </span>
                      </td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>

          {radarData.length > 0 && (
            <RadarGraph
              data={radarData}
              title="Package Balance: Physical Effort Profile"
              xAxisKey="package"
              keys={radarKeys}
              colors={radarColors}
              leyenda={radarLeyenda}
            />
          )}
        </>
      )}
    </div>
  );
}

export default RF05;