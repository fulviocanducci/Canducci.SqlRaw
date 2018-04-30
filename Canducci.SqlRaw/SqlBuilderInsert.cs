using Canducci.SqlRaw.Providers;
using Canducci.SqlRaw.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canducci.SqlRaw
{
    public class SqlBuilderInsert: SqlBuilderBase<SqlBuilderInsert>
    {
        public SqlBuilderInsert(string table, Provider provider)
            :base(table, provider)
        {     
        }
        
        public override string ToRawSql()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"INSERT INTO {Table}");
            strBuilder.Append("(");
            strBuilder.Append(string.Join(",", Columns));
            strBuilder.Append(")");
            strBuilder.Append(" VALUES");
            strBuilder.Append("(");
            var ValuesOfNull = Values;
            for(int i = 0; i < ValuesOfNull.Count; i++)
            {
                if (ValuesOfNull[i] is SqlBuilderParameter parameter)
                {
                    ValuesOfNull[i] = parameter.Value == null ? "NULL": parameter.Value;
                }
                else
                {
                    ValuesOfNull[i] = $"'{ValuesOfNull[i]}'";
                }
            }
            strBuilder.Append(string.Join(",", ValuesOfNull));
            strBuilder.Append(")");
            return strBuilder.ToString();
        }

        public override (string Sql, List<object> Values, object ClassObject) ToSqlBinding()
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
                    parameter += $"@p{i++}";
                });
                return parameter;
            };
            
            strBuilder.Append($"INSERT INTO {Table}");
            strBuilder.Append("(");
            strBuilder.Append(string.Join(",", Columns));
            strBuilder.Append(")");
            strBuilder.Append(" VALUES");
            strBuilder.Append("(");
            strBuilder.Append(Func(Values));
            strBuilder.Append(")");
            return (strBuilder.ToString(), Values, ParameterObjectBuilder.CreateObjectWithValues(Values));
        }
    }
}
