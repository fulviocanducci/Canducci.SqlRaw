namespace Canducci.SqlRaw.Providers
{
    public class MysqlServerProvider : Provider
    {
        public override string CloseTag()
        {
            return "`";
        }

        public override string OpenTag()
        {
            return "`";
        }
    }
}
