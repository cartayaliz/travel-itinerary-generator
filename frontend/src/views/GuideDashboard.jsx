import { useState } from "react"; // 1. IMPORTAMOS USESTATE
import "./GuideDashboard.css";
import Input from "../components/Input";
import Button from "../components/Button"; 
import Alerts from "../components/Alerts";
import { useNavigate } from "react-router-dom";

//importante todavia no funcionan profile y crear activity eso deberia llevarte a otro dashboard
function GuideDashboard() {
  const [activityName, setActivityName] = useState("");
  const [activityHour, setActivityHour] = useState("");
  const [newActivities, setNewActivities] = useState([]); 

  const [activeTab, setActiveTab] = useState("My Itineraries");
  const[mensajeError, setMensajeError]= useState("");
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
    setMensajeError("");
    if (name.trim() === "" || activities === "") {
      setMensajeError("Please fill all fields");
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
  <Alerts mensaje={"Activity successfully created"} tipo="exito"/>  

   // 1. Juntamos las actividades viejas con la nueva
    const updatedActivities = [...newActivities, newAct];
    
    // 2. Las ordenamos por la propiedad 'hour'
    // (localeCompare funciona perfecto con horas en formato "HH:MM")
    updatedActivities.sort((a, b) => a.hour.localeCompare(b.hour));

    // 3. Guardamos la lista ya ordenada
    setNewActivities(updatedActivities);


    setActivityName("");
    setActivityHour("");
  };
  return (
    <div className="app-layout">
      <aside className="sidebar">
        <h2>Guide Menu</h2>
        <ul>
          <li className={activeTab === "My Itineraies" ? "active" : ""} onClick={() => setActiveTab("My Itineraries")}>
            My Itineraries
          </li>
          <li className={activeTab === "Create Activity" ? "active" : ""} onClick={() => setActiveTab("Create Activity")}>
            Create Activity
          </li>
          <li className={activeTab === "Profile" ? "active" : ""} onClick={() => setActiveTab("Profile")}>
            Profile
          </li>
          <li onClick={() => navigate("/guia/other-guides")}>Other Guides</li>
        </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>Guide Dashboard</h1>
          <p>Welcome back, René Díaz</p>
        </header>
        { activeTab === "My Itineraries" && (

          <section className="guide-section">
          <div className="section-header">
            <h2>My Itineraries</h2>
            <Button 
              texto="+ Generate New Itinerary" 
              onClick={() => setShowForm(true)} 
            />
          </div>
          {/* se ve el formulario de crear nuevo solo cuando el show es true que ocurre cuando el click */}
          {showForm && (
            <div className="form-container">
              <h3>Create New Itinerary</h3>
              <Alerts mensaje={mensajeError} />

              <Input 
                label="Itinerary Name" 
                placeholder="Havana Adventure" 
                value={name} 
                onChange={(e) =>{ setName(e.target.value);
                setMensajeError("");} }
              />
              
              <Input 
                label="Number of Activities" 
                type="number"
                placeholder="5" 
                value={activities} 
                onChange={(e) => {setActivities(e.target.value);
                setMensajeError("");} }
              />

              <div style={{ marginTop: "1rem", display: "flex", gap: "1rem" }}>
                <Button texto="Save" onClick={handleSave} />
                <Button texto="Cancel" onClick={() => {setShowForm(false);
                setMensajeError("");
                setActivities("");
                setName(""); }} />
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
        )}
        { activeTab === "Create Activity" && (
          <section className="guide-section">
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
                <Button texto="Cancel" onClick={() => {
                  setActivityName("");
                  setActivityHour("");
                  setMensajeError("");
                  setActiveTab("My Itineraries");
                } }/>
              </div>
              </div>
               {/* Lista de actividades creadas */}
              <div className="itinerary-list" style={{ marginTop: "2rem" }}>
                <h3>Activities Created</h3>
                {newActivities.length === 0 ? (
                  <p>No activities created yet. Fill the form above.</p>
                ) : (
                  [...newActivities].sort((a, b) => a.hour.localeCompare(b.hour)).map((act) => (
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
          ) 
        }
        
      </main>
    </div>
  );
}

export default GuideDashboard;