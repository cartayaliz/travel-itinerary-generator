import { ScatterChart, Scatter, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, ZAxis } from 'recharts';
import "../../views/Reports.css";

function RF06() {
  // Datos para la Correlación (Gráfico de Dispersión)
  // Eje X: Nivel de esfuerzo (1=Bajo, 2=Medio, 3=Alto) | Eje Y: Valoración promedio (1-5)
  const correlationData = [
    { effort: 1, rating: 4.8, activity: "Malecón Walk" },
    { effort: 1, rating: 4.9, activity: "Beach Relax" },
    { effort: 2, rating: 4.2, activity: "Museum Visit" },
    { effort: 2, rating: 3.8, activity: "Old Havana Tour" },
    { effort: 3, rating: 3.1, activity: "Mountain Hiking" },
    { effort: 3, rating: 2.9, activity: "Scuba Diving" },
  ];

  // Datos para la tabla de Abandono (Top 10)
  const abandonmentData = [
    { id: 1, activity: "Scuba Diving", guide: " René Díaz", package: "Varadero Beach", abandonmentRate: "45%" },
    { id: 2, activity: "Mountain Hiking", guide: " Maya Ramón", package: "Viñales Trip", abandonmentRate: "38%" },
    { id: 3, activity: "Art Workshop", guide: " Liz Cartaya", package: "Havana Colonial", abandonmentRate: "25%" },
  ];

  // Datos para la tabla de Desempeño (Modificaciones)
  const performanceData = [
    { id: 1, group: "Requested Modifications", avgRating: 3.5 },
    { id: 2, group: "General Average", avgRating: 4.2 },
  ];

  return (
    <div className="report-card">
      <h2>RF-06: Advanced Analysis (Correlation, Abandonment & Performance)</h2>
      
      {/* GRÁFICO DE DISPERSIÓN */}
      <div style={{ marginTop: "2rem" }}>
        <h3> Correlation: Physical Effort vs. Average Rating</h3>
        <p style={{ color: "#666", marginBottom: "1rem", fontSize: "0.9rem" }}>
          Each dot represents an activity. The X-axis is the effort level (1=Low, 2=Medium, 3=High), and the Y-axis is the average rating.
        </p>
        <div style={{ width: "100%", height: 300 }}>
          <ResponsiveContainer width="100%" height="100%">
            <ScatterChart margin={{ top: 20, right: 20, bottom: 20, left: 20 }}>
              <CartesianGrid />
              <XAxis type="number" dataKey="effort" name="Effort Level" domain={[0, 4]} ticks={[1, 2, 3]} />
              <YAxis type="number" dataKey="rating" name="Average Rating" domain={[0, 5]} />
              <ZAxis type="number" range={[100, 100]} /> {/* Tamaño del punto */}
              <Tooltip cursor={{ strokeDasharray: '3 3' }} />
              <Scatter name="Activities" data={correlationData} fill="#764ba2" />
            </ScatterChart>
          </ResponsiveContainer>
        </div>
      </div>

      {/* TABLA DE ABANDONO */}
      <div style={{ marginTop: "3rem" }}>
        <h3> Top 10 Activities with Highest Abandonment Rate</h3>
        <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left", marginTop: "1rem" }}>
          <thead>
            <tr style={{ borderBottom: "2px solid #ddd" }}>
              <th style={{ padding: "10px" }}>ID</th>
              <th style={{ padding: "10px" }}>Activity</th>
              <th style={{ padding: "10px" }}>Creator Guide</th>
              <th style={{ padding: "10px" }}>Tour Package</th>
              <th style={{ padding: "10px" }}>Abandonment Rate</th>
            </tr>
          </thead>
          <tbody>
            {abandonmentData.map((row) => (
              <tr key={row.id} style={{ borderBottom: "1px solid #eee" }}>
                <td style={{ padding: "10px" }}>{row.id}</td>
                <td style={{ padding: "10px" }}>{row.activity}</td>
                <td style={{ padding: "10px" }} translate="no">{row.guide}</td>
                <td style={{ padding: "10px" }}>{row.package}</td>
                <td style={{ padding: "10px", color: "#b91c1c", fontWeight: "bold" }}>{row.abandonmentRate}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* TABLA DE DESEMPEÑO */}
      <div style={{ marginTop: "3rem" }}>
        <h3> Performance: Modifications vs. General Average</h3>
        <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left", marginTop: "1rem" }}>
          <thead>
            <tr style={{ borderBottom: "2px solid #ddd" }}>
              <th style={{ padding: "10px" }}>ID</th>
              <th style={{ padding: "10px" }}>Tourist Group</th>
              <th style={{ padding: "10px" }}>Average Rating</th>
            </tr>
          </thead>
          <tbody>
            {performanceData.map((row) => (
              <tr key={row.id} style={{ borderBottom: "1px solid #eee" }}>
                <td style={{ padding: "10px" }}>{row.id}</td>
                <td style={{ padding: "10px" }}>{row.group}</td>
                <td style={{ padding: "10px", fontWeight: "bold" }}>⭐ {row.avgRating} / 5</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default RF06;