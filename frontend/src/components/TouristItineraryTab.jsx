function TouristItineraryTab(){

    return(
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

    );
}
export default TouristItineraryTab;