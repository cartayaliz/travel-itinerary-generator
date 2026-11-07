import Layout from "../components/Layout"; 

function Home() {
  return (
    <Layout>
      <div style={{ padding: "2rem", textAlign: "center" }}>
        <h2>Bienvenido a Travel Itinerary Generator</h2>
        <p>Por favor, inicia sesión o regístrate para continuar.</p>
      </div>
    </Layout>
  );
}

export default Home;