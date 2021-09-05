﻿using System;
using System.Collections.Generic;
using ETLLibrary.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETLLibrary.Database
{
    public class SqlServerDatasetManager : ISqlServerDatasetManager
    {
        private SqlServerGateway _gateway;

        public SqlServerDatasetManager(EtlContext context)
        {
            _gateway = new SqlServerGateway(context);
        }

        public List<string> GetTables(string dbName, string dbUsername, string dbPassword, string url)
        {
            var tableNames = new List<string>();
            var queryString = DatabaseConfigurator.GetSqlQuery(dbName);
            var connectionString = DatabaseConfigurator.GetConnectionString(dbName, dbUsername, dbPassword, url);
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(queryString, connection);
            try
            {
                command.Connection.Open();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return null;
            }
            var myReader = command.ExecuteReader();
            while (myReader.Read())
            {
                tableNames.Add((myReader.GetValue(0)).ToString());
            }
            return tableNames;
        }

        public void CreateDataset(string username, DatasetInfo info)
        {
            _gateway.AddDataset(username, info);
        }

        public void DeleteDataset(string name, User user)
        {
            _gateway.DeleteDataset(name, user.Id);
        }

        public List<string> GetDatasets(string username)
        {
            return _gateway.GetUserDatasets(username);
        }
    }
}