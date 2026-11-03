import { Routes, Route } from "react-router-dom";
import Home from "../views/Home";
import Login from "../views/Login";
import Register from "../views/Register";
import GuiaDashboard from "../views/GuiaDashboard";
import TuristDashboard from "../views/TuristDashboard";

function AppRouter() {
  return (
    <Routes>
      {/* Cuando la ruta sea /login, muestra el componente Login */}
      <Route path="/login" element={<Login />} />
      
      {/* Si la ruta es /, redirige automáticamente a Home */}
      <Route path="/" element={<Home/>} />

      <Route path="/register" element={<Register/>} />

     <Route path="/guia/dashboard" element={<GuiaDashboard />} />
     <Route path="/turista/dashboard" element={<TuristDashboard />} />
     <Route path="/admin/dashboard" element={<h1>Bienvenido al Panel de Administrador (En construcción)</h1> }/>
     <Route path="/jefe/dashboard" element={<h1>Bienvenido al Panel de Jefe (En construcción)</h1> }/>
    </Routes>
  );
}

export default AppRouter;