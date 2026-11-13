import { useNavigate } from "react-router-dom";
import GuideSidebar from "../components/GuideSidebar"; 
import "./GuideDashboard.css";

function OtherGuides() {
  const navigate = useNavigate();

  const otherGuidesItineraries = [
    { id: 10, guide: "Liz Cartaya", package: "Havana travel", status: "Approved" },
    { id: 11, guide: "David Castillo", package: "Matanzas, Varadero", status: "In Review" },
    { id: 12, guide: "Maya Ramón", package: "Cayo Santamaría", status: "Approved" },
  ];
  return (
    <div className="app-layout">
      <GuideSidebar />

      <main className="main-content">
        <header className="main-header">
          <h1>Other Guides' Itineraries</h1>
          <p>Package: Havana travel</p>
        </header>

       <div className="itinerary-list">
          {otherGuidesItineraries.map((it) => (
            <div key={it.id} className="itinerary-card" onClick={() => navigate(`/guia/itinerary/${it.id}`, { state: { newItinerary: it } })} style={{ cursor: "pointer" }}>
              {/* NOTA: Agregué el onClick al div de la tarjeta para que al hacer clic te lleve al detalle, así funcionará el botón de volver atrás */}
              <div className="itinerary-info">
                <h3>{it.package}</h3>
                <p>Created by: <strong>{it.guide}</strong></p>
              </div>
              <div className="itinerary-status">
                <span className={`status ${it.status === 'Approved' ? 'approved' : 'pending'}`}>
                  {it.status}
                </span>
              </div>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
}
export default OtherGuides;