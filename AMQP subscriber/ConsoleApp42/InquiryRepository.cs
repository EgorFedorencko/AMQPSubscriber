using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp42
{
    class InquiryRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString;
        public Guid AddInquiry(Inquiry inquiry)
        {
            using (IDbConnection db = new SqlConnection("Server=tcp:testdbms157.database.windows.net,1433;Initial Catalog=TestInfrastructureDBMS;Persist Security Info=False;User ID=admindbms;Password=6iK=Bb!9#dyv2.P;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("client_id", inquiry.client_id, DbType.Guid, direction: ParameterDirection.Input);
                dynamicParams.Add("department_address", inquiry.department_address, DbType.String, direction: ParameterDirection.Input);
                dynamicParams.Add("amout", inquiry.amout, DbType.Decimal, direction: ParameterDirection.Input);
                dynamicParams.Add("UAN",inquiry.UAN , DbType.String, direction: ParameterDirection.Input);
                dynamicParams.Add("status",0, DbType.Int32, direction: ParameterDirection.Input);
                dynamicParams.Add("Id", DBNull.Value ,DbType.Guid, direction: ParameterDirection.Output);
                db.Execute("dbo.ADD_INQUIRY", dynamicParams, commandType: CommandType.StoredProcedure);
                return dynamicParams.Get<Guid>("Id");
            }
 
        }
    }
}
