import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import Input from "../components/Input";
import Select from "../components/Select";
import Layout from "../components/Layout";
import "./Login.css";


function Login() {
    const [error, setError] = useState("");
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

    setError("");
    
    // 1. Validar que tenga al menos una letra y un número
    const tieneLetraYNumero = /^(?=.*[A-Za-z])(?=.*\d).+$/;

    if (!usuario || !password) {
      setError("Por favor, llena todos los campos");
      return;
    }
    if (!tieneLetraYNumero.test(password)) {
    setError("La contraseña debe contener al menos una letra y un número");
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
    <Layout>
      <div className="login-container">
          <form className="login-form" onSubmit={handleSubmit}>
              <h2>Iniciar sesión</h2>    
              {/* Si 'error' tiene texto, muestra este div rojo */}
              {error && <div className="error-mensaje">{error}</div>}         
              <Input 
              label="Usuario"
              placeholder="Escribe tu usuario" 
              value={usuario} 
              onChange={(e) => setUsuario(e.target.value)} 
               /> 
              
              <Input 
              label="Contraseña"
              type="password"
              placeholder="Ingresa tu contraseña"
              value={password}                 
              /* Garantizar que la contraseña tenga 8 o más caracteres */
              minLength={8} 
              onChange={(e) => setPassword(e.target.value)} 
              /* No permitir campo vacío*/
               />

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
   </Layout> 
  );
}

export default Login;