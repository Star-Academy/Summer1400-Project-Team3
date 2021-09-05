﻿namespace ETLLibrary.Database
{
    public static class DatabaseConfigurator
    {
        public static string GetSqlQuery(string dbName)
        {
            return $"SELECT TABLE_NAME FROM {dbName}.INFORMATION_SCHEMA.TABLES;";
        }

        public static string GetConnectionString(string dbName, string dbUsername, string dbPassword, string url)
        {
            return $"Server={url};Database={dbName};User Id={dbUsername};Password={dbPassword};";
        }
    }
}