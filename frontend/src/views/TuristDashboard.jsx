import { useState } from "react";
import TouristItineraryTab from "../components/TouristItineraryTab";
import Review from "../components/Review";
import Request from "../components/Request";
import TouristSidebar from "../components/TouristSidebar";
import "./TuristDashboard.css";

function TuristDashboard() {
  const [activeTab, setActiveTab] = useState("itinerary");

  return (
    <div className="turist-layout">
     <TouristSidebar activeTab={activeTab} setActiveTab={setActiveTab} />
    
      <main className="main-content">
        <header className="main-header">
          <h1>My Trip: Havana Colonial</h1>
          <p>Assigned Guide: René Díaz</p>
        </header>
        
        {activeTab === "itinerary" && < TouristItineraryTab /> }
        {activeTab === "reviews" && (
            <section>
              <h2>Rate your experience</h2>
              <Review onCancel={() => setActiveTab("itinerary")} /> 
            </section>
        )}
        {activeTab === "requests" && (
          <section>
            <h2>Request a modification</h2>
            <Request onCancel={() => setActiveTab("itinerary")} />
          </section>
        )}

      </main>
    </div>
  );
}

export default TuristDashboard;