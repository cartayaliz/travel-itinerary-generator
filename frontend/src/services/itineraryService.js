const API_URL = "http://localhost:5064/api";

/*
 * Obtiene todos los itinerarios del backend.
 * @returns {Promise<Array>} Lista de itinerarios
 */
export async function obtenerMisItinerarios() {
  const response = await fetch(`${API_URL}/Itinerarios`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Error al obtener itinerarios: ${response.status}`);
  }

  return await response.json();
}

/*
 * Crea un nuevo itinerario en el backend.
 * @param {Object} data - Datos del itinerario según el DTO
 * @returns {Promise<Object>} Respuesta del servidor
 */
export async function crearItinerario(data) {
  const response = await fetch(`${API_URL}/Itinerarios`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error(`Error al crear itinerario: ${response.status}`);
  }

  return await response.json();
}

/*
 * Obtiene un itinerario por su ID (útil para la vista de detalles).
 * @param {number} id - ID del itinerario
 * @returns {Promise<Object>} Datos del itinerario
 */
export async function obtenerItinerarioPorId(id) {
  const response = await fetch(`${API_URL}/Itinerarios/${id}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Error al obtener el itinerario: ${response.status}`);
  }

  return await response.json();
}
