using System;
using System.Data;
using System.Data.SqlClient;
namespace Sqlserver_R_2
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connStr = @"Data Source=.;Initial Catalog=CRUD;Persist Security Info=True;User ID=sa;Password=vagrant123,.";
            SqlConnection mySqlConnection = new SqlConnection(connStr);//新建连接对象  
            string sqlStr = @"SELECT *  from 
            Figure";//SQL语句  
            try
            {
                mySqlConnection.Open();//打开连接  
                SqlDataAdapter sda = new SqlDataAdapter(sqlStr, mySqlConnection);//新建SqlDataAdapter对象  
                DataSet ds = new DataSet();//新建Dataset对象  
                sda.Fill(ds);//填充DataSet对象  
                Console.WriteLine("人物\t\t属性");
                int cnt = ds.Tables[0].Rows.Count;//读取行数  
                for (int ix = 0; ix != cnt; ++ix)
                    Console.WriteLine("{0}\t\t{1}", ds.Tables[0].Rows[ix].ItemArray[1],
                        ds.Tables[0].Rows[ix].ItemArray[2]);//循环读取数据 注意索引范围  
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();//关闭连接  
            }
            Console.ReadKey();
        }
    }
}
