using Dapper;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbOperationMsSqlAsync : IDbOperationsAsync
    {
        private DynamicParameters _parameters;
        private readonly string _connectionString;
        public DbOperationMsSqlAsync(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<int> ExecuteQueryAsync(string sql, Dictionary<string, object> parameters)
        {
            int affectedRows = 0;
            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    affectedRows =  await connection.ExecuteAsync(sql, _parameters);

                }
                else
                {
                    affectedRows = await connection.ExecuteAsync(sql);
                }
            }

            return affectedRows;
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, Dictionary<string, object> parameters) where T : class
        {
            IEnumerable<T> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = await connection.QueryAsync<T>(sql, _parameters);

                }
                else
                {
                    result = await connection.QueryAsync<T>(sql);
                }
            }
            return result;
        }

        public async Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string sql, Dictionary<string, object> parameters) where T : class
        {
            T result;

            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = await connection.QueryFirstOrDefaultAsync<T>(sql, _parameters);

                }
                else
                {
                    result = await connection.QueryFirstOrDefaultAsync<T>(sql);
                }
            }
            return result;
        }

        public async Task<DataTable> ExecuteReaderAsync(string sql, Dictionary<string, object> parameters)
        {
            var dataTable = new DataTable();
            IDataReader reader = null;
            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    reader = await connection.ExecuteReaderAsync(sql, _parameters);

                }
                else
                {
                    reader = await connection.ExecuteReaderAsync(sql);
                }
                dataTable.Load(reader);
            }

            return dataTable;
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, Dictionary<string, object> parameters)
        {
            T result;

            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = await connection.ExecuteScalarAsync<T>(sql, _parameters);
                }
                else
                {
                    result = await connection.ExecuteScalarAsync<T>(sql);
                }
            }

            return result;
        }

        public async Task<int> ExecuteStoredProcedureAysnc(string procedureName, Dictionary<string, object> parameters)
        {
            var affectedRows = 0;

            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    affectedRows = await connection.ExecuteAsync(procedureName, _parameters, commandType: CommandType.StoredProcedure);

                }
                else
                {
                    affectedRows = await connection.ExecuteAsync(procedureName, commandType: CommandType.StoredProcedure);
                }
            }

            return affectedRows;
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureQueryAsync<T>(string procedureName, Dictionary<string, object> parameters) where T : class
        {
            IEnumerable<T> result;

            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = await connection.QueryAsync<T>(procedureName, _parameters, commandType: CommandType.StoredProcedure);

                }
                else
                {
                    result = await connection.QueryAsync<T>(procedureName, commandType: CommandType.StoredProcedure);
                }
            }

            return result;
        }
    }
}
