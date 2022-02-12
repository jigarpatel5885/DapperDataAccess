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
        Task<int> ExecuteQueryAsync(string sql, Dictionary<string, object> parameters);

        Task<int> ExecuteStoredProcedureAysnc(string procedureName, Dictionary<string, object> parameters);

        Task<DataTable> ExecuteReaderAsync(string sql, Dictionary<string, object> parameters);

        Task<T> ExecuteScalarAsync<T>(string sql, Dictionary<string, object> parameters);

        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, Dictionary<string, object> parameters) where T : class;

        Task<IEnumerable<T>> ExecuteStoredProcedureQueryAsync<T>(string procedureName, Dictionary<string, object> parameters) where T : class;

        Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string sql, Dictionary<string, object> parameters) where T : class;
    }
}
