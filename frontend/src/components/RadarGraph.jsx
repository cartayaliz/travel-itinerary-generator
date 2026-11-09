import { Radar, RadarChart, PolarGrid, PolarAngleAxis, PolarRadiusAxis, ResponsiveContainer, Tooltip } from 'recharts';

function RadarGraph({data, title, xAxisKey, keys, colors, leyenda})
{
    return(
        <div>
        <div style={{ width: '100%', height: 400, marginTop: '2rem' }}>
            <h3 style={{ textAlign: 'center', marginBottom: '1rem', color: '#333' }}>{title}</h3>
            
            <ResponsiveContainer width="100%" height="100%">
                <RadarChart cx="50%" cy="50%" outerRadius="80%" data={data}>
                <PolarGrid />
                <PolarAngleAxis dataKey={xAxisKey} />
                <PolarRadiusAxis />
                <Tooltip />
              
                
                {/* 2. Hacemos un map sobre las 'keys' para crear un Radar por cada dato */}
                {keys.map((key, index) => (
                    <Radar 
                    key={key.dataKey}
                    name={key.name} 
                    dataKey={key.dataKey} 
                    stroke={colors[index]} 
                    fill={colors[index]} 
                    fillOpacity={0.5} 
                    />
                ))}
                </RadarChart>
            </ResponsiveContainer>
            </div>

            {/* 1. LEYENDA PERSONALIZADA (Cuadraditos de colores) */}
            <div style={{ display: "flex", justifyContent: "center", gap: "1.5rem", marginTop: "1rem", marginBottom: "1.5rem" }}>
                {keys.map((key, index) => (
                <div key={key.dataKey} style={{ display: "flex", alignItems: "center", gap: "8px" }}>
                    <span 
                    style={{ 
                        display: "inline-block", 
                        width: "16px", 
                        height: "16px", 
                        backgroundColor: colors[index], 
                        borderRadius: "4px" 
                    }}
                    ></span>
                    <span style={{ fontSize: "0.95rem", color: "#444", fontWeight: "500" }}>
                    {key.name}
                    </span>
                </div>
                ))}
            </div>

            {leyenda && (
                <div style={{ marginTop: "1rem", fontSize: "0.9rem", color: "#555", backgroundColor: "#f9f9f9", padding: "1rem", borderRadius: "8px" }}>
                <p style={{ fontWeight: "bold", marginBottom: "0.5rem" }}>¿Cómo leer este gráfico?</p>
                <ul style={{ paddingLeft: "1.5rem", margin: 0 }}>
                    {leyenda.map((texto, index) => (
                    <li key={index} dangerouslySetInnerHTML={{ __html: texto }}></li>
                    ))}
                </ul>
                </div>
            )}
        </div>
    );
}
export default RadarGraph;