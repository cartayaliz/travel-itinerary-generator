
import { useState } from 'react';
import './App.css'

// src/App.jsx
function App() {
  const [modo, setModo] = useState("light");
  const colors = modo === "light"
  ? { background: "#ffffff", text: "#000000" }
  : { background: "#000000", text: "#ffffff" };

  function ChangeMode(){
    setModo(modo === "light" ? "dark" : "light");
  }

  return (
    <div className="app">
      <header>
        <h1>Travel Itinerary Generator</h1>
        <p>Sistema de gestión de itinerarios turísticos</p>
        
      </header>

      <main style={{ backgroundColor: colors.background, color: colors.text }}>
                <button onClick={ChangeMode}>
                  Change mode
                </button>
                
       
      </main>

      <footer>
        <p> by BugSlayer team</p>
      </footer>
      
    </div>
  );
}


export default App;
