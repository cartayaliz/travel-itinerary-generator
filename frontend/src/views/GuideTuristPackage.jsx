import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
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
      <aside className="sidebar">
        <h2>Guide Menu</h2>
        <ul>
          <li onClick={() => navigate("/guia/dashboard")}>My Itineraries</li>
          <li onClick={() => navigate("/guia/create-activity")}>Create Activity</li> {/* despues con lo que liz hizo */}
          <li>Profile</li>
          <li className="active">Other Guides' Itineraries</li>
        </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>Other Guides' Itineraries</h1>
          <p>Package: Havana travel</p>
        </header>

        <div className="itinerary-list">
          {otherGuidesItineraries.map((it) => (
            <div key={it.id} className="itinerary-card">
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