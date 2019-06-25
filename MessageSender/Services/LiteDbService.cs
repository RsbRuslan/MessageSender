using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LiteDB;
using MessageSender.Interfaces;

namespace MessageSender.Services
{
    public class LiteDbService<TCollection> : IDbService<TCollection>
    {
        private readonly string _dbFilePath;

        public LiteDbService()
        {
            _dbFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _dbFilePath += "/MessagesStorage.db";
        }

        public void Invoke(Action<LiteDatabase, LiteCollection<TCollection>> action)
        {
            using (var db = new LiteDatabase(_dbFilePath))
            {
                LiteCollection<TCollection> collection = db.GetCollection<TCollection>();

                action.Invoke(db, collection);
            }
        }

        public TReturn Invoke<TReturn>(Func<LiteDatabase, LiteCollection<TCollection>, TReturn> action)
        {
            using (var db = new LiteDatabase(_dbFilePath))
            {
                LiteCollection<TCollection> collection = db.GetCollection<TCollection>();

                return action(db, collection);
            }
        }

    }
}
