namespace Bny.UploadBoletos.Application.Extensions
{
    public static class StringExtensions
    {
        public static bool EhInicioArquivo(this string value)
        {
            return string.Equals(value, "0#RV");
        }

        public static bool EhFimArquivo(this string value)
        {
            return string.Equals(value, "99#RV");
        }
    }
}
