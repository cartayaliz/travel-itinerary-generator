import "./GuideDashboard.css";

import Button from "../components/Button"; 

function GuideDashboard() {
  /*esto es de prueba */
  const myItineraries = [
    { id: 1, name: "Havana Colonial", status: "Approved", activities: 5 },
    { id: 2, name: "Varadero Beach", status: "In Review", activities: 4 },
  ];

  return (
    <div className="app-layout">
      <aside className="sidebar">
      <h2>Guide Menu</h2>
        <ul>
          <li className="active">My Itineraries</li>
          <li>Create Activity</li>
          <li>Profile</li>
        </ul>
      </aside>

      <main className="main-content">
        <header className="main-header">
          <h1>Guide Dashboard</h1>
          <p>Welcome back, René Díaz</p>
        </header>

        <section className="guide-section">
          <div className="section-header">
            <h2>My Itineraries</h2>
            <Button texto="+ Generate New Itinerary" onClick={() => alert("Pop up todavia")} />
          </div>

          <div className="itinerary-list">
            {myItineraries.map((itinerary) => (
              <div key={itinerary.id} className="itinerary-card">
                <div className="itinerary-info">
                  <h3>{itinerary.name}</h3>
                  <p>Activities: {itinerary.activities}</p>
                </div>
              </div>
            ))}
          </div>
        </section>
      </main>
    </div>
  );
}

export default GuideDashboard;