using Dapper;
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
        int ExecuteQuery(string sql,DynamicParameters parameters);

        int ExecuteStoredProcedure(string procedureName, DynamicParameters parameters);

        DataTable ExecuteReader(string sql, DynamicParameters parameters);

        T ExecuteScalar<T>(string sql, DynamicParameters parameters);

        IEnumerable<T> ExecuteQuery<T>(string sql, DynamicParameters parameters) where T : class;

        IEnumerable<T> ExecuteStoredProcedureQuery<T>(string procedureName, DynamicParameters parameters) where T : class;


        T ExecuteQueryFirstOrDefault<T>(string sql, DynamicParameters parameters) where T : class;

        Dictionary<string,object> ExecuteStoredProcedureWithOutPutParameters(string procedureName, DynamicParameters parameters, List<string> outputParameters);


    }
}
