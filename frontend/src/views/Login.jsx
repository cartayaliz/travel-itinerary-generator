import { useState } from "react";
import "./Login.css";

function Login() {
    const [usuario, setUsuario] = useState("");
    const [password, setPassword] = useState("");
    const handleSubmit = (e) => { e.preventDefault();

    // 1. Validar que tenga al menos una letra y un número
    const tieneLetraYNumero = /^(?=.*[A-Za-z])(?=.*\d).+$/;

    if (!tieneLetraYNumero.test(password)) {
    alert("La contraseña debe contener al menos una letra y un número");
    return;
    }

  console.log("Iniciando sesión...");

};
  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleSubmit}>
        <h2>Iniciar sesión</h2>
        
        <div className="form-group">
          <label>Usuario:</label>
          <input type="text" placeholder="Escribe tu usuario" value={usuario} onChange={(e) => setUsuario(e.target.value)} required />  
        </div>

        <div className="form-group">
          <label>Contraseña:</label>
          <input type="password" placeholder="Ingresa tu contraseña" value={password} minLength={8} onChange={(e) => setPassword(e.target.value)} required />
        </div>

        <button type="submit" className="btn-primary">Entrar</button>
      </form>
    </div>
  );
}

export default Login;