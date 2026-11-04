import "./Reports.css";
{/* ejemplos temp */}
function Reports() {
  const reportsData = [
    { id: 1, package: "Havana Colonial", guide: "René Díaz", date: "2026-10-01", status: "Approved" },
    { id: 2, package: "Varadero Beach", guide: "Liz Cartaya", date: "2026-10-05", status: "In Review" },
    { id: 3, package: "Viñales Trip", guide: "Maya Ramón", date: "2026-10-10", status: "Approved" },
  ];

  return (
    <div className="reports-layout">
      <header className="reports-header">
        <h1>Reports & Analytics</h1>
        <p>System generated reports</p>
      </header>

      <main className="reports-content">
        <div className="report-card">
          <h2>RF-01: Generated Itineraries</h2>
          <table className="reports-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Tour Package</th>
                <th>Creator Guide</th>
                <th>Creation Date</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {reportsData.map((row) => (
                <tr key={row.id}>
                  <td>{row.id}</td>
                  <td>{row.package}</td>
                  <td>{row.guide}</td>
                  <td>{row.date}</td>
                  <td>
                    <span className={`status ${row.status === 'Approved' ? 'approved' : 'pending'}`}>
                      {row.status}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </main>
    </div>
  );
}

export default Reports;