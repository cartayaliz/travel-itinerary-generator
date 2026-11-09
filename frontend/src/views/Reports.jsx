import RF01 from "../components/reports/RF01";
import RF02 from "../components/reports/RF02";
import RF03 from "../components/reports/RF03";
import RF04 from "../components/reports/RF04";
import RF05 from "../components/reports/RF05";
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
        <RF03/>
        <RF04/>
        <RF05/>
      </main>
    </div>
  );
}

export default Reports;