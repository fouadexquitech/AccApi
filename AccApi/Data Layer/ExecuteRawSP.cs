using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Threading.Tasks;
using System;

namespace AccApi.Data_Layer
{
    public class ExecuteRawSP
    {

        public async Task<List<T>> ExecuteRawStoredProcedure<T>(DbContext _context, string prodecureName, List<SqlParameter> parameters, Func<DbDataReader, T> map)
        {

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = prodecureName;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 0;

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var entities = new List<T>();

                    while (await result.ReadAsync())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }

        }


    }
}
