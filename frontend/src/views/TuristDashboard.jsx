import { useState } from "react";
import Review from "../components/Review";
import Button from "../components/Button";
import "./TuristDashboard.css";

function TuristDashboard() {
  const [showReview, setShowReview] = useState(false);

  return (
    <div className="turist-layout">
      <aside className="sidebar">
      <h2>Tourist Menu</h2>
        <ul>
          <li className="active">My Itinerary</li>
          <li>My Reviews</li>
          <li>Requests</li>
        </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>My Trip: Havana Colonial</h1>
          <p>Assigned Guide: René Díaz</p>

          <div style={{ marginTop: "2rem" }}>
            <Button 
              texto={showReview ? "Your opinion helps us improve" : "Leave a Review"} 
              onClick={() => setShowReview(true)} 
            />
            {showReview &&  <Review onCancel={() => setShowReview(false)}/>}
          </div>
        </header>
        
        <section className="itinerary-section">
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
     
          
        </section>
      </main>
    </div>
  );
}

export default TuristDashboard;