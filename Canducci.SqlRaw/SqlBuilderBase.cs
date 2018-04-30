using Canducci.SqlRaw.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Canducci.SqlRaw
{
    public abstract class SqlBuilderBase<T> where T: class
    {
        protected string Table { get; set; }
        protected List<string> Columns { get; set; }
        protected List<object> Values { get; set; }
        protected Dictionary<string, object> Wheres { get; set; }
        protected Provider Provider { get; set; }

        public SqlBuilderBase(string table, Provider provider)
        {
            Provider = provider;
            Table = provider.CreateTag(table);            
            Columns = new List<string>();
            Values = new List<object>();
        }

        private T GetSqBuilderType() => (T)Convert.ChangeType(this, typeof(T));

        public T Add(IEnumerable<string> name, IEnumerable<object> values)
        {
            if (!(name.Count() == values.Count()))
            {
                throw new ArgumentOutOfRangeException();
            }
            AddColumns(name.ToArray());
            AddValues(values);
            return GetSqBuilderType();
        }

        public T AddColumn(string name)
        {
            Columns.Add(Provider.CreateTag(name));
            return GetSqBuilderType();
        }

        public T AddColumns(params string[] names)
        {
            names.ToList().ForEach(x => AddColumn(x));
            return GetSqBuilderType();
        }

        public T AddValue<TValue>(TValue value)
        {
            Values.Add(value);
            return GetSqBuilderType();
        }

        public T AddValues(params object[] values)
        {            
            values.ToList().ForEach(x => AddValue(x));
            return GetSqBuilderType();
        }

        public abstract string ToRawSql();
        public abstract (string Sql, List<object> Values) ToSqlBinding();
    }
}
