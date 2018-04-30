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

            (string Sql, List<object> Values) SqlSqlServer = insertSqlServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToSqlBinding();
            (string Sql, List<object> Values) SqlMySqlServer = insertMysqlServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToSqlBinding();
            (string Sql, List<object> Values) SqlPostgresServer = insertPostgresServer.AddColumns("Name", "Created").AddValues("Name 1", created).ToSqlBinding();

            Assert.AreEqual($"INSERT INTO [People]([Name],[Created]) VALUES(p0,p1)", SqlSqlServer.Sql);
            Assert.AreEqual($"INSERT INTO `People`(`Name`,`Created`) VALUES(p0,p1)", SqlMySqlServer.Sql);
            Assert.AreEqual($"INSERT INTO \"People\"(\"Name\",\"Created\") VALUES(p0,p1)", SqlPostgresServer.Sql);

            Assert.AreEqual("Name 1", SqlSqlServer.Values[0]);
            Assert.AreEqual(created, SqlSqlServer.Values[1]);

            Assert.AreEqual("Name 1", SqlMySqlServer.Values[0]);
            Assert.AreEqual(created, SqlMySqlServer.Values[1]);

            Assert.AreEqual("Name 1", SqlPostgresServer.Values[0]);
            Assert.AreEqual(created, SqlPostgresServer.Values[1]);
        }
    }
}
