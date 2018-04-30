using Canducci.SqlRaw.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canducci.SqlRaw
{
    public class SqlBuilderInsert
    {
        public string Table { get; private set; }        
        public List<string> Columns { get; private set; }
        public List<object> Values { get; private set; }
        public Provider Provider { get; private set; }

        public SqlBuilderInsert(string table, Provider provider)
        {
            Table = table;
            Provider = provider;            
            Columns = new List<string>();
            Values = new List<object>();
        }

        public SqlBuilderInsert AddColumn(string name)
        {
            Columns.Add(Provider.CreateTag(name));
            return this;
        }

        public SqlBuilderInsert AddColumns(params string[] names)
        {
            names.ToList().ForEach(x => AddColumn(x));
            return this;
        }

        public SqlBuilderInsert AddValue<T>(T value)
        {
            Values.Add(value);
            return this;
        }

        public SqlBuilderInsert AddValues(params object[] values)
        {
            values.ToList().ForEach(x => AddValue(x));
            return this;
        }

        public string ToRawSql()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"INSERT INTO {Provider.CreateTag(Table)}");
            strBuilder.Append("(");
            strBuilder.Append(string.Join(",", Columns));
            strBuilder.Append(")");
            strBuilder.Append(" VALUES");
            strBuilder.Append("(");
            strBuilder.Append("'" + string.Join("','", Values) + "'");
            strBuilder.Append(")");
            return strBuilder.ToString();
        }

        public (string Sql, List<object> Values) ToSqlBiding()
        {
            StringBuilder strBuilder = new StringBuilder();
            Func<List<object>, string> Func = delegate (List<object> values)
            {
                string parameter = "";
                int i = 0;
                values.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(parameter))
                        parameter += ",";
                    parameter += $"p{i++}";
                });
                return parameter;
            };
            
            strBuilder.Append($"INSERT INTO {Provider.CreateTag(Table)}");
            strBuilder.Append("(");
            strBuilder.Append(string.Join(",", Columns));
            strBuilder.Append(")");
            strBuilder.Append(" VALUES");
            strBuilder.Append("(");
            strBuilder.Append(Func(Values));
            strBuilder.Append(")");
            return (strBuilder.ToString(), Values);
        }
    }
}
