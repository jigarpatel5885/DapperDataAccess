using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IDbOperations
    {
        int ExecuteQuery(string sql,Dictionary<string,object> parameters);

        int ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters);

        DataTable ExecuteReader(string sql, Dictionary<string, object> parameters);

        T ExecuteScalar<T>(string sql, Dictionary<string, object> parameters);

        IEnumerable<T> ExecuteQuery<T>(string sql, Dictionary<string, object> parameters) where T : class;

        IEnumerable<T> ExecuteStoredProcedureQuery<T>(string procedureName, Dictionary<string, object> parameters) where T : class;


        T ExecuteQueryFirstOrDefault<T>(string sql, Dictionary<string, object> parameters) where T : class;


    }
}
