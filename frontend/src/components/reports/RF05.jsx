import RadarGraph from "../RadarGraph";
import "../../views/Reports.css";

function RF05()
{
    const data = [
    { id: 1, package: "Museum Visit", totalActivities: 3, category: "Culture" , effort: "Low" },
    { id: 2, package: "Old Havana Tour", totalActivities: 5, category: "Culture", effort: "Medium"},
    { id: 3, package: "Malecon Walk", totalActivities: 2, category: "Tour", effort: "Low" },
    ];
    const radarData = [
    { package: "Havana Colonial", Low: 2, Medium: 4, High: 2 },
    { package: "Varadero Beach", Low: 5, Medium: 1, High: 0 },
    { package: "Viñales Trip", Low: 0, Medium: 2, High: 3 },
    ];

    // 1.datos a usar en el gráfico
    const radarKeys = [
    { dataKey: "Low", name: "Low Effort" },
    { dataKey: "Medium", name: "Medium Effort" },
    { dataKey: "High", name: "High Effort" }
    ];

    // 2. Definimos los colores (en el mismo orden que las keys)
    const radarColors = ["#15803d", "#f8ec49", "#b91c1c"];

     // 3. Definimos la leyenda
    const radarLeyenda = [
    "Cada punto del gráfico representa un paquete turístico.",
    "Las áreas más grandes hacia el <strong>verde</strong> indican que el paquete tiene más actividades de bajo esfuerzo.",
     "Las áreas hacia el <strong>amarillo</strong> indican actividades de <strong>esfuerzo medio</strong>.",
    "Las áreas hacia el <strong>rojo</strong> indican que el paquete exige más esfuerzo físico.",
    "Un gráfico más equilibrado significa que el itinerario tiene una buena variedad de niveles de esfuerzo."
    ];
    return(
        <div className="report-card">
            <h2>RF-05: Inter-package comparison</h2>
            <p style={{ color: "#666", marginBottom: "1rem" }}>
                Comparison of category balance and physical effort across different packages.
            </p>
            <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left" }}>
                <thead>
                    <tr style={{ borderBottom: "2px solid #ddd" }}>
                            <th style={{padding: "10px"}}>ID</th>
                            <th style={{padding: "10px"}}>Package </th>
                            <th style={{padding: "10px"}}>Total Activities </th>
                            <th style={{padding: "10px"}}> Category </th>
                            <th style={{padding: "10px"}}> Effort</th>
                    </tr>
                </thead>    
                <tbody>
                    {data.map((row) => (
                    <tr key={row.id} style={{ borderBottom: "1px solid #eee" }}>
                    <td style={{ padding: "10px" }}>{row.id}</td>
                    <td style={{ padding: "10px" }}>{row.package}</td>
                    <td style={{ padding: "10px" }}>{row.totalActivities}</td>
                    <td style={{ padding: "10px" }}>{row.category}</td>
                    <td style={{ padding: "10px" }}>
                        <span className={`effort-badge ${
                        row.effort === 'High' ? 'effort-high' : 
                        row.effort === 'Medium' ? 'effort-medium' : 
                        'effort-low'
                        }`}>
                        {row.effort}
                        </span>
                    </td>
                    </tr>
                ))}
                </tbody>
            </table>
            <RadarGraph 
                data={radarData} 
                title="Package Balance: Physical Effort Profile" 
                xAxisKey="package"
                keys={radarKeys}
                colors={radarColors}
                leyenda={radarLeyenda}
            />
        </div>
    );
}
export default RF05;