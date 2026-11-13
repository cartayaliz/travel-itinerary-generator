import "../../views/Reports.css";
function RF03()
{
     const auditData = [
    { id: 1, itinerary: "Havana Colonial", visit: "Malecón Walk", reviewer: "Jefe de Ops (Daniel)", date: "2026-10-02", observation: "Aprobado sin cambios. Excelente distribución." },
    { id: 2, itinerary: "Varadero Beach", visit: "Beach Relax", reviewer: "Jefe de Ops (Maya)", date: "2026-10-06", observation: "Se sugiere cambiar la hora a las 10:00 AM por el clima." },
    { id: 3, itinerary: "Viñales Trip", visit: "Mountain Hiking", reviewer: "Jefe de Ops (René)", date: "2026-10-12", observation: "Revisar nivel de esfuerzo, puede ser muy alto para el grupo." },
  ];

    return(
           <div className="report-card">
      <h2>RF-03: Operations Manager validations</h2>
      <p style={{ color: "#666", marginBottom: "1rem" }}>
        Revisions and observations on individual visits.
      </p>

      <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left" }}>
        <thead>
          <tr style={{ borderBottom: "2px solid #ddd" }}>
            <th style={{ padding: "10px" }}>ID</th>
            <th style={{ padding: "10px" }}>Itinerary</th>
            <th style={{ padding: "10px" }}>Visit / Activity</th>
            <th style={{ padding: "10px" }}>Reviewer</th>
            <th style={{ padding: "10px" }}>Date</th>
            <th style={{ padding: "10px" }}>Observation</th>
          </tr>
        </thead>
        <tbody>
          {auditData.map((row) => (
            <tr key={row.id} style={{ borderBottom: "1px solid #eee" }}>
              <td style={{ padding: "10px" }}>{row.id}</td>
              <td style={{ padding: "10px" }}>{row.itinerary}</td>
              <td style={{ padding: "10px" }}>{row.visit}</td>
              <td style={{ padding: "10px" }}>{row.reviewer}</td>
              <td style={{ padding: "10px" }}>{row.date}</td>
              <td style={{ padding: "10px", fontStyle: "italic", color: "#555" }}>
                "{row.observation}"
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
    );
}
export default RF03;