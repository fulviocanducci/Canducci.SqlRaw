using System;
namespace Canducci.SqlRaw
{
    public abstract class SqlBuilderParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public SqlBuilderParameter(object value, Type type)
        {
            Value = value;
            ParameterType = type;
        }
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type ParameterType { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class SqlBuilderParameter<T> : SqlBuilderParameter
       where T : struct
    {
        /// <summary>
        /// Create QueryBuilderParamenter
        /// </summary>
        /// <param name="value"></param>
        public SqlBuilderParameter(T? value = default(T?))
            : base(value, typeof(T?))
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class SqlBuilderParameterString : SqlBuilderParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public SqlBuilderParameterString(string value)
            : base(value, typeof(string))
        {
        }
    }
}
