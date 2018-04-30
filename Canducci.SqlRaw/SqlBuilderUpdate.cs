using Canducci.SqlRaw.Providers;
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
            for (i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                {
                    strBuilder.Append(",");
                }
                strBuilder.AppendFormat("{0}='{1}'", Columns[i], Values[i]);
            }
            strBuilder.Append(" WHERE ");
            i = 0;
            foreach (var where in Wheres)
            {
                strBuilder.AppendFormat("{0}={1}", where.Key, where.Value);
            }
            return strBuilder.ToString();
        }

        public override (string Sql, List<object> Values) ToSqlBinding()
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
                strBuilder.AppendFormat("{0}={1}", Columns[i], $"p{i}");
            }
            strBuilder.Append(" WHERE ");            
            foreach (var where in Wheres)
            {
                strBuilder.AppendFormat("{0}={1}", where.Key, $"p{i++}");
            }
            List<object> valueAndWhere = Values;
            valueAndWhere.AddRange(Wheres.Select(x => x.Value));            
            return (strBuilder.ToString(), valueAndWhere);
        }
    }
}
