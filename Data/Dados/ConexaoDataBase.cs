using Microsoft.AspNetCore.Http;

namespace ProjetoWakeCommerce.Data
{
    public static class ConexaoDataBase
    {
        private static HttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public static string Connection()
        {
            return "server=localhost\\SQLEXPRESS;database=WakeCommerce;trusted_connection=true;TrustServerCertificate=True";
        }
    }
}
