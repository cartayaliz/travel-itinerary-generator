// src/services/authService.js
import { hashearPasswordSHA256 } from "../utils/crypto";

const API_URL = "http://localhost:5064/api";

export function obtenerRolDesdeToken(token) {
  try {
    const payloadBase64 = token.split(".")[1];
    const payloadJson = JSON.parse(atob(payloadBase64));
    const rol =
      payloadJson.role ||
      payloadJson["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    return rol ? rol.toLowerCase() : null;
  } catch (error) {
    console.error("No se pudo decodificar el token:", error);
    return null;
  }
}

export async function loginUsuario(userName, password) {
  const passwordCifrada = await hashearPasswordSHA256(password);

  const response = await fetch(`${API_URL}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      UserName: userName,
      Password: passwordCifrada,
    }),
  });

  const data = await response.json().catch(() => ({}));

  if (!response.ok) {
    throw new Error(data.message || "Usuario o contraseña incorrectos.");
  }

  return data; // Devuelve { token, usuarioID, userName, mensaje }
}

export async function registrarTurista({ userName, nombre, edad, password }) {
  const passwordCifrada = await hashearPasswordSHA256(password);

  const response = await fetch(`${API_URL}/turistas`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      UserName: userName,
      Nombre: nombre,
      Edad: Number(edad),
      Password: passwordCifrada,
    }),
  });

  const data = await response.json().catch(() => ({}));

  if (!response.ok) {
    throw new Error(data.message || "No se pudo completar el registro.");
  }

  return data;
}