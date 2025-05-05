namespace WebApp.Utilities;

public static class HashUtility
{
    public static string Obfuscate(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        
        // Ofuscación básica para logs (no usar para seguridad real)
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
            .Replace("=", "")
            .Substring(0, 8);
    }
}