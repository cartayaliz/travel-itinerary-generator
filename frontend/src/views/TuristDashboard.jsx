import { useState } from "react";
import Review from "../components/Review";
import Request from "../components/Request";
import "./TuristDashboard.css";

function TuristDashboard() {
  const [activeTab, setActiveTab] = useState("itinerary");

  return (
    <div className="turist-layout">
      <aside className="sidebar">
      <h2>Tourist Menu</h2>
          <ul>
    <li 
      className={activeTab === "itinerary" ? "active" : ""} 
      onClick={() => setActiveTab("itinerary")}
    >
      My Itinerary
    </li>
    <li 
      className={activeTab === "reviews" ? "active" : ""} 
      onClick={() => setActiveTab("reviews")}
    >
      My Reviews
    </li>
    <li 
      className={activeTab === "requests" ? "active" : ""} 
      onClick={() => setActiveTab("requests")}
    >
      Requests
    </li>
  </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>My Trip: Havana Colonial</h1>
          <p>Assigned Guide: René Díaz</p>
        </header>
        
        {activeTab === "itinerary" && ( <section className="itinerary-section">
          <h2>Today's Activities</h2>
            {/*ejemplo de actividad, luego es de la base de datos*/}
          <div className="list-activities">
            <div className="activity-card">
               <div className="hour">09:00</div>
               <div className="details">
                 <h3>Walk along the Malecón</h3>
                 <span className="effort low">Effort: Low</span>
               </div>
            </div>
          </div>  
        </section> )}
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