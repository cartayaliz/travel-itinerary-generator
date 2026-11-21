import BarGraph from "../BarGraph";
import "../../views/Reports.css";


function RF02()
{
    const activitiesData = [
    { id: 1, name: "Malecón Walk", category: "Tour", effort: "Low", timesUsed: 10 },
    { id: 2, name: "Museum Visit", category: "Culture", effort: "Medium", timesUsed: 5 },
    { id: 3, name: "Mountain Hiking", category: "Nature", effort: "High", timesUsed: 2 },
    { id: 4, name: "Old Havana Tour", category: "Culture", effort: "Low", timesUsed: 8 },
    ];
    const chartData = activitiesData.map(act => ({
    name: act.name,
    total: act.timesUsed
    }));

    return(
       
         <div className="report-card">
            <h2>RF-02: Most Used Activities</h2>
            {/* Tabla de Actividades */}
            <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", marginTop: "1rem", textAlign: "left" }}>
                <thead>
                <tr style={{ borderBottom: "2px solid #ddd" }}>
                    <th style={{ padding: "10px" }}>ID</th>
                    <th style={{ padding: "10px" }}>Activity Name</th>
                    <th style={{ padding: "10px" }}>Category</th>
                    <th style={{ padding: "10px" }}>Effort Level</th>
                    <th style={{ padding: "10px" }}>Times Used</th>
                </tr>
                </thead>
                <tbody>
                {activitiesData.map((row) => (
                    <tr key={row.id} style={{ borderBottom: "1px solid #eee" }}>
                    <td style={{ padding: "10px" }}>{row.id}</td>
                    <td style={{ padding: "10px" }}>{row.name}</td>
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
                    <td style={{ padding: "10px" }}>{row.timesUsed}</td>
                    </tr>
                ))}
                </tbody>
            </table>

            {/* Gráfico de barras */}
            <div style={{ marginTop: "2rem", height: "300px" }}>
                <BarGraph 
                data={chartData} 
                title="Top Activities by Usage" 
                />
            </div>
         </div>
   
    );
}
export default RF02;