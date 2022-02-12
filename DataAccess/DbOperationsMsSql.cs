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
        public int ExecuteQuery(string sql, Dictionary<string, object> parameters)
        {
            int affectedRows = 0;
            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if(parameters.Count >0)
                {
                    affectedRows = connection.Execute(sql,_parameters);
                    
                }
                else
                {
                    affectedRows = connection.Execute(sql);
                }               
            }

            return affectedRows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteReader(string sql, Dictionary<string, object> parameters)
        {
            var dataTable = new DataTable();
            IDataReader reader = null;
            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                     reader = connection.ExecuteReader(sql, _parameters);

                }
                else
                {
                    reader = connection.ExecuteReader(sql);
                }
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
        public T ExecuteScalar<T>(string sql, Dictionary<string, object> parameters)
        {
            T result  ;

            using(var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = connection.ExecuteScalar<T>(sql, _parameters);
                }
                else
                {
                    result = connection.ExecuteScalar<T>(sql);
                }
            }

            return result;
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            var affectedRows = 0;

            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    affectedRows = connection.Execute(procedureName, _parameters,commandType : CommandType.StoredProcedure);

                }
                else
                {
                    affectedRows = connection.Execute(procedureName,commandType:CommandType.StoredProcedure);
                }
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
        public IEnumerable<T> ExecuteStoredProcedureQuery<T>(string procedureName,Dictionary<string,object> parameters) where T : class
        {
            IEnumerable<T> result ;

            _parameters = new DynamicParameters(parameters);
            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = connection.Query<T>(procedureName, _parameters, commandType: CommandType.StoredProcedure);

                }
                else
                {
                    result = connection.Query<T>(procedureName, commandType: CommandType.StoredProcedure);
                }
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
        public IEnumerable<T> ExecuteQuery<T>(string sql, Dictionary<string, object> parameters) where T : class
        {
            IEnumerable<T> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = connection.Query<T>(sql, _parameters);

                }
                else
                {
                    result = connection.Query<T>(sql);
                }
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
        public T ExecuteQueryFirstOrDefault<T>(string sql, Dictionary<string, object> parameters) where T : class
        {
            T result;
            _parameters = new DynamicParameters(parameters);

            using (var connection = new SqlConnection(_connectionString))
            {
                if (parameters.Count > 0)
                {
                    result = connection.QueryFirstOrDefault<T>(sql, _parameters);

                }
                else
                {
                    result = connection.QueryFirstOrDefault<T>(sql);
                }
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
