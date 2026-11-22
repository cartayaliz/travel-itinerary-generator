import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button";
import Input from "../components/Input";
import Select from "../components/Select";
import Layout from "../components/Layout";
import Alerts from "../components/Alerts";
import { registrarTurista } from "../services/authService";
import "./Register.css";

function Register() {
  const [mensajeError, setMensajeError] = useState("");
  const [userName, setUserName] = useState("");
  const [nombre, setNombre] = useState("");
  const [edad, setEdad] = useState("");
  const [password, setPassword] = useState("");
  const [cargando, setCargando] = useState(false);

    const navigate = useNavigate();  

    const handleSubmit = async (e) => { 
      e.preventDefault(); 
      setMensajeError("");

    // 1. Validar que tenga al menos una letra y un número
    const tieneLetraYNumero = /^(?=.*[A-Za-z])(?=.*\d).+$/;

    if (!userName || !nombre || !edad || !password ) {
      setMensajeError("Por favor, llena todos los campos");
      return;
    }
    if (!tieneLetraYNumero.test(password)) {
    setMensajeError("La contraseña debe contener al menos una letra y un número");
    return;

    }

    setCargando(true);

  try {
      await registrarTurista({ userName, nombre, edad, password });
      navigate("/login");
    } catch (error) {
      console.error("Error en el registro:", error);
      setMensajeError(error.message || "No se pudo conectar con el servidor.");
    } finally {
      setCargando(false);
    }
  
};
  return (
    <Layout>
      <div className="register-container">
          <form className="register-form" onSubmit={handleSubmit}>
              <h2>Registro de Turista </h2>  
              <Alerts mensaje={mensajeError} /> 

              <Input
                label="Nombre de usuario"
                placeholder="Ej. maria_turista"
                value={userName}
                onChange={(e) => {
                  setUserName(e.target.value);
                  setMensajeError("");
                }}
              />

              <Input
                label="Nombre completo"
                placeholder="Escribe tu nombre y apellido"
                value={nombre}
                onChange={(e) => {
                  setNombre(e.target.value);
                  setMensajeError("");
                }}
              />

            <Input
              label="Edad"
              type="number"
              placeholder="Ingresa tu edad"
              value={edad}
              onChange={(e) => {
                setEdad(e.target.value);
                setMensajeError("");
              }}
            />

            <Input
              label="Contraseña"
              type="password"
              placeholder="Mínimo 8 caracteres, letra y número"
              value={password}
              minLength={8}
              onChange={(e) => {
                setPassword(e.target.value);
                setMensajeError("");
              }}
            />

            <Button
              type="submit"
              texto={cargando ? "Registrando..." : "Registrarse"}
              disabled={cargando}
            />
        
        </form>
      </div>  
   </Layout> 
  );
}

export default Register;