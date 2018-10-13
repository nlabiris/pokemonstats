using System;
using System.Collections.Generic;
using System.Data;

namespace PokemonStats_WPF.DataAccess {
    internal interface IDatabase : IDisposable {
        IDbConnection CreateConnection();
        IDbConnection CreateOpenConnection();
        void OpenConnection();
        void CloseConnection();
        IDbCommand CreateCommand();
        IDbCommand CreateCommand(string commandText, IDbConnection connection);
        IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);
        IDataParameter CreateParameter(string parameterName, object parameterValue);
        void BeginTransaction();
        void CommitTransaction();
    }   
}