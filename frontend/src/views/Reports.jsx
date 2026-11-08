import RF01 from "../components/reports/RF01";
import RF02 from "../components/reports/RF02";
import "./Reports.css";

{/* ejemplos temp */}
function Reports() {
  
  return (
    <div className="reports-layout">
      <header className="reports-header">
        <h1>Reports & Analytics</h1>
        <p>System generated reports</p>
      </header>

      <main className="reports-content">
        <RF01/>
        <RF02/>
      </main>
    </div>
  );
}

export default Reports;