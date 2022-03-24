using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IDbOperationsAsync
    {
        Task<int> ExecuteQueryAsync(string sql, DynamicParameters parameters);

        Task<int> ExecuteStoredProcedureAysnc(string procedureName, DynamicParameters parameters);

        Task<DataTable> ExecuteReaderAsync(string sql, DynamicParameters parameters);

        Task<T> ExecuteScalarAsync<T>(string sql, DynamicParameters parameters);

        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, DynamicParameters parameters) where T : class;

        Task<IEnumerable<T>> ExecuteStoredProcedureQueryAsync<T>(string procedureName, DynamicParameters parameters) where T : class;

        Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parameters) where T : class;

        Task<Dictionary<string, object>> ExecuteStoredProcedureWithOutPutParameters(string procedureName, DynamicParameters parameters, List<string> outputParameters);
    }
}
