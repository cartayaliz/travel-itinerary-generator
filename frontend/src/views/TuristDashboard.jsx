import "./TuristaDashboard.css";

function TuristaDashboard() {
  return (
    <div className="turist-layout">
      {}
      <aside className="sidebar">
        <ul>
          <li></li>
        </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          
        </header>
        <section className="itinerary-section">
          
          <div className="list-activities">
            <div className="activity-card">
               <div className="hour">--:--</div>
               <div className="details">
                 <h3>Nombre de la actividad</h3>
                 <span className="low effort">Effort: Low</span>
               </div>
            </div>
          </div>
        </section>
      </main>
    </div>
  );
}

export default TuristaDashboard;