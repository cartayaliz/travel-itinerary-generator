import { useState } from "react";

function Input({label, type = "text", placeholder, value, onChange, required= false, minLength})
{
    // Estado para controlar si se ve o no la contraseña
     const [mostrarPassword, setMostrarPassword] = useState(false);
       // Determinamos el tipo real del input. Si es "password" y "mostrarPassword" es true, será "text".
     const tipoInput = type === "password" && mostrarPassword ? "text" : type;
    return(
    <div className="form-group">

        <label>{label}</label>
        {/* Contenedor relativo para poder posicionar el emoji de ojito a la derecha */}
      <div style={{ position: "relative" }}>
        <input type={tipoInput} placeholder={placeholder} value={value} onChange={onChange} required={required} minLength={minLength} style={{ width: "100%", paddingRight: "40px" }} // Espacio extra para que el texto no choque con el ojo
        />
        {/* Si el input original es de tipo password, mostramos el emoji de ojito */}
        {type === "password" && (
          <button
            type="button"
            onClick={() => setMostrarPassword(!mostrarPassword)}
            style={{
              position: "absolute",
              right: "10px",
              top: "50%",
              transform: "translateY(-50%)",
              background: "none",
              border: "none",
              cursor: "pointer",
              fontSize: "1.2rem",
            }}
          >
            {mostrarPassword ? "👁️" : "👁️"}
          </button>
        )}
      </div>

    </div>

    );
}
export default Input;