import { Routes, Route } from "react-router-dom";
import Home from "../views/Home";
import Login from "../views/Login";
import TuristDashboard from '../views/TuristDashboard';
import Reports from "../views/Reports";
import GuideDashboard from "../views/GuideDashboard";
import ItineraryDetail from "../views/ItineraryDetail";
import Register from "../views/Register";

function AppRouter() {
  return (
    <Routes>
      {/* Si la ruta es /, redirige automáticamente a Home */}
      <Route path="/" element={<Home/>} />

      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register/>} />

     <Route path="/guia/dashboard" element={<GuideDashboard />} />
     <Route path="/turista/dashboard" element={<TuristDashboard />} />
     <Route path="/admin/dashboard" element={<h1>Bienvenido al Panel de Administrador (En construcción)</h1> }/>
     <Route path="/jefe/dashboard" element={<h1>Bienvenido al Panel de Jefe (En construcción)</h1> }/>
     <Route path="/reports" element={<Reports />} />
     <Route path="/guia/itinerary/:id" element={< ItineraryDetail />} />
    </Routes>
  );
}

export default AppRouter;