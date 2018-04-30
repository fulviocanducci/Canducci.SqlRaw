namespace Canducci.SqlRaw.Providers
{
    public sealed class PostgresServerProvider : Provider
    {
        public override string CloseTag()
        {
            return "\"";
        }

        public override string OpenTag()
        {
            return "\"";
        }
    }
}
