import { useState, useEffect, useMemo } from 'react';
import BarGraph from "../BarGraph";
import { obtenerSatisfaccionEsfuerzo } from "../../services/reporteService";
import "../../views/Reports.css";

/**
 * @typedef {Object} ReporteSatisfaccionEsfuerzo
 * @property {string} nombreActividad - Nombre de la actividad turística.
 * @property {string} nivelEsfuerzo - Nivel de esfuerzo físico requerido.
 * @property {number} promedioValoracion - Promedio de valoración de los turistas.
 * @property {number} totalValoraciones - Cantidad total de valoraciones recibidas.
 */

/**
 * Obtiene la clase CSS correspondiente al nivel de esfuerzo (soporta inglés y español).
 * @param {string | undefined} nivel - Nivel de esfuerzo recibido del backend.
 * @returns {'effort-high' | 'effort-medium' | 'effort-low'}
 */
function obtenerClaseEsfuerzo(nivel) {
  const normalizado = String(nivel ?? '').trim().toLowerCase();
  if (normalizado === 'high' || normalizado === 'alto' || normalizado === 'alta') {
    return 'effort-high';
  }
  if (normalizado === 'medium' || normalizado === 'medio' || normalizado === 'media') {
    return 'effort-medium';
  }
  return 'effort-low';
}

/**
 * Componente del reporte RF-04: satisfacción y esfuerzo físico por actividad.
 * @returns {JSX.Element}
 */
function RF04() {
  /** @type {[ReporteSatisfaccionEsfuerzo[], Function]} */
  const [data, setData] = useState([]);

  /** @type {[boolean, Function]} */
  const [loading, setLoading] = useState(true);

  /** @type {[string | null, Function]} */
  const [error, setError] = useState(null);

  useEffect(() => {
    /**
     * Carga los datos de satisfacción y esfuerzo desde la API.
     * @async
     * @returns {Promise<void>}
     */
    const cargarReporte = async () => {
      try {
        const resultado = await obtenerSatisfaccionEsfuerzo();
        setData(Array.isArray(resultado) ? resultado : []);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    cargarReporte();
  }, []);

  // Gráfico mostrando la valoración promedio por actividad
  const chartData = useMemo(
    () =>
      data.map((item) => ({
        name: item.nombreActividad,
        total: item.promedioValoracion,
      })),
    [data]
  );

  return (
    <div className="report-card">
      <h2>RF-04: Satisfaction and Physical Effort</h2>
      <p style={{ color: "#666", marginBottom: "1rem" }}>
        Tourist ratings compared against the required physical effort.
      </p>

      {loading && <p>Loading satisfaction and effort report...</p>}
      {error && <p style={{ color: "#b91c1c" }}>Error: {error}</p>}

      {!loading && !error && (
        <>
          {/* Tabla */}
          <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left" }}>
            <thead>
              <tr style={{ borderBottom: "2px solid #ddd" }}>
                <th style={{ padding: "10px" }}>ID</th>
                <th style={{ padding: "10px" }}>Activity</th>
                <th style={{ padding: "10px" }}>Effort</th>
                <th style={{ padding: "10px" }}>Rating</th>
                <th style={{ padding: "10px" }}>Total Reviews</th>
              </tr>
            </thead>
            <tbody>
              {data.length === 0 ? (
                <tr>
                  <td colSpan="5" style={{ padding: "10px", textAlign: "center" }}>
                    No satisfaction records found.
                  </td>
                </tr>
              ) : (
                data.map((row, index) => {
                  const badgeClass = obtenerClaseEsfuerzo(row.nivelEsfuerzo);

                  return (
                    <tr key={`${row.nombreActividad}-${index}`} style={{ borderBottom: "1px solid #eee" }}>
                      <td style={{ padding: "10px" }}>{index + 1}</td>
                      <td style={{ padding: "10px" }}>{row.nombreActividad}</td>
                      <td style={{ padding: "10px" }}>
                        <span className={`effort-badge ${badgeClass}`}>
                          {row.nivelEsfuerzo}
                        </span>
                      </td>
                      <td style={{ padding: "10px" }}>⭐ {row.promedioValoracion} / 5</td>
                      <td style={{ padding: "10px" }}>{row.totalValoraciones}</td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>

          {/* Gráfico de barras */}
          {chartData.length > 0 && (
            <div style={{ marginTop: "2rem", height: "300px" }}>
              <BarGraph data={chartData} title="Average Rating by Activity" />
            </div>
          )}
        </>
      )}
    </div>
  );
}

export default RF04;