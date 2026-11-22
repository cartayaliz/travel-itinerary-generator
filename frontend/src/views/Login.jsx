import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import Input from "../components/Input";
import Select from "../components/Select";
import Layout from "../components/Layout";
import Alerts from "../components/Alerts";
import "./Login.css";
import { loginUsuario, obtenerRolDesdeToken } from "../services/authService";


function Login() {
    const [mensajeError, setMensajeError] = useState("");
    const [usuario, setUsuario] = useState("");
    const [password, setPassword] = useState("");
    const [cargando, setCargando] = useState(false);

    const navigate = useNavigate();  

    const handleSubmit = async(e) => { 
      e.preventDefault();
      setMensajeError(""); 

    
    // Validar que tenga todos los campos

    if (!usuario || !password) {
      setMensajeError("Por favor, llena todos los campos");
      return;
    }

    setCargando(true);

  try {
      const data = await loginUsuario(usuario, password);

      // Guardar credenciales de sesión
      localStorage.setItem("token", data.token);
      localStorage.setItem("usuarioID", data.usuarioID);
      localStorage.setItem("userName", data.userName);

      // Extraer el rol que asignó el backend en el token JWT
      const rolUsuario = obtenerRolDesdeToken(data.token);

      if (rolUsuario?.includes("guia")) {
        navigate("/guia/dashboard");
      } else if (rolUsuario?.includes("turista")) {
        navigate("/turista/dashboard");
      } else if (rolUsuario?.includes("jefe")) {
        navigate("/jefe/dashboard");
      } else {
        setMensajeError("No se pudo determinar el rol del usuario en el token.");
      }
    } catch (error) {
      console.error("Error de autenticación:", error);
      setMensajeError(error.message || "No se pudo conectar con el servidor.");
    } finally {
      setCargando(false);
    }
};

  return (
    <Layout>
      <div className="login-container">
          <form className="login-form" onSubmit={handleSubmit}>
              <h2>Iniciar sesión</h2>    
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
              label="Contraseña"
              type="password"
              placeholder="Ingresa tu contraseña"
              value={password}                 
               onChange={(e) => 
                {setPassword(e.target.value);
                setMensajeError(""); // Limpia el error al escribir
              
              } }
               />

              <Button type="submit" 
              texto={cargando ? "Entrando..." : "Entrar"} 
              disabled={cargando} 
              />
        
        </form>
      </div>  
   </Layout> 
  );
}

export default Login;