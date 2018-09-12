using System.Configuration;

namespace SimpleBot.Config

{
    public static class SqlDbConfiguration
    {
        public static string SqlDbConnection = ConfigurationManager.ConnectionStrings["sqlServer"].ConnectionString;
        //public static string Banco = @"SimpleBot";

        //public static string TabelaMensagem = @"mensagem";
        //public static string TabelaUsuario = @"usuario";
    }
}