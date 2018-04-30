﻿namespace Canducci.SqlRaw.Providers
{
    public class SqlServerProvider : Provider
    {
        public override string CloseTag()
        {
            return "]";
        }

        public override string OpenTag()
        {
            return "[";
        }
    }
}
