namespace SimpleBot.Logic
{
    public static class MongoDbConfiguration
    {
        public static string Conexao = @"mongodb://127.0.0.1:27017";
        public static string Banco = @"simpleBot";

        public static string TabelaMensagem = @"mensagem";
        public static string TabelaUsuario = @"usuario";
    }
}