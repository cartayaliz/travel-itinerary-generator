import { Link } from "react-router-dom";
import "./Header.css";

function Header()
{
    return(
    <header className="header">
        <div className="header-brand">
            <h1>Travel Itinerary Generator</h1>
            <p>Sistema de gestión de itinerarios turísticos</p>
        </div>
       <nav className="header-links">
            <Link to="/login"className="nav-link"> Iniciar Sesión</Link>
             <Link to="/register"className="nav-link"> Registrarse</Link>
       </nav>
    </header>
    );
}
export default Header;