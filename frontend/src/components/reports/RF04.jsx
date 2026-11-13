import BarGraph from "../BarGraph";
import "../../views/Reports.css";

function RF04()
{
    const data = [
    { id: 1, activity: "Museum Visit" , effort: "Low", avgRating: 4.8, totalReviews: 15 },
    { id: 2, activity: "Old Havana Tour", effort: "Medium", avgRating: 2.5, totalReviews: 10 },
    { id: 3, activity: "Malecon Walk", effort: "Low", avgRating: 4.3, totalReviews: 130 },
    ];
    // Gráfico mostrando la valoración
    const chartData = data.map(item => ({
        name: item.activity,
        total: item.avgRating
    }));
    return(
            <div className="report-card">
                <h2> RF-04: Satisfaction and Physical Effort</h2>
                <p style={{ color: "#666", marginBottom: "1rem" }}>
                    Turist ratings compared against the required physical effort.
                </p>
            {/* Tabla */}
            <table className="reports-table" style={{ width: "100%", borderCollapse: "collapse", textAlign: "left" }}>
                <thead>
                    <tr style={{ borderBottom: "2px solid #ddd" }}>
                        <th style={{ padding: "10px" }}>ID</th>
                        <th style={{ padding: "10px" }}>Activity</th>
                        <th style={{ padding: "10px" }}>Effort</th>
                        <th style={{ padding: "10px" }}>Rating</th>
                        <th style={{ padding: "10px" }}>Total Reviews</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map((row) => (
                        <tr key={row.id} style={{ borderBottom: "1px solid #eee" }}>
                        <td style={{ padding: "10px" }}>{row.id}</td>
                        <td style={{ padding: "10px" }}>{row.activity}</td>
                        <td style={{ padding: "10px" }}>
                            <span className={`effort-badge ${
                                row.effort === 'High' ? 'effort-high' : 
                                row.effort === 'Medium' ? 'effort-medium' : 
                                'effort-low'
                                }`}>
                                {row.effort}
                            </span>
                        </td>
                        <td style={{ padding: "10px" }}>⭐ {row.avgRating} / 5</td>
                        <td style={{ padding: "10px" }}>{row.totalReviews}</td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/* Gráfico de barras */}
            <div style={{ marginTop: "2rem", height: "300px" }}>
                <BarGraph data={chartData} title="Average Rating by Activity" />
            </div>
        </div>
    );
}
export default RF04;