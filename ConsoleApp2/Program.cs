using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {

        static void Main(string[] args)
        {
            string svrName = "your_ip";
            string userId = "your_id";
            string userPw = "your_pw";

            // Description
            // this is normally setting Oracle SE version. if you have used XE version, you have to change SERVICE_NAME orcl to XE.

            string connectionString = string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id={1};Password={2};", svrName, userId, userPw);
            //string connectionString = string.Format("user id={0};password={1}; data source={2}:1521/orcl", userId, userPw, svrName);


            using (OracleConnection oracleConnection =
                new OracleConnection(connectionString))
            {
                try
                {
                    DataSet ds = new DataSet();
                    string SQL = "SELECT * FROM BOOKCD where BK_CD = '9788956698649'";
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = new OracleCommand(SQL, oracleConnection);

                    da.Fill(ds, "test");

                    Console.WriteLine(string.Format("값 = {0}", ds.Tables["test"].Rows[0][0].ToString()));

                    // 이전 예제를 보면 DataROW를 'Foreach'로 호출하여 변수를 편하게 사용할 수 있다. (In previous examples, DataROW can be called 'Foreach' to make the variables easier to use.)
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    oracleConnection.Close(); //using으로 묶여있으므로 굳이 예외처리를 하지 않아도 자동으로 연결을 해제한다. (As it is blocked with using, disconnect it automatically without having to make an finally(close) exception.)
                }

            }
                
        }
    }
}

