import { Routes, Route, Navigate } from "react-router-dom";
import Login from "../views/Login";

function AppRouter() {
  return (
    <Routes>
      {/* Cuando la ruta sea /login, muestra el componente Login */}
      <Route path="/login" element={<Login />} />
      
      {/* Si la ruta es /, redirige automáticamente al login */}
      <Route path="/" element={<Navigate to="/login" />} />
    </Routes>
  );
}

export default AppRouter;