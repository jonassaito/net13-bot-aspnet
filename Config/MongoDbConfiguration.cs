using System.Configuration;

namespace SimpleBot.Config

{
    public static class MongoDbConfiguration
    {
        public static string Conexao = ConfigurationManager.ConnectionStrings["mongoDB"].ConnectionString;
        public static string Banco = @"simpleBot";

        public static string TabelaMensagem = @"mensagem";
        public static string TabelaUsuario = @"usuario";
    }
}