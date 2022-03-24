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
    public class DbOperationsMsSql : IDbOperations
    {
        private DynamicParameters _parameters;
        private  readonly string _connectionString;
        public DbOperationsMsSql(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteQuery(string sql, DynamicParameters parameters)
        {
            int affectedRows = 0;
            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                
                    affectedRows = connection.Execute(sql,_parameters);
                    
                               
            }

            return affectedRows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteReader(string sql, DynamicParameters parameters)
        {
            var dataTable = new DataTable();
            IDataReader reader = null;
            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
               
                     reader = connection.ExecuteReader(sql, _parameters);

                               dataTable.Load(reader);
            }

            return dataTable;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, DynamicParameters parameters)
        {
            T result  ;

            using(var connection = new SqlConnection(_connectionString))
            {
                
                    result = connection.ExecuteScalar<T>(sql, _parameters);
                
            }

            return result;
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteStoredProcedure(string procedureName, DynamicParameters parameters)
        {
            var affectedRows = 0;

            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                
                    affectedRows = connection.Execute(procedureName, _parameters,commandType : CommandType.StoredProcedure);

                
            }

            return affectedRows;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProcedureQuery<T>(string procedureName,DynamicParameters parameters) where T : class
        {
            IEnumerable<T> result ;

            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
               
                    result = connection.Query<T>(procedureName, _parameters, commandType: CommandType.StoredProcedure);

            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<T>(string sql, DynamicParameters parameters) where T : class
        {
            IEnumerable<T> result;

            using (var connection = new SqlConnection(_connectionString))
            {
               
                    result = connection.Query<T>(sql, _parameters);

            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteQueryFirstOrDefault<T>(string sql, DynamicParameters parameters) where T : class
        {
            T result;
            _parameters = new DynamicParameters(parameters);

            using (var connection = new SqlConnection(_connectionString))
            {
               
                    result = connection.QueryFirstOrDefault<T>(sql, _parameters);

               
            }
            return result;
        }

        public Dictionary<string,object> ExecuteStoredProcedureWithOutPutParameters(string procedureName,DynamicParameters parameters ,List<string> outputParameters)
        {
            var affectedRows = 0;
            var returnParameters = new Dictionary<string, object>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters!=null)    
                {
                    affectedRows = connection.Execute(procedureName, _parameters, commandType: CommandType.StoredProcedure);

                }
                else
                {
                    affectedRows = connection.Execute(procedureName, commandType: CommandType.StoredProcedure);
                }

                foreach (var item in _parameters.ParameterNames)
                {
                    if(outputParameters.Contains(item))
                    {
                        returnParameters.Add(item.ToString(), _parameters.Get<dynamic>(item.ToString()));
                    }
                    
                }
            }

            return returnParameters;
        }

        
    }
}
