### Clase
    Red de atracciones y transporte (red-atracciones-transporte)

### Funciones:
    - añadir-actividad-grafo(actividad):
        si grafo no contiene nodo de igual id: añadirlo.
    - eliminar-actividad-grafo(id):
        si grafo contiene actividad de igual id: eliminarlo junto a sus aristas incidentes.
    - añadir-actividad-grafo(actividad):
        si grafo no contiene nodo de igual id: añadirlo.
    - eliminar-atracción-grafo(id):
        si grafo contiene atracción de igual id: eliminarlo junto a sus aristas incidentes.
    - nodo-actividad(id):
        devolver nodo que representa la actividad en el grafo.
    - nodo-atracción(id):
        devolver nodo que representa la atracción en el grafo.
    - tiempo(nodoL, nodoR): tiempo de transporte de nodoL a nodoR.
    - costo(nodoL, nodoR): costo de transporte de nodoL a nodoR.
    - añadir-arista(nodoL, nodoR, cercanía geográfica, tiempo de transporte, costo de transporte):
        si el grafo no contiene (nodoL, nodoR): añadirlo; tiempo(nodoL, nodoR) = t; costo(nodoL, nodoR) = c.
    - eliminar-arista(nodoL, nodoR):
        si el grafo contiene (nodoL, nodoR): eliminarlo, tiempo(nodoL,nodoR) = null, costo(nodoL, nodoR) = null.
    - calcular-costo(itinerario):
        calcula el costo total del transporte en el itinerario
    - calcular-tiempo(itinerario):
        calcular el tiempo total del transporte en un itinerario.
    - proponer-alternativa(origen, siguiente, costo máximo, tiempo máximo, filtro de actividad):
        Devolver id de actividad que a partir de la red de transporte tal que:
            - distinto a origen y siguiente;
            - alcanzable por la actividad de origen;
            - alcance la actividad siguiente;
            - que cumpla el filtro de actividad.

### Colaboradores:
    - arista(nodoL, nodoR, tiempo, costo)
    - actividad(id, categoría, tipo, nivel de esfuerzo, ubicación, estado de cancelación, valoración)
    - atracción(id, ubicación)
    - itinerario(actividades)
    - filtroActividad(actividad): devuelve 1 si la actividad cumple los valores de requeridos (nivel de esfuerzo, categoría, estado de cancelación, tipo u otros), o en caso contrario.