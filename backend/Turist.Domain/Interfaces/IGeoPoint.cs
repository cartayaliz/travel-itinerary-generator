namespace Turist.Domain.Interfaces;

public interface IGeoPoint
{
    public double Latitude { get; }
    public double Longitude { get; }

    /// <summary>
    /// Convertir grados a radianes.
    /// </summary>
    /// <param name="deg"></param>
    /// <returns></returns>
    private static double DegToRad(double deg) => deg * Math.PI / 180;

    /// <summary>
    /// La forma estándar de calcular distancias entre dos puntos de latitud/longitud sobre la superficie de la Tierra.
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="lat2"></param>
    /// <param name="lon1"></param>
    /// <param name="lon2"></param>
    /// <returns></returns>
    public static double HaversineKm(double lat1, double lat2, double lon1, double lon2)
    {
        const double R = 6371.0;
        var dLat = DegToRad(lat2 - lat1);
        var dLon = DegToRad(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        return R * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    }
}