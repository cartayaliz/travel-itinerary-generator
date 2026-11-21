import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

function BarGraph({ data, title }) {
  return (
    <div style={{ width: '100%', height: 300, marginTop: '2rem' }}>
      <h3 style={{ textAlign: 'center', marginBottom: '1rem', color: '#333' }}>{title}</h3>
      <ResponsiveContainer width="100%" height="100%">
        <BarChart data={data}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="name" />
          <YAxis />
          <Tooltip />
          <Legend />
          {/* Aquí definimos la barra  */}
          <Bar dataKey="total" fill="#764ba2" name="Cantidad" />
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}

export default BarGraph;