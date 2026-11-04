
import { useParams, useNavigate, useLocation } from "react-router-dom";
import Button from "../components/Button";
import "./GuideDashboard.css"; //mismo que el CSS del dashboard original

function ItineraryDetail() {
  const { id } = useParams(); //saca el ID de la URL
  const navigate = useNavigate();
  const location = useLocation(); //para leer los datos que enviamos
  const incomingItinerary = location.state?.newItinerary; // recibe el itinerario nuevo
  //esto vendrá del backend luego
  const mockItineraries = [
    { 
      id: 1, 
      name: "Havana Colonial", 
      status: "Approved", 
      activities: [
        { id: 101, name: "Malecón Walk", hour: "09:00" },
        { id: 102, name: "Museum Visit", hour: "11:00" }
      ] 
    }
  ];

  const itinerary = incomingItinerary || mockItineraries.find((i) => i.id === Number(id));
  if (!itinerary) {
    return (
      <div style={{ padding: "2rem" }}>
        <h2>Itinerary not found.</h2>
        <Button texto="Back to Dashboard" onClick={() => navigate("/guia/dashboard")} />
      </div>
    );
  }
  return (
    <div className="app-layout">
      <aside className="sidebar">
        <h2>Guide Menu</h2>
        <ul>
          <li className="active" onClick={() => navigate("/guia/dashboard")}>Back to Dashboard</li>
        </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>{itinerary.name}</h1>
          <p>Status: {itinerary.status}</p>
        </header>

        <section className="guide-section">
          <div className="section-header">
            <h2>Itinerary Activities</h2>
            <Button texto="+ Add Activity" onClick={() => alert("Próximamente")} />
          </div>

          <div className="itinerary-list">
            {itinerary.activities.length === 0 ? (
              <p>No activities yet. Add one!</p>
            ) : (
              itinerary.activities.map((act) => (
                <div key={act.id} className="itinerary-card">
                  <div className="itinerary-info">
                    <h3>{act.name}</h3>
                    <p>Time: {act.hour}</p>
                  </div>
                </div>
              ))
            )}
          </div>
        </section>
      </main>
      
    </div>
  );
  
}

export default ItineraryDetail;