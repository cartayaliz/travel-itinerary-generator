import { Routes, Route } from "react-router-dom";
import Home from "../views/Home";
import Login from "../views/Login";
import Dashboard from "../views/Dashboard";
import TuristDashboard from '../views/TuristDashboard';
import Reports from "../views/Reports";
import GuideDashboard from "../views/GuideDashboard";
import ItineraryDetail from "../views/ItineraryDetail";
import Register from "../views/Register";
import OtherGuides from "../views/GuideTuristPackage";

function AppRouter() {
  return (
    <Routes>
      {/* Cuando la ruta sea /login, muestra el componente Login */}
      <Route path="/login" element={<Login />} />
      <Route path="/" element={<Navigate to="/login" />} />
     <Route path="/guia/dashboard" element={<GuideDashboard />} />
      {/* Si la ruta es /, redirige automáticamente a Home */}
      <Route path="/" element={<Home/>} />

      <Route path="/register" element={<Register/>} />

     <Route path="/guia/dashboard" element={<GuiaDashboard />} />
     <Route path="/turista/dashboard" element={<TuristDashboard />} />
     <Route path="/admin/dashboard" element={<h1>Bienvenido al Panel de Administrador (En construcción)</h1> }/>
     <Route path="/jefe/dashboard" element={<h1>Bienvenido al Panel de Jefe (En construcción)</h1> }/>
     <Route path="/reports" element={<Reports />} />
     <Route path="/guia/itinerary/:id" element={<ItineraryDetail />} />
     <Route path="guia/other-guides" element={<OtherGuides />} />
    </Routes>
  );
}

export default AppRouter;