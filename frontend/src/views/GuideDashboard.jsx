import { useState, useEffect} from "react";
import { useNavigate, useLocation} from "react-router-dom";
import GuideSidebar from "../components/GuideSidebar";
import MyItinerariesTab from "../components/MyItinerariesTab";
import CreateActivityTab from "../components/CreateActivityTab";
import ProfileTab from "../components/ProfileTab";
import "./GuideDashboard.css";

function GuideDashboard() {
 
  const [activeTab, setActiveTab] = useState("My Itineraries");
  const location = useLocation(); 

  useEffect(() => {
    const cambiarTab = () => {
      if (location.state?.tab) {
        setActiveTab(location.state.tab);
      }
    };
    cambiarTab();
  }, [location]);

  return (
    <div className="app-layout">
      <GuideSidebar />

      <main className="main-content">
        <header className="main-header">
          <h1>Guide Dashboard</h1>
          <p>Welcome back</p>
        </header>
        { activeTab === "My Itineraries" && <MyItinerariesTab /> }
        { activeTab === "Create Activity" && <CreateActivityTab /> }
        { activeTab === "Profile" && <ProfileTab /> }
        
      </main>
    </div>
  );
}

export default GuideDashboard;