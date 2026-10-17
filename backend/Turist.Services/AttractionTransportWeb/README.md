### Clase
    Red de atracciones y transporte (red-atracciones-transporte)

### Funciones:
    - añadir-actividad-grafo(actividad):
        si grafo no contiene nodo de igual id: añadirlo.
    - eliminar-actividad-grafo(id):
        si grafo contiene actividad de igual id: eliminarlo junto a sus aristas incidentes.
    - cambiar-cancelación(id, estado de cancelación):
        dado su id cambiar estado de cancelación de actividad.
    - añadir-actividad(atracción):
        si grafo no contiene nodo de igual id: añadirlo.
    - eliminar-atracción(id):
        si grafo contiene atracción de igual id: eliminarlo junto a sus aristas incidentes.
    - nodo-actividad(id):
        devolver nodo que representa la actividad en el grafo.
    - nodo-atracción(id):
        devolver nodo que representa la atracción en el grafo.
    - añadir-arista(nodoL, nodoR, cercanía geográfica, tiempo de transporte, costo de transporte):
        si el grafo no contiene (nodoL, nodoR): añadirlo; tiempo(nodoL, nodoR) = t; costo(nodoL, nodoR) = c.
    - eliminar-arista(nodoL, nodoR):
        si el grafo contiene (nodoL, nodoR): eliminarlo, tiempo(nodoL,nodoR) = null, costo(nodoL, nodoR) = c.
    - calcular-costo(itinerario):
        calcula el costo total del transporte en el itinerario
    - calcular-tiempo(itinerario):
        calcular el tiempo total de un itinerario (actividades ordenadas y horarios).
    - validar-horario(itinerario):
        valida y halla conflictos entre horarios en itinerary.
    - recomendar-actividad(origen, momento, radio, filtro de actividad):
        dado una ubicación geográfica: devolver id de actividad que:
            - esté abierta en el momento.
            - no esté cancelada.
            - ubicación dentro del radio.
            - que cumpla el filtro.
    - abierto-intervalo(id de actividad, desde, hasta)
    - abierto-instante(id de actividad, momento)
### Colaboradores:

    - arista(nodoL, nodoR, tiempo, costo)
    - actividad(id, categoría, tipo, nivel de esfuerzo, ubicación, estado de cancelación, valoración)
    - atracción(id, ubicación)
    - itinerario(actividades)
    - filtroActividad(categorías, niveles de esfuerzo)
    - validación (id, id de itinerario, id de guía,fecha de validación, estado)