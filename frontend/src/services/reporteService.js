/**
 * Obtiene el reporte comparativo de modificaciones vs. promedio del grupo.
 * @returns {Promise<Array<{
 *   turistaID: number,
 *   turistaNombre: string,
 *   paqueteNombre: string,
 *   promedioTuristaModificado: number,
 *   promedioGrupo: number
 * }>>}
 */
export async function obtenerComparacionModificaciones() {
    const response = await fetch("http://localhost:5064/api/reportes/comparacion-modificaciones", {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Error al obtener el reporte: ${response.status}`);
  }

  return await response.json();
}
/**
 * Obtiene las actividades con mayor tasa de abandono.
 * @returns {Promise<Array<{
 *   nombreActividad: string,
 *   nombreGuia: string,
 *   nombrePaquete: string,
 *   tasaAbandono: number,
 *   totalVisitas: number,
 *   visitasAbandonadas: number
 * }>>}
 */
export async function obtenerAbandonoActividades() {
  const response = await fetch("http://localhost:5064/api/reportes/actividades-mayor-abandono", {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Error al obtener el reporte de abandono: ${response.status}`);
  }

  return await response.json();
}
/**
 * Obtiene la correlación entre nivel de esfuerzo y valoración promedio por paquete.
 * @returns {Promise<Array<{
 *   nombrePaquete: string,
 *   nivelEsfuerzo: string,
 *   promedioValoracion: number,
 *   totalValoraciones: number
 * }>>}
 */
export async function obtenerCorrelacionEsfuerzoValoracion() {
  const response = await fetch("http://localhost:5064/api/reportes/correlacion-esfuerzo-valoracion", {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  });
  if (!response.ok) {
    throw new Error(`Error al obtener el reporte de abandono: ${response.status}`);
  }

  return await response.json();
}
/**
 * Obtiene el reporte comparativo entre paquetes turísticos con distribuciones
 * de categorías y niveles de esfuerzo.
 *
 * @returns {Promise<Array<{
 *   nombrePaquete: string,
 *   totalItinerarios: number,
 *   totalParadas: number,
 *   distribucionCategorias: Record<string, number>,
 *   distribucionEsfuerzo: Record<string, number>
 * }>>}
 */
export async function obtenerComparacionPaquetes() {
  const response = await fetch("http://localhost:5064/api/reportes/comparacion-paquetes", {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Error al obtener el reporte de paquetes: ${response.status}`);
  }

  return await response.json();
}
/**
 * Obtiene el reporte de satisfacción y esfuerzo físico por actividad.
 * @returns {Promise<Array<{
 *   nombreActividad: string,
 *   nivelEsfuerzo: string,
 *   promedioValoracion: number,
 *   totalValoraciones: number
 * }>>}
 */
export async function obtenerSatisfaccionEsfuerzo() {
  const response = await fetch("http://localhost:5064/api/reportes/satisfaccion-esfuerzo", {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  });

  if (!response.ok) {
    throw new Error(`Error al obtener el reporte de satisfacción y esfuerzo: ${response.status}`);
  }

  return await response.json();
}