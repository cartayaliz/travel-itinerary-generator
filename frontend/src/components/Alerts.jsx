function Alerta({ mensaje }) {
  if (!mensaje) return null; // Si no hay mensaje, no dibuja nada
  return <div className="error-mensaje">{mensaje}</div>;
}
export default Alerta;