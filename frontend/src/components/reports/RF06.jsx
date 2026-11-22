import { useState, useEffect } from 'react';
import { ScatterChart, Scatter, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, ZAxis } from 'recharts';
import {
  obtenerComparacionModificaciones,
  obtenerAbandonoActividades,
  obtenerCorrelacionEsfuerzoValoracion,
} from '../../services/reporteService';
import "../../views/Reports.css";

/**
 * @typedef {Object} ReporteComparacionModificacion
 * @property {number} turistaID
 * @property {string} turistaNombre
 * @property {string} paqueteNombre
 * @property {number} promedioTuristaModificado
 * @property {number} promedioGrupo
 */

/**
 * @typedef {Object} ReporteActividadAbandono
 * @property {string} nombreActividad
 * @property {string} nombreGuia
 * @property {string} nombrePaquete
 * @property {number} tasaAbandono
 * @property {number} totalVisitas
 * @property {number} visitasAbandonadas
 */

/**
 * @typedef {Object} ReporteCorrelacionEsfuerzoValoracion
 * @property {string} nombrePaquete
 * @property {string} nivelEsfuerzo
 * @property {number} promedioValoracion
 * @property {number} totalValoraciones
 */

/**
 * Convierte el nivel de esfuerzo a un valor numérico para el eje X del gráfico de dispersión.
 * @param {string|number} nivel - Nivel de esfuerzo recibido desde el backend.
 * @returns {number} Valor numérico en escala 1 a 3 (o 0 si no coincide).
 */
function mapearEsfuerzoANumero(nivel) {
  if (typeof nivel === 'number') return nivel;
  const normalizado = String(nivel).trim().toLowerCase();
  if (normalizado === '1' || normalizado === 'bajo' || normalizado === 'low') return 1;
  if (normalizado === '2' || normalizado === 'medio' || normalizado === 'medium') return 2;
  if (normalizado === '3' || normalizado === 'alto' || normalizado === 'high') return 3;
  return Number(normalizado) || 0;
}

/**
 * Formatea la tasa de abandono para mostrarla como porcentaje.
 * @param {number} tasa - Valor de tasa de abandono devuelto por el backend.
 * @returns {string} Porcentaje formateado.
 */
function formatearTasaAbandono(tasa) {
  if (typeof tasa !== 'number' || Number.isNaN(tasa)) return '0%';
  const porcentaje = tasa > 0 && tasa <= 1 ? tasa * 100 : tasa;
  return `${Number(porcentaje.toFixed(1))}%`;
}

/**
 * Tooltip personalizado para los puntos del gráfico de correlación.
 * @param {Object} props - Propiedades inyectadas por Recharts Tooltip.
 * @returns {JSX.Element|null}
 */
function CorrelationTooltip({ active, payload }) {
  if (!active || !payload || payload.length === 0) return null;
  const item = payload[0].payload;
  return (
    <div style={{ background: "#fff", border: "1px solid #ddd", padding: "8px 12px", borderRadius: "6px" }}>
      <p style={{ margin: 0, fontWeight: "bold" }}>{item.nombrePaquete}</p>
      <p style={{ margin: "4px 0 0" }}>Esfuerzo: {item.nivelEsfuerzo}</p>
      <p style={{ margin: "2px 0 0" }}>Valoración promedio: {item.promedioValoracion}</p>
      <p style={{ margin: "2px 0 0" }}>Total valoraciones: {item.totalValoraciones}</p>
    </div>
  );
}

/**
 * Componente de reporte analítico avanzado (RF-06).
 * Muestra la correlación entre esfuerzo físico y valoración, el top de abandono
 * de actividades y el desempeño comparativo de turistas con itinerarios modificados.
 *
 * @returns {JSX.Element} Vista renderizada del reporte RF-06.
 */
function RF06() {
  /** @type {[Array<ReporteCorrelacionEsfuerzoValoracion & { effortX: number }>, Function]} */
  const [correlationData, setCorrelationData] = useState([]);

  /** @type {[ReporteActividadAbandono[], Function]} */
  const [abandonmentData, setAbandonmentData] = useState([]);

  /** @type {[ReporteComparacionModificacion[], Function]} */
  const [performanceData, setPerformanceData] = useState([]);

  /** @type {[boolean, Function]} */
  const [loading, setLoading] = useState(true);

  /** @type {[string | null, Function]} */
  const [error, setError] = useState(null);

  useEffect(() => {
    /**
     * Carga de forma concurrente los tres conjuntos de datos del reporte RF-06.
     * @async
     * @returns {Promise<void>}
     */
    const cargarTodoElReporte = async () => {
      try {
        const [correlacion, abandono, comparacion] = await Promise.all([
          obtenerCorrelacionEsfuerzoValoracion(),
          obtenerAbandonoActividades(),
          obtenerComparacionModificaciones(),
        ]);

        const puntosCorrelacion = (Array.isArray(correlacion) ? correlacion : []).map((punto) => ({
          ...punto,
          effortX: mapearEsfuerzoANumero(punto.nivelEsfuerzo),
        }));

        setCorrelationData(puntosCorrelacion);
        setAbandonmentData(Array.isArray(abandono) ? abandono : []);
        setPerformanceData(Array.isArray(comparacion) ? comparacion : []);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    cargarTodoElReporte();
  }, []);

  return (
    <div className="report-card">
      <h2>RF-06: Advanced Analysis (Correlation, Abandonment &amp; Performance)</h2>

      {loading && <p>Loading report data...</p>}
      {error && <p style={{ color: "#b91c1c" }}>Error: {error}</p>}

      {!loading && !error && (
        <>
          {/* GRÁFICO DE DISPERSIÓN */}
          <div style={{ marginTop: "2rem" }}>
            <h3>Correlation: Physical Effort vs. Average Rating</h3>
            <p style={{ color: "#666", marginBottom: "1rem", fontSize: "0.9rem" }}>
              Each dot represents a tour package. The X-axis maps effort level (1=Low, 2=Medium, 3=High), and the Y-axis is the average rating.
            </p>
            <div style={{ width: "100%", height: 300 }}>
              <ResponsiveContainer width="100%" height="100%">
                <ScatterChart margin={{ top: 20, right: 20, bottom: 20, left: 20 }}>
                  <CartesianGrid />
                  <XAxis type="number" dataKey="effortX" name="Effort Level" domain={[0, 4]} ticks={[1, 2, 3]} />
                  <YAxis type="number" dataKey="promedioValoracion" name="Average Rating" domain={[0, 5]} />
                  <ZAxis type="number" dataKey="totalValoraciones" range={[80, 220]} name="Ratings" />
                  <Tooltip cursor={{ strokeDasharray: '3 3' }} content={<CorrelationTooltip />} />
                  <Scatter name="Tour Packages" data={correlationData} fill="#764ba2" />
                </ScatterChart>
              </ResponsiveContainer>
            </div>
          </div>

          {/* TABLA DE ABANDONO */}
          <div style={{ marginTop: "3rem" }}>
            <h3>Top 10 Activities with Highest Abandonment Rate</h3>
            <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left", marginTop: "1rem" }}>
              <thead>
                <tr style={{ borderBottom: "2px solid #ddd" }}>
                  <th style={{ padding: "10px" }}>Activity</th>
                  <th style={{ padding: "10px" }}>Creator Guide</th>
                  <th style={{ padding: "10px" }}>Tour Package</th>
                  <th style={{ padding: "10px" }}>Visits (Abandoned / Total)</th>
                  <th style={{ padding: "10px" }}>Abandonment Rate</th>
                </tr>
              </thead>
              <tbody>
                {abandonmentData.length === 0 ? (
                  <tr>
                    <td colSpan="5" style={{ padding: "10px", textAlign: "center" }}>
                      No abandonment records found.
                    </td>
                  </tr>
                ) : (
                  abandonmentData.map((row, index) => (
                    <tr
                      key={`${row.nombreActividad}-${row.nombrePaquete}-${index}`}
                      style={{ borderBottom: "1px solid #eee" }}
                    >
                      <td style={{ padding: "10px" }}>{row.nombreActividad}</td>
                      <td style={{ padding: "10px" }} translate="no">{row.nombreGuia}</td>
                      <td style={{ padding: "10px" }}>{row.nombrePaquete}</td>
                      <td style={{ padding: "10px" }}>
                        {row.visitasAbandonadas} / {row.totalVisitas}
                      </td>
                      <td style={{ padding: "10px", color: "#b91c1c", fontWeight: "bold" }}>
                        {formatearTasaAbandono(row.tasaAbandono)}
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>

          {/* TABLA DE DESEMPEÑO */}
          <div style={{ marginTop: "3rem" }}>
            <h3>Performance: Modifications vs. Group Average</h3>
            <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left", marginTop: "1rem" }}>
              <thead>
                <tr style={{ borderBottom: "2px solid #ddd" }}>
                  <th style={{ padding: "10px" }}>Tourist</th>
                  <th style={{ padding: "10px" }}>Tour Package</th>
                  <th style={{ padding: "10px" }}>Modified Tourist Rating</th>
                  <th style={{ padding: "10px" }}>Group Average</th>
                </tr>
              </thead>
              <tbody>
                {performanceData.length === 0 ? (
                  <tr>
                    <td colSpan="4" style={{ padding: "10px", textAlign: "center" }}>
                      No modification records found.
                    </td>
                  </tr>
                ) : (
                  performanceData.map((row) => (
                    <tr key={`${row.turistaID}-${row.paqueteNombre}`} style={{ borderBottom: "1px solid #eee" }}>
                      <td style={{ padding: "10px" }} translate="no">{row.turistaNombre}</td>
                      <td style={{ padding: "10px" }}>{row.paqueteNombre}</td>
                      <td style={{ padding: "10px", fontWeight: "bold" }}>⭐ {row.promedioTuristaModificado} / 5</td>
                      <td style={{ padding: "10px", fontWeight: "bold" }}>⭐ {row.promedioGrupo} / 5</td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </>
      )}
    </div>
  );
}

export default RF06;