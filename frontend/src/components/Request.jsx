import { useState } from "react";
import Select from "./Select";
import Button from "./Button";
import Input from "./Input";
import Alerts from "./Alerts";

function Request({onCancel})
{
  const[activity, setActivity] = useState("Malecon Walk");
  const [reason, setReason] = useState("");
  const [mensajeError, setMensajeError] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const handleSubmit = (e) => 
    {
        e.preventDefault();
        setMensajeError("");
        setSuccessMessage("");

        if (reason.trim() === "") {
        setMensajeError("Please explain why you want to change this activity.");
        return;
        }

        setSuccessMessage("Your request has been sent to your guide.");
        setReason(""); 
        setActivity("Malecon Walk"); // Volvemos a la opción por defecto
    };
    return(
        <div className="form-container" >
            <h3>Request Itinerary Modification</h3>
      
            {mensajeError && <Alerts mensaje={mensajeError} />}
            {successMessage && <Alerts mensaje={successMessage} tipo="exito" />}

            <form onSubmit={handleSubmit}>
                <Select 
                label="Which activity do you want to change?"
                value={activity}
                onChange={(e) => setActivity(e.target.value)}
                options={[
                    { value: "Malecon Walk", label: "09:00 - Walk along the Malecón" },
                    { value: "Museum Visit", label: "11:00 - Museum Visit" },
                    { value: "Lunch", label: "13:00 - Lunch at Old Havana" }
                ]}
                />
                <Input 
                label="Reason for change"
                placeholder="E.g. I feel too tired for this activity..."
                value={reason}
                onChange={(e) => setReason(e.target.value)}
                />

                <div style={{ display: "flex", gap: "1rem", marginTop: "1rem" }}>
                <Button type="submit" texto="Send Request" />
                <Button texto="Cancel" onClick={onCancel} />
                </div>
            </form>

        </div>
    );
}
export default Request;