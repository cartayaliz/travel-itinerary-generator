import { useState } from "react";
import { useNavigate} from "react-router-dom";
import Input from "../components/Input";
import Button from "../components/Button"; 
import Alerts from "../components/Alerts";

function CreateActivityTab(){
   
  const navigate = useNavigate(); 
  const[mensajeError, setMensajeError]= useState(""); 
  const [successMessage, setSuccessMessage] = useState("");
  const [activityName, setActivityName] = useState("");
  const [activityHour, setActivityHour] = useState("");
  const [newActivities, setNewActivities] = useState([]); 

  const handleAddActivity = () => {
        // Limpiamos errores y mensajes anteriores
        setMensajeError("");
        setSuccessMessage("");

        if (activityName.trim() === "" || activityHour === "") {
        setMensajeError("Please fill all fields");
        return;
    }

  const newAct = {
      id: Date.now(),
      name: activityName,
      hour: activityHour
    };  
  

   // 1. Juntamos las actividades viejas con la nueva
    const updatedActivities = [...newActivities, newAct];
    
    // 2. Las ordenamos por la propiedad 'hour'
    // (localeCompare funciona perfecto con horas en formato "HH:MM")
    updatedActivities.sort((a, b) => a.hour.localeCompare(b.hour));

    // 3. Guardamos la lista ya ordenada
    setNewActivities(updatedActivities);


    setActivityName("");
    setActivityHour("");
    setSuccessMessage("Activity created successfully !");
  };
    const handleDeleteActivity = (id) => {
    // Filtramos para dejar solo las actividades cuyo ID sea diferente al que queremos borrar
    const updatedActivities = newActivities.filter((act) => act.id !== id);
    setNewActivities(updatedActivities);
  };

  return(
    <section className="guide-section">
              <div className="form-container">
                <h3>Create New Activity </h3>
              
              <Alerts mensaje={mensajeError} />
              {successMessage && <Alerts mensaje={successMessage} tipo="exito" />}

                <Input 
                label="Activity Name" 
                placeholder="Museum Visit" 
                value={activityName} 
                onChange={(e) => { setActivityName(e.target.value);
                setMensajeError(""); 
                setSuccessMessage("");
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
                  setSuccessMessage("");
                  navigate("/guia/dashboard", {state:{tab:"My Itineraries"}});
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
                      <button 
                        onClick={() => handleDeleteActivity(act.id)} 
                        className="btn-delete"
                        title="Delete activity"
                      >
                        🗑️
                      </button>
                    </div>
                  ))
                )}
              </div>
            </section>
  );
}
export default CreateActivityTab;