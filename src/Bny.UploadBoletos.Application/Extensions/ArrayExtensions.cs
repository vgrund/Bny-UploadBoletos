namespace Bny.UploadBoletos.Application.Extensions
{
    public static class ArrayExtensions
    {
        public static string SafeGetValueByIndex(this string[] array, int index)
        {
            if (index < array.Length)
            {
                return array[index];
            }
            return string.Empty;
        }
    }
}
