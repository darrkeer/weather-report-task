public class CelciumValue
{
    public static string Convert(float f)
    {
        return $"{(f >= 0 ? "+" : "-")}{f}°C";
    }
}