﻿using System.Collections.Generic;
using System.Linq;
using ETLLibrary.Database.Models;
using ETLLibrary.Database.Utils;
using ETLLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETLLibrary.Database.Gataways
{
    public class SqlServerGateway : IDatabaseGateway
    {
        
        public readonly EtlContext Context;

        public SqlServerGateway(EtlContext context)
        {
            Context = context;
        }
        
        public List<string> GetUserDatasets(string username)
        {
            var user = Context.Users.Where(w => w.Username == username).Include(w => w.DbConnections).Single();
            return user.DbConnections.Select(document => document.Name).ToList();
        }

        public void AddDataset(string username, DatasetInfo info)
        {
            var user = Context.Users.Include(x => x.DbConnections).Single(u => u.Username == username);
            var dbConnection = new DbConnection()
            {
                DbName = info.DbName,
                DbPassword = info.DbPassword,
                DbUsername = info.DbUsername,
                Name = info.Name,
                Url = info.Url,
                Table = info.Table,
                User = user
            };
            user.DbConnections.Add(dbConnection);
            Context.SaveChanges();
        }

        public void DeleteDataset( string datasetName, int userId)
        {
            var dbConnection = Context.DbConnections.SingleOrDefault(x => x.Name == datasetName && x.UserId == userId);
            if (dbConnection == null) return;
            Context.DbConnections.Remove(dbConnection);
            Context.SaveChanges();
        }
    }
}