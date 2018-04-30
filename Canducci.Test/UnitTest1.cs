using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Canducci.SqlRaw;
using Canducci.SqlRaw.Providers;
using System.Collections.Generic;

namespace Canducci.Test
{
    [TestClass]
    public class UnitTest1
    {
        private Provider SqlServerProvider => new SqlServerProvider();
        private Provider MysqlServerProvider => new MysqlServerProvider();
        private Provider PostgresServerProvider => new PostgresServerProvider();

        [TestMethod]
        public void TestMethodInsertOuputRawSql()
        {
            var created = DateTime.Now.AddDays(-1);

            SqlBuilder raw = new SqlBuilder();

            SqlBuilderInsert insertSqlServer = raw.InsertFrom("People", SqlServerProvider);
            SqlBuilderInsert insertMysqlServer = raw.InsertFrom("People", MysqlServerProvider);
            SqlBuilderInsert insertPostgresServer = raw.InsertFrom("People", PostgresServerProvider);

            string strSqlSqlServer = insertSqlServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToRawSql();
            string strSqlMySqlServer = insertMysqlServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToRawSql();
            string strSqlPostgresServer = insertPostgresServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToRawSql();

            Assert.AreEqual($"INSERT INTO [People]([Name],[Created]) VALUES('Name 1','{created}')", strSqlSqlServer);
            Assert.AreEqual($"INSERT INTO `People`(`Name`,`Created`) VALUES('Name 1','{created}')", strSqlMySqlServer);
            Assert.AreEqual($"INSERT INTO \"People\"(\"Name\",\"Created\") VALUES('Name 1','{created}')", strSqlPostgresServer);

        }

        [TestMethod]
        public void TestMethodInsertOuputSqlAndListOfParameter()
        {
            var created = DateTime.Now.AddDays(-1);

            SqlBuilder raw = new SqlBuilder();

            SqlBuilderInsert insertSqlServer = raw.InsertFrom("People", SqlServerProvider);
            SqlBuilderInsert insertMysqlServer = raw.InsertFrom("People", MysqlServerProvider);
            SqlBuilderInsert insertPostgresServer = raw.InsertFrom("People", PostgresServerProvider);

            (string Sql, List<object> Values) strSqlSqlServer = insertSqlServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToSqlBiding();
            (string Sql, List<object> Values) strSqlMySqlServer = insertMysqlServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToSqlBiding();
            (string Sql, List<object> Values) strSqlPostgresServer = insertPostgresServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToSqlBiding();

            Assert.AreEqual($"INSERT INTO [People]([Name],[Created]) VALUES(p0,p1)", strSqlSqlServer.Sql);
            Assert.AreEqual($"INSERT INTO `People`(`Name`,`Created`) VALUES(p0,p1)", strSqlMySqlServer.Sql);
            Assert.AreEqual($"INSERT INTO \"People\"(\"Name\",\"Created\") VALUES(p0,p1)", strSqlPostgresServer.Sql);

        }
    }
}
