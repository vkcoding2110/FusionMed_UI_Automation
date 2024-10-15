namespace UIAutomation.Utilities
{
    internal static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)System.Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}
