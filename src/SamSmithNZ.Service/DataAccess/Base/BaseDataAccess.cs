using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SamSmithNZ.Service.DataAccess.Base
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class BaseDataAccess<T>
    {
        public string ConnectionString { get; set; }

        public void SetupConnectionString(IConfiguration configuration)
        {
            this.ConnectionString = configuration["ConnectionStrings:DefaultConnectionString"];
        }

        public async Task<List<T>> GetList(string query, DynamicParameters parameters = null, int timeOut = 15) //15 seconds is the default timeout
        {
            if (this.ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            IEnumerable<T> results;
            SqlConnection connection = new(this.ConnectionString);
            try
            {
                await connection.OpenAsync();
                results = await connection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure, commandTimeout: timeOut);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return results.ToList<T>();
        }

        public async Task<T> GetItem(string query, DynamicParameters parameters = null)
        {
            if (this.ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            T result;
            SqlConnection connection = new(this.ConnectionString);
            try
            {
                await connection.OpenAsync();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
                result = results.FirstOrDefault<T>();
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return result;
        }

        public async Task<R> GetScalarItem<R>(string query, DynamicParameters parameters = null)
        {
            if (this.ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            R result;
            SqlConnection connection = new(this.ConnectionString);
            try
            {
                await connection.OpenAsync();
                IEnumerable<R> results = await connection.QueryAsync<R>(query, parameters, commandType: CommandType.StoredProcedure);
                result = results.FirstOrDefault<R>();
                //DebugSQLString(query, parameters);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return result;
        }

        public async Task<bool> SaveItem(string query, DynamicParameters parameters = null, int timeOut = 15) //15 seconds is the default timeout
        {
            if (this.ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            bool result = false;
            SqlConnection connection = new(this.ConnectionString);
            try
            {
                await connection.OpenAsync();
                await connection.ExecuteScalarAsync<bool>(query, parameters, commandType: CommandType.StoredProcedure, commandTimeout: timeOut);
                //DebugSQLString(query, parameters); 
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                DebugSQLString(query, parameters);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return result;
        }

        private static string DebugSQLString(string sp, DynamicParameters parameters = null)
        {
            StringBuilder sb = new();
            sb.Append("exec ");
            sb.Append(sp);
            sb.Append(' ');
            if (parameters != null)
            {
                int i = 0;
                foreach (string name in parameters.ParameterNames)
                {
                    i++;
                    dynamic value = parameters.Get<dynamic>(name);
                    sb.Append('@');
                    sb.Append(name);
                    sb.Append('=');
                    if (value == null)
                    {
                        sb.Append("null");
                    }
                    else
                    {
                        if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.Guid" || value.GetType().ToString() == "System.DateTime")
                        {
                            sb.Append('\'');
                        }
                        if (value.GetType().ToString() == "System.Boolean")
                        {
                            if (value == true)
                            {
                                sb.Append('1');
                            }
                            else
                            {
                                sb.Append('0');
                            }
                        }
                        else
                        {
                            sb.Append(value.ToString());
                        }
                        if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.Guid" || value.GetType().ToString() == "System.DateTime")
                        {
                            sb.Append('\'');
                        }
                    }
                    if (parameters.ParameterNames.ToList<string>().Count > i)
                    {
                        sb.Append(", ");
                    }
                }
            }
            Debug.WriteLine(sb.ToString());
            return sb.ToString();
        }
    }
}

