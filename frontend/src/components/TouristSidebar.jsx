function TouristSidebar({ activeTab, setActiveTab }) {
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
        </div>
);
}

export default TouristSidebar;