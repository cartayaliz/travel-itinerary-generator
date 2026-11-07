import "./Alerts.css";

function Alerts({ mensaje , tipo = "error" }) {
  if (!mensaje) return null; // Si no hay mensaje, no dibuja nada
   const claseCSS = tipo === "exito" ? "alert-exito" : "alert-error";
  return <div className={`alert-mensaje ${claseCSS}`}>{mensaje}</div>;
 
}
export default Alerts;