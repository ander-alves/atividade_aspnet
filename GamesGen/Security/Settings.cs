namespace GamesGen.Security
{
    public class Settings
    {
        private static string secret = "9bc17f47b98208ef82d898ab4030bb0741cce7a85774a003b9191b6b74c0aab1";
        public static string Secret { get => secret; set => secret = value; } 
    }
}
