using Canducci.SqlRaw.Providers;

namespace Canducci.SqlRaw
{
    public class SqlBuilder
    {
        public SqlBuilder()
        {
        }

        public SqlBuilderInsert InsertFrom(string table, Provider provider)
        {
            return new SqlBuilderInsert(table, provider);
        }

    }
}
