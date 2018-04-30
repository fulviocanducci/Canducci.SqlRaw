using Canducci.SqlRaw.Providers;
using Canducci.SqlRaw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canducci.SqlRaw
{
    public class SqlBuilderUpdate : SqlBuilderBase<SqlBuilderUpdate>
    {
        public SqlBuilderUpdate(string table, Provider provider)
            : base(table, provider)
        {
            Wheres = new Dictionary<string, object>();
        }

        public SqlBuilderUpdate Where(string name, object value)
        {
            Wheres.Add(Provider.CreateTag(name), value);
            return this;
        }

        public override string ToRawSql()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"UPDATE {Table}");
            strBuilder.Append(" SET ");
            int i = 0;
            var ValuesOfNull = Values;
            for (i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                {
                    strBuilder.Append(",");
                }
                if (Values[i] is SqlBuilderParameter parameter)
                {
                    ValuesOfNull[i] = parameter.Value == null ? "NULL" : parameter.Value;
                }
                else
                {
                    ValuesOfNull[i] = $"'{ValuesOfNull[i]}'";
                }
                strBuilder.AppendFormat("{0}={1}", Columns[i], ValuesOfNull[i]);
            }
            strBuilder.Append(" WHERE ");
            i = 0;
            foreach (var where in Wheres)
            {
                strBuilder.AppendFormat("{0}={1}", where.Key, where.Value);
            }
            return strBuilder.ToString();
        }

        public override (string Sql, List<object> Values, object ClassObject) ToSqlBinding()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"UPDATE {Table}");
            strBuilder.Append(" SET ");
            int i = 0;
            for (i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                {
                    strBuilder.Append(",");
                }
                strBuilder.AppendFormat("{0}={1}", Columns[i], $"@p{i}");
            }
            strBuilder.Append(" WHERE ");            
            foreach (var where in Wheres)
            {
                strBuilder.AppendFormat("{0}={1}", where.Key, $"@p{i++}");
            }
            List<object> valueAndWhere = Values;
            valueAndWhere.AddRange(Wheres.Select(x => x.Value));            
            return (strBuilder.ToString(), valueAndWhere, ParameterObjectBuilder.CreateObjectWithValues(Values));
        }
    }
}
