using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ToDoLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    public IConfiguration _config { get; }

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, U>(
        string storedProcedureName,
        U parameters,
        string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName)!;

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(
                        storedProcedureName,
                        parameters,
                        commandType: CommandType.StoredProcedure);

        return rows.ToList();

    }

    public Task SaveData<T>(
        string storedProcedureName,
        T parameters,
        string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName)!;

        using IDbConnection connection = new SqlConnection(connectionString);

        return connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure);
    }


}
