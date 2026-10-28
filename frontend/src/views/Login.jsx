import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import Input from "../components/Input";
import Select from "../components/Select";
import "./Login.css";


function Login() {
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

    // 1. Validar que tenga al menos una letra y un número
    const tieneLetraYNumero = /^(?=.*[A-Za-z])(?=.*\d).+$/;

    if (!tieneLetraYNumero.test(password)) {
    alert("La contraseña debe contener al menos una letra y un número");
    return;

    }

  console.log("Iniciando sesión...");

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
    <div className="app">
      <header>
        <h1>Travel Itinerary Generator</h1>
        <p>Sistema de gestión de itinerarios turísticos</p>
        
      </header>

      <main className="login-main">
                <div className="login-container">
                <form className="login-form" onSubmit={handleSubmit}>
                    <h2>Iniciar sesión</h2>
                    
                    <Input 
                    label="Usuario"
                    placeholder="Escribe tu usuario" 
                    value={usuario} 
                    onChange={(e) => setUsuario(e.target.value)} 
                    required /> 
                    
                    <Input 
                    label="Contraseña"
                    type="password"
                    placeholder="Ingresa tu contraseña"
                     value={password}                 
                    // Garantizar que la contraseña tenga 8 o más caracteres
                    minLength={8} 
                    onChange={(e) => setPassword(e.target.value)} 
                    // No permitir campo vacío
                    required />

                   <Select
                    label="Seleccionar rol (Prueba)"
                    value={rol}
                    onChange={(e) => setRol(e.target.value)}
                    options={opcionesRoles}
                    required
                    />

                   <Button type="submit" texto="Entrar" />
              
              </form>
            </div>   
      </main>

      <footer>
        
        <p> by BugSlayer team</p>
      </footer>
      
    </div>
    
  );
}

export default Login;