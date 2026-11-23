import { useState } from "react";
import { useNavigate} from "react-router-dom";
import Input from "../components/Input";
import Button from "../components/Button"; 
import Alerts from "../components/Alerts";
import ExportButton from "./ExportButton";

function MyItinerariesTab(){
   
 const navigate = useNavigate(); 
 const[mensajeError, setMensajeError]= useState("");
 const [showForm, setShowForm] = useState(false);
 const [name, setName] = useState("");
 const [activities, setActivities] = useState("");
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
    navigate(`/guia/itinerary/${newItinerary.id}`, { state: { newItinerary } });
 }; 

 return (
      <section className="guide-section">
      <div className="section-header">
        <h2>My Itineraries</h2>
           <Button texto="+ Generate New Itinerary" onClick={() => setShowForm(true)} />
         <div style={{ display: "flex", gap: "1rem", alignItems: "center" }}>
          <ExportButton elementId="itineraries-section" fileName="my-itineraries.pdf" />
          </div>
      </div>
   
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

      <div className="itinerary-list" id="itineraries-section">
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
 );
} 
export default MyItinerariesTab;