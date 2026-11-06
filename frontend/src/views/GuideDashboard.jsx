import { useState } from "react"; // 1. IMPORTAMOS USESTATE
import "./GuideDashboard.css";
import Input from "../components/Input";
import Button from "../components/Button"; 

import { useNavigate } from "react-router-dom";

//importante todavia no funcionan profile y crear activity eso deberia llevarte a otro dashboard
function GuideDashboard() {

  const navigate = useNavigate(); 
  const [myItineraries, setMyItineraries] = useState([
    { 
      id: 1, 
      name: "Havana Colonial", 
      status: "Approved", 
      activities: [
        { id: 101, name: "Malecón Walk", hour: "09:00" },
        { id: 102, name: "Museum Visit", hour: "11:00" }
      ] 
    },
    { 
      id: 2, 
      name: "Varadero Beach", 
      status: "In Review", 
      activities: [
        { id: 201, name: "Beach Relax", hour: "10:00" }
      ] 
    },
   ]);
   //en prueba agregar actividdes
  const [showForm, setShowForm] = useState(false);
  const [name, setName] = useState("");
  const [activities, setActivities] = useState("");
  const handleSave = () => {
    if (name.trim() === "" || activities === "") {
      alert("Please fill all fields");
      return;
    }

    const newItinerary = {
      id: Date.now(), // ID único
      name: name,
      status: "In Review", // los nuevos entran en revisión
      activities: [],
    };

    setMyItineraries([...myItineraries, newItinerary]);
    setName("");
    setActivities("");
    setShowForm(false);
    navigate(`/guia/itinerary/${newItinerary.id}`, { state: { newItinerary } });//para que se muestren dos dashboard en el navegador
  };

  return (
    <div className="app-layout">
      <aside className="sidebar">
        <h2>Guide Menu</h2>
        <ul>
          <li className="active">My Itineraries</li>
          <li>Create Activity</li>
          <li>Profile</li>
          <li onClick={() => navigate("/guia/other-guides")}>Other Guides</li>
        </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>Guide Dashboard</h1>
          <p>Welcome back, René Díaz</p>
        </header>

        <section className="guide-section">
          <div className="section-header">
            <h2>My Itineraries</h2>
            <Button 
              texto="+ Generate New Itinerary" 
              onClick={() => setShowForm(true)} 
            />
          </div>
          {/* se ve el formulario de crear nuevo solo cuando el show estrue que ocurre cuando el click */}
          {showForm && (
            <div className="form-container">
              <h3>Create New Itinerary</h3>
              
              <Input 
                label="Itinerary Name" 
                placeholder="Havana Adventure" 
                value={name} 
                onChange={(e) => setName(e.target.value)} 
              />
              
              <Input 
                label="Number of Activities" 
                type="number"
                placeholder="5" 
                value={activities} 
                onChange={(e) => setActivities(e.target.value)} 
              />

              <div style={{ marginTop: "1rem", display: "flex", gap: "1rem" }}>
                <Button texto="Save" onClick={handleSave} />
                <Button texto="Cancel" onClick={() => setShowForm(false)} />
              </div>
            </div>
          )}

          <div className="itinerary-list">
            {myItineraries.map((itinerary) => (
              <div key={itinerary.id} className="itinerary-card">
                <div className="itinerary-info">
                  <h3>{itinerary.name}</h3>
                  <p>Activities: {itinerary.activities.length}</p>
                </div>
                <div className="itinerary-status">
                  <span className={`status ${itinerary.status === 'Approved' ? 'approved' : 'pending'}`}>
                    {itinerary.status}
                  </span>
                </div>
              </div>
            ))}
          </div>
        </section>
      </main>
    </div>
  );
}

export default GuideDashboard;