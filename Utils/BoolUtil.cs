namespace personaAutorizadaBack.Utils
{
    public class BoolUtil
    {
        public static bool ToBoolOrDefault(string? value, bool defaultValue = true)
        {
            if (bool.TryParse(value, out var result))
                return result;

            if (value == "1")
                return true;

            if (value == "0")
                return false;

            return defaultValue;
        }
    }
}
