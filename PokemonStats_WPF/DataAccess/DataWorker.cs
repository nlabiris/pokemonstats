using System;
using System.Configuration;
using System.Reflection;
using PokemonStats_WPF.Properties;

namespace PokemonStats_WPF.DataAccess {
    public class DataWorker {
        private static IDatabase _database = null;
        static DataWorker() {
            try {
                _database = DatabaseFactory.CreateDatabase();
            } catch (Exception ex) {
                throw ex;
            }
        }

        internal static IDatabase database {
            get { return _database; }
        }
    }
}