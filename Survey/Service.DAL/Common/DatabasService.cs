using System.Data;
using System.Data.SqlClient;
using Service.DAL.Interface;

namespace Service.DAL.Common
{
    public class DatabasService : IDatabasService
    {
        public async Task<List<T?>> SqlCommand_Get_Data<T>(
                      string connectionString,
                      string storedProcName,
                      List<SqlParameter> parameters)

        {
            var result = new List<T?>();

            var sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
            var cmd = new SqlCommand()
            {
                Connection = sqlConnection,
                CommandText = storedProcName,
                CommandType = CommandType.StoredProcedure
            };
            FixParams(parameters);
            cmd.Parameters.AddRange(parameters.ToArray());

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                T? dto = (T?)Activator.CreateInstance(typeof(T));
                MapToReaderDto(reader, dto);
                result.Add(dto);
            }

            await sqlConnection.CloseAsync();

            return result;
        }

        public async Task<T?> SqlCommand_Get_Single_Data<T>(
                     string connectionString,
                     string storedProcName,
                     List<SqlParameter> parameters)

        {
            var result = new List<T?>();

            var sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
            var cmd = new SqlCommand()
            {
                Connection = sqlConnection,
                CommandText = storedProcName,
                CommandType = CommandType.StoredProcedure
            };
            FixParams(parameters);
            cmd.Parameters.AddRange(parameters.ToArray());

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                var dto = (T?)Activator.CreateInstance(typeof(T));
                MapToReaderDto(reader, dto);
                result.Add(dto);
            }

            await sqlConnection.CloseAsync();

            return result.FirstOrDefault();
        }


        public async Task<T?> SqlCommand_Get_Single_Data_Type<T>(
                     string connectionString,
                     string storedProcName,
                     List<SqlParameter> parameters)

        {
            var result = new List<T?>();

            var sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
            var cmd = new SqlCommand()
            {
                Connection = sqlConnection,
                CommandText = storedProcName,
                CommandType = CommandType.StoredProcedure
            };
            FixParams(parameters);
            cmd.Parameters.AddRange(parameters.ToArray());

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                object? safeValue = MapToReaderDto<T>(reader);

                result.Add((T?)safeValue);
            }

            await sqlConnection.CloseAsync();

            return result.FirstOrDefault();
        }



        public async Task SqlCommand_No_Data(
                     string connectionString,
                     string storedProcName,
                     List<SqlParameter> parameters)

        {

            var sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
            var cmd = new SqlCommand()
            {
                Connection = sqlConnection,
                CommandText = storedProcName,
                CommandType = CommandType.StoredProcedure
            };
            FixParams(parameters);
            cmd.Parameters.AddRange(parameters.ToArray());

            await cmd.ExecuteReaderAsync();

            await sqlConnection.CloseAsync();
        }

        private static void FixParams(List<SqlParameter> parameters)
        {
            if (parameters.Any(p => p.Value == DBNull.Value || p.Value == null))
            {
                foreach (var param in parameters.Where(p => p.Value == DBNull.Value || p.Value == null))
                {
                    param.Value = DBNull.Value;
                };
            }
        }

        private static object? MapToReaderDto<T>(IDataReader reader)
        {
            Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            object? safeValue = (reader[0] == DBNull.Value) || (reader[0] == null) ? null : Convert.ChangeType(reader[0], t);
            return safeValue;
        }

        private static void MapToReaderDto<T>(IDataReader reader, T dto)
        {
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var ColumnName = prop.Name.ToUpper();

                if (!reader.HasColumn(ColumnName))
                {
                    continue;
                }
                var value = reader[ColumnName];

                if (reader.HasValue(ColumnName))
                {
                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    object? safeValue = (value == DBNull.Value) || (value == null) ? null : Convert.ChangeType(value, t);

                    prop.SetValue(dto, safeValue);
                }

                else
                {
                    value = null;
                    prop.SetValue(dto, value);
                }




            }
        }
    }

    public static class SqlDataReaderExtention
    {
        public static bool HasColumn(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper().Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasValue(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper().Equals(columnName, StringComparison.OrdinalIgnoreCase) && !reader.IsDBNull(i))
                {
                    return true;
                }
            }
            return false;
        }

    }


}
