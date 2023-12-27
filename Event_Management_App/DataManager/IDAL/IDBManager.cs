using Event_Management_App.DataManager.DAL;
using System.Data;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IDBManager
    {
        DataManager.DAL.DBManager InitDbCommand(string cmd);
        DataManager.DAL.DBManager InitDbCommand(string cmd, CommandType cmdtype);
        DataManager.DAL.DBManager AddCMDParam(string parametername, object value);
        DataManager.DAL.DBManager AddCMDParam(string parametername, object value, DbType dbtype);
        DataManager.DAL.DBManager AddCMDOutParam(string parametername, DbType dbtype, int iSize = 0);

        T GetOutParam<T>(string paramname);
        DataTable ExecuteDataTable();
        DataSet ExecuteDataSet();

        object? ExecuteScalar();

        Task<object?> ExecuteScalarAsync();
        int ExecuteNonQuery();
        Task<int> ExecuteNonQueryAsync();
        DBManager InitDbCommandText(string cmd);
    }
}
