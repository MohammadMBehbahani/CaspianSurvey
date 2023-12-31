using System.Data.SqlClient;

namespace Service.DAL.Interface
{
    public interface IDatabasService
    {
        Task<List<T?>> SqlCommand_Get_Data<T>(string connectionString, string storedProcName, List<SqlParameter> parameters);
        Task<T?> SqlCommand_Get_Single_Data<T>(string connectionString, string storedProcName, List<SqlParameter> parameters);
        Task<T?> SqlCommand_Get_Single_Data_Type<T>(string connectionString, string storedProcName, List<SqlParameter> parameters);
        Task SqlCommand_No_Data(string connectionString, string storedProcName, List<SqlParameter> parameters);
    }
}