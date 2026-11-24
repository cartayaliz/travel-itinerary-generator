import { useState, useEffect } from "react";
import { useNavigate} from "react-router-dom";
import Input from "../components/Input";
import Button from "../components/Button"; 
import Alerts from "../components/Alerts";
import ExportButton from "./ExportButton";
import { obtenerMisItinerarios, crearItinerario } from "../services/itineraryService";

function MyItinerariesTab(){
   
 const navigate = useNavigate(); 
 const[mensajeError, setMensajeError]= useState("");
 const [showForm, setShowForm] = useState(false);
 const [name, setName] = useState("");
 const [activities, setActivities] = useState("");
 const [myItineraries, setMyItineraries] = useState([]);
 const [loading, setLoading] = useState(true);

 // Cargar los itinerarios reales al entrar a la pestaña
  useEffect(() => {
    const fetchItineraries = async () => {
      try {
        const data = await obtenerMisItinerarios();
        setMyItineraries(data);
      } catch (error) {
        console.error(error);
        setMensajeError("Error al cargar los itinerarios del servidor");
      } finally {
        setLoading(false);
      }
    };
    fetchItineraries();
  }, []);

  const handleSave = async () => {
    setMensajeError("");
    if (name.trim() === "" || activities === "") {
      setMensajeError("Please fill all fields");
      return;
    }

    try {
      // Armamos el JSON con los datos exactos que espera el DTO de C#
      const nuevoItinerarioData = {
        nombre: name,
        origenItinerario: 0, 
        guiaID: 1, // TEMPORAL: Aquí debería poner el ID del guía logueado
        cantidadActividades: parseInt(activities),
        proporcionVisitas: 30,
        proporcionExcursiones: 40,
        proporcionTalleres: 30,
        coberturaCategorias: 100
      };
      
      // Enviamos al backend
      await crearItinerario(nuevoItinerarioData);
      
      // Volvemos a pedir la lista actualizada para que aparezca el nuevo
      const dataActualizada = await obtenerMisItinerarios();
      setMyItineraries(dataActualizada);
      
      setName("");
      setActivities("");
      setShowForm(false);
      
    } catch (error) {
      console.error(error);
      setMensajeError("Error al guardar el itinerario en el servidor");
    }
  };

  if (loading) {
    return <p>Cargando itinerarios...</p>;
  }


 return (
      <section className="guide-section">
        <div id="itineraries-section">
      <div className="section-header">
        <h2>My Itineraries</h2>

        <div style={{ display: "flex", alignItems: "flex-start", gap: "8px", flex: 1, minWidth: 0 }}>
           <Button
            texto="+ Generate New Itinerary"
            onClick={() => setShowForm(true)}
            className="btn-primary itinerary-top-btn btn-ignore-pdf"
          />
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
      </div>

      <div className="itinerary-list" id="itineraries-section">
        {myItineraries.length === 0 ? (
          <p>No itineraries found. Create one!</p>
        ) : (
          myItineraries.map((itinerary) => (
            <div key={itinerary.itinerarioID} className="itinerary-card">
              <div className="itinerary-info">
                <h3>{itinerary.nombre }</h3>
                <p>Activities: {itinerary.cantidadActividades }</p>
              </div>
              <div className="itinerary-status">
                <span className="status pending">In Review</span>
              </div>
            </div>
          ))
        )}
      </div>
    </section>
 );
} 
export default MyItinerariesTab;