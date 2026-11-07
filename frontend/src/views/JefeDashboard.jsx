import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import "./GuideDashboard.css"; 

function JefeDashboard() {
  const navigate = useNavigate();

  //Itinerarios esperando validación
  const [pendingItineraries, setPendingItineraries] = useState([
    { id: 1, name: "Havana Colonial", guide: "René Díaz", status: "In Review" },
    { id: 2, name: "Varadero Beach", guide: "Liz Cartaya", status: "In Review" },
  ]);

  //función para cambiar el estado a "Approved" o "Rejected"
  const handleValidation = (id, newStatus) => {
    //busca el itinerario por id, le cambia el estado y actualiza la lista
    const updatedList = pendingItineraries.map((it) => 
      it.id === id ? { ...it, status: newStatus } : it
    );
    setPendingItineraries(updatedList);
  };

  return (
    <div className="app-layout">
      <aside className="sidebar">
        <h2>Chief Operations</h2>
        <ul>
          <li className="active">Validate Itineraries</li>
          <li onClick={() => navigate("/reports")}>Reports</li>
        </ul>
        <Button texto="Logout" onClick={() => navigate("/login")} />
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>Itinerary Validation</h1>
          <p>Review and approve generated itineraries</p>
        </header>

        <div className="itinerary-list">
          {pendingItineraries.map((it) => (
            <div key={it.id} className="itinerary-card" style={{ flexDirection: "column", alignItems: "flex-start" }}>
              <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                <div className="itinerary-info">
                  <h3>{it.name}</h3>
                  <p>Guide: {it.guide}</p>
                </div>
                <div className="itinerary-status">
                  <span className={`status ${it.status === 'Approved' ? 'approved' : it.status === 'Rejected' ? 'high' : 'pending'}`}>
                    {it.status}
                  </span>
                </div>
              </div>
              
              {/*botones para el jefe si quiee aprobar o rechazar*/}
              {it.status === "In Review" && (
                <div style={{ marginTop: "1rem", display: "flex", gap: "1rem" }}>
                  <Button texto="Approve" onClick={() => handleValidation(it.id, "Approved")} />
                  <Button texto="Reject" onClick={() => handleValidation(it.id, "Rejected")} />
                </div>
              )}
            </div>
          ))}
        </div>
      </main>
    </div>
  );
}

export default JefeDashboard;