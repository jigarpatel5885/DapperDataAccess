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
        public async Task<int> ExecuteQueryAsync(string sql, DynamicParameters parameters)
        {
            int affectedRows = 0;
            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                
                    affectedRows =  await connection.ExecuteAsync(sql, _parameters);

                
            }

            return affectedRows;
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, DynamicParameters parameters) where T : class
        {
            IEnumerable<T> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                
                    result = await connection.QueryAsync<T>(sql, parameters);

                
            }
            return result;
        }

        public async Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string sql,DynamicParameters parameters) where T : class
        {
            T result;

            using (var connection = new SqlConnection(_connectionString))
            {
                
                    result = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);

            }
            return result;
        }

        public async Task<DataTable> ExecuteReaderAsync(string sql, DynamicParameters parameters)
        {
            var dataTable = new DataTable();
            IDataReader reader = null;

            using (var connection = new SqlConnection(_connectionString))
            {

                reader = await connection.ExecuteReaderAsync(sql, parameters);

            }

            return dataTable;
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, DynamicParameters parameters)
        {
            T result;

            using (var connection = new SqlConnection(_connectionString))
            {
               
                    result = await connection.ExecuteScalarAsync<T>(sql, parameters);
               
            }

            return result;
        }

        public async Task<int> ExecuteStoredProcedureAysnc(string procedureName, DynamicParameters parameters)
        {
            var affectedRows = 0;

            
            using (var connection = new SqlConnection(_connectionString))
            {
                
                    affectedRows = await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

                
            }

            return affectedRows;
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureQueryAsync<T>(string procedureName, DynamicParameters parameters) where T : class
        {
            IEnumerable<T> result;

            
            using (var connection = new SqlConnection(_connectionString))
            {
               
                    result = await connection.QueryAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);

               
            }

            return result;
        }

        public async Task< Dictionary<string, object>> ExecuteStoredProcedureWithOutPutParameters(string procedureName, DynamicParameters parameters, List<string> outputParameters)
        {
            var affectedRows = 0;
            var returnParameters = new Dictionary<string, object>();

            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters != null)
                {
                    affectedRows = await connection.ExecuteAsync(procedureName, _parameters, commandType: CommandType.StoredProcedure);

                }
                else
                {
                    affectedRows = await connection.ExecuteAsync(procedureName, commandType: CommandType.StoredProcedure);
                }

                foreach (var item in _parameters.ParameterNames)
                {
                    if (outputParameters.Contains(item))
                    {
                        returnParameters.Add(item.ToString(), _parameters.Get<dynamic>(item.ToString()));
                    }

                }
            }

            return returnParameters;
        }
    }
}
