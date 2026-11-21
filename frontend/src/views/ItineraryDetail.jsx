import { useState } from "react";
import { useParams, useNavigate, useLocation } from "react-router-dom";
import Alerts from "../components/Alerts";
import Button from "../components/Button";
import Input from "../components/Input";
import GuideSidebar from "../components/GuideSidebar";
import "./GuideDashboard.css"; //mismo que el CSS del dashboard original

function ItineraryDetail() {
  const [mensajeError, setMensajeError] = useState("");
  const[showActivity, setShowActivity] = useState(false);
  const [activityName, setActivityName] = useState("");
  const [activityHour, setActivityHour] = useState("");
  const [newActivities, setNewActivities] = useState([]); 

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

  

    const handleAddActivity = () => {
        // Limpiamos errores anteriores
        setMensajeError("");
        if (activityName.trim() === "" || activityHour === "") {
        setMensajeError("Please fill all fields");
        return;
    }

    const newAct = {
      id: Date.now(),
      name: activityName,
      hour: activityHour
    };

    // Agregamos la actividad a la lista local
    setNewActivities([...newActivities, newAct]);
    
    // Limpiamos el formulario y lo cerramos
    setActivityName("");
    setActivityHour("");
    setShowActivity(false);
  };

  return (
    <div className="app-layout">
      <GuideSidebar />

      <main className="main-content">
        <header className="main-header">
          <h1>{itinerary.name}</h1>
          <p>Status: {itinerary.status}</p>
          <Button texto="← Back" onClick={() => navigate(-1)} />
        </header>

        <section className="guide-section">
          <div className="section-header">
            <h2>Itinerary Activities</h2>
            <Button texto="+ Add Activity" onClick={() => setShowActivity(true)} />
          </div>
          {showActivity && (
              <div className="form-container">
                <h3>Create New Activity </h3>
                <Alerts mensaje={mensajeError} />
                <Input 
                label="Activity Name" 
                placeholder="Museum Visit" 
                value={activityName} 
                onChange={(e) => { setActivityName(e.target.value);
                setMensajeError(""); 
              
              } }
                
              />
              
              <Input 
                label="Hour" 
                type="time"
                placeholder="5" 
                value={activityHour} 
                onChange={(e) => {setActivityHour(e.target.value);
                setMensajeError("");
                }}
              />
              <div style={{ marginTop: "1rem", display: "flex", gap: "1rem" }}>
                <Button texto="Save" onClick={handleAddActivity} />
                <Button texto="Cancel" onClick={() => { setShowActivity(false); 
                  setActivityName("");
                  setActivityHour("");
                  setMensajeError("");
                } }/>
              </div>
              </div>

          )}

          <div className="itinerary-list">
            {[...(itinerary.activities || []), ...newActivities].length === 0  ? (
              <p>No activities yet. Add one!</p>
            ) : (
              [...(itinerary.activities || []), ...newActivities].map((act) => (
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