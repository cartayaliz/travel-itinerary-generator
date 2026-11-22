import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import Input from "../components/Input";
import Layout from "../components/Layout";
import Alerts from "../components/Alerts";
import "./Login.css";

// Función auxiliar para leer los datos internos (claims) del token JWT
function obtenerRolDesdeToken(token) {
  try {
    const payloadBase64 = token.split(".")[1];
    const payloadJson = JSON.parse(atob(payloadBase64));
    // .NET suele guardar el rol como "role" o bajo el esquema estándar de Microsoft
    const rol =
      payloadJson.role ||
      payloadJson["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    return rol ? rol.toLowerCase() : null;
  } catch (error) {
    console.error("No se pudo decodificar el token:", error);
    return null;
  }
}

async function hashearPasswordSHA256(password) {
  const encoder = new TextEncoder();
  const data = encoder.encode(password);
  const hashBuffer = await crypto.subtle.digest("SHA-256", data);
  const hashArray = Array.from(new Uint8Array(hashBuffer));
  // Devuelve siempre el mismo string hexadecimal en minúsculas
  return hashArray.map((b) => b.toString(16).padStart(2, "0")).join("");
}

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
    // Hasheamos con SHA-256 antes de enviarla por la red
    const passwordCifrada = await hashearPasswordSHA256(password);
  try {
      const response = await fetch("http://localhost:5064/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          UserName: usuario,
          Password: passwordCifrada,
        }),
      });

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Usuario o contraseña incorrectos.");
      }

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
               onChange={(e) => {setPassword(e.target.value);
                setMensajeError(""); // Limpia el error al escribir
              
              } }
               />

              <Button type="submit" texto={cargando ? "Entrando..." : "Entrar"} disabled={cargando} />
        
        </form>
      </div>  
   </Layout> 
  );
}

export default Login;