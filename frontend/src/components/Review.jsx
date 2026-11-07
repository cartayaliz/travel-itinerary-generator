import { useState } from "react";
import Select from "./Select";
import Button from "./Button";
import Input from "./Input";
import Alerts from "./Alerts";

function Review({onCancel})
{
    const[rating, setRating] = useState("5");
    const[comment, setComment] = useState("");
    const [successMessage, setSuccessMessage] = useState("")
    const[mensajeError, setMensajeError]= useState("");

    const handleSubmit = (e) => {
    e.preventDefault();
    setSuccessMessage("");
    setMensajeError("");

    if (comment.trim() === "") {
      setMensajeError("Please write a comment before submitting.");
      return;
    }

    console.log("Review submitted:", { rating, comment });
    
    // Simulamos éxito
    setSuccessMessage("Thank you! Your review has been submitted.");
    setComment(""); // Limpiamos el formulario
    setRating("5");
  };

    return(
        <div className="form-container">
            <h3>Leave a Review</h3>

            {/* Mostrar mensaje de error o exito */}
            {mensajeError && <Alerts mensaje={mensajeError}/>}
            {successMessage && <Alerts mensaje={successMessage} tipo = "exito"/>}

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label> Rating (1-5 Stars): </label>
                    <Select value={rating} onChange={(e) => setRating(e.target.value)} 
                      options={[ 
                        { value: "5", label: "⭐⭐⭐⭐⭐ (Excellent)" },
                        { value: "4", label: "⭐⭐⭐⭐ (Very Good)" },
                        { value: "3", label: "⭐⭐⭐ (Good)" },
                        { value: "2", label: "⭐⭐ (Fair)" },
                        { value: "1", label: "⭐ (Poor)" }
                      ]}
                    />
                </div>

                    <Input 
                    label="Your Comment"
                    placeholder="Tell us about your experience..."
                    value={comment}
                    onChange={(e) => {setComment(e.target.value);
                        setMensajeError("");
                    }
                    }/>
                   <div style={{ marginTop: "1rem", display: "flex", gap: "1rem" }}>
                    <Button type="submit" texto="Submit Review" />
                    <Button texto="Cancel" onClick={onCancel} />
                   </div>
            </form>

        </div>
    
    );
}

export default Review;