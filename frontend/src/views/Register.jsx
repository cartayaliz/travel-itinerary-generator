import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import Input from "../components/Input";
import Select from "../components/Select";
import Layout from "../components/Layout";
import Alerts from "../components/Alerts";
import "./Register.css";

function Register() {
    const [mensajeError, setMensajeError] = useState("");
    const [usuario, setUsuario] = useState("");
    const [password, setPassword] = useState("");
    const [rol, setRol] = useState("guia");
    const opcionesRoles = [
  { value: "guia", label: "Guía Turístico" },
  { value: "admin", label: "Administrador" },
  { value: "jefe", label: "Jefe de Operaciones" },
  { value: "turista", label: "Turista" }
];
    const navigate = useNavigate();  

    const handleSubmit = (e) => { e.preventDefault(); 
    setMensajeError("");
    // 1. Validar que tenga al menos una letra y un número
    const tieneLetraYNumero = /^(?=.*[A-Za-z])(?=.*\d).+$/;

    if (!usuario || !password) {
      setMensajeError("Por favor, llena todos los campos");
      return;
    }
    if (!tieneLetraYNumero.test(password)) {
    setMensajeError("La contraseña debe contener al menos una letra y un número");
    return;

    }

  console.log("Registrándose en la plataforma...");

  if (rol === "guia") {
    navigate("/guia/dashboard");
  }
  else if (rol === "turista") {
    navigate("/turista/dashboard");
  }
  else if (rol === "jefe") {
    navigate("/jefe/dashboard");
  }
  else if (rol === "admin") {
    navigate("/admin/dashboard");
  }
  
};
  return (
    <Layout>
      <div className="register-container">
          <form className="register-form" onSubmit={handleSubmit}>
              <h2> </h2>  
              <Alerts mensaje={mensajeError} />         
              <Input 
              label="Usuario"
              placeholder="Escribe tu usuario" 
              value={usuario} 
               onChange={(e) => {setUsuario(e.target.value);
                setMensajeError(""); // Limpia el error al escribir
              
              } }
               /> 
              
              <Input 
              label="Password"
              type="password"
              placeholder="Ingresa tu contraseña"
              value={password}                 
              /* Garantizar que la contraseña tenga 8 o más caracteres */
              minLength={8} 
              onChange={(e) => {setPassword(e.target.value);
                setMensajeError(""); // Limpia el error al escribir
              
              } }
              
               />

              <Select
              label="Seleccionar rol (Prueba)"
              value={rol}
              onChange={(e) => setRol(e.target.value)}
              options={opcionesRoles}
              required
              />

              <Button type="submit" texto="Enter" />
        
        </form>
      </div>  
   </Layout> 
  );
}

export default Register;