import { Routes, Route, Navigate } from "react-router-dom";
import Login from "../views/Login";
import Dashboard from "../views/Dashboard";
import TuristDashboard from '../views/TuristDashboard';
import Reports from "../views/Reports";
function AppRouter() {
  return (
    <Routes>
      {/* Cuando la ruta sea /login, muestra el componente Login */}
      <Route path="/login" element={<Login />} />
      
      {/* Si la ruta es /, redirige automáticamente al login */}
      <Route path="/" element={<Navigate to="/login" />} />
     <Route path="/guia/dashboard" element={<Dashboard />} />
     <Route path="/turista/dashboard" element={<TuristDashboard />} />
     <Route path="/admin/dashboard" element={<h1>Bienvenido al Panel de Administrador (En construcción)</h1> }/>
     <Route path="/jefe/dashboard" element={<h1>Bienvenido al Panel de Jefe (En construcción)</h1> }/>
     <Route path="/reports" element={<Reports />} />
    </Routes>
  );
}

export default AppRouter;