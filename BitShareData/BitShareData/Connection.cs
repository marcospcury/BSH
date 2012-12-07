using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BitShareData
{
    /// <summary>
    /// Helper para realizar consultas rápidas ao banco de dados
    /// </summary>
    public static class Connection
    {
        private static string connectionString = "data source=108.175.158.161;initial catalog=BitShareDB;user id=bitsharer;password=iwillshare;";

        private static SqlConnection connectionSQL;

        /// <summary>
        /// Executa uma query qualquer serializando o resultado em  IEnumerable<T>
        /// </summary>
        public static IEnumerable<T> ExecuteQuery<T>(string query)
        {
            connectionSQL = new SqlConnection(connectionString);
            connectionSQL.Open();
            var retorno = connectionSQL.Query<T>(query);
            connectionSQL.Close();
            return retorno;
        }

        /// <summary>
        /// Executa uma query com Join entre duas tabelas e une em um único objeto
        /// </summary>
        public static IEnumerable<T1> ExecuteQuery<T1, T2>(string query, Func<T1, T2, T1> map, string splitOn)
        {
            connectionSQL = new SqlConnection(connectionString);
            connectionSQL.Open();
            var retorno = connectionSQL.Query<T1, T2, T1>(query, map, splitOn: splitOn);
            connectionSQL.Close();
            return retorno;
        }


        /// <summary>
        /// Executa uma query com Join entre tres tabelas e une em um único objeto
        /// </summary>
        public static IEnumerable<T1> ExecuteQuery<T1, T2, T3>(string query, Func<T1, T2, T3, T1> map, string splitOn)
        {
            connectionSQL = new SqlConnection(connectionString);
            connectionSQL.Open();
            var retorno = connectionSQL.Query<T1, T2, T3, T1>(query, map, splitOn: splitOn);
            connectionSQL.Close();
            return retorno;
        }
    }
}
