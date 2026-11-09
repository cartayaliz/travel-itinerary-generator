import { useNavigate, useLocation } from "react-router-dom";

function GuideSidebar() {
  const navigate = useNavigate();
  const location = useLocation();

  // 1. Verificamos si estamos en el Dashboard
  const isDashboard = location.pathname === "/guia/dashboard";
  
  // 2. Leemos qué pestaña estamos pidiendo (si no hay, es "My Itineraries")
  const currentTab = location.state?.tab || "My Itineraries";

  return (
    <aside className="sidebar">
      <h2>Guide Menu</h2>
      <ul style={{ listStyle: "none", padding: 0 }}>
        
        {/* MY ITINERARIES */}
        <li style={{ marginBottom: "10px" }}>
          <span
            onClick={() => navigate("/guia/dashboard", { state: { tab: "My Itineraries" } })}
            className={isDashboard && currentTab === "My Itineraries" ? "active-link" : ""}
            style={{ cursor: "pointer", display: "block", padding: "10px" }}
          >
            My Itineraries
          </span>
        </li>
        {/* CREATE ACTIVITY */}
        <li style={{ marginBottom: "10px" }}>
          <span 
            onClick={() => navigate("/guia/dashboard", { state: { tab: "Create Activity" } })}
            className={isDashboard && currentTab === "Create Activity" ? "active-link" : ""}
            style={{ cursor: "pointer", display: "block", padding: "10px" }}
          >
            Create Activity
          </span>
        </li>

        {/* PROFILE */}
        <li style={{ marginBottom: "10px" }}>
          <span 
            onClick={() => navigate("/guia/dashboard", { state: { tab: "Profile" } })}
            className={isDashboard && currentTab === "Profile" ? "active-link" : ""}
            style={{ cursor: "pointer", display: "block", padding: "10px" }}
          >
            Profile
          </span>
        </li>
        {/* OTHER GUIDES */}
        <li style={{ marginBottom: "10px" }}>
          <span
            onClick={() => navigate("/guia/other-guides")}
            className={location.pathname === "/guia/other-guides" ? "active-link" : ""}
            style={{ cursor: "pointer", display: "block", padding: "10px" }}
          >
            Other Guides' Itineraries
          </span>
        </li>

      </ul>
    </aside>
  );
}

export default GuideSidebar;