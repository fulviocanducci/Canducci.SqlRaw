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

        public SqlBuilderUpdate UpdateFrom(string table, Provider provider)
        {
            return new SqlBuilderUpdate(table, provider);
        }

        public static SqlBuilderParameter NullValue<T>(T? value = default(T?)) where T : struct
            => new SqlBuilderParameter<T>(value);
        
        public static SqlBuilderParameter NullValue(string value = null)
            => new SqlBuilderParameterString(value);

    }
}
