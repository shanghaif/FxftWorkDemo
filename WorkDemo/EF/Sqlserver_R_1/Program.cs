using System;
using System.Data.SqlClient;

namespace Sqlserver_R_1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connStr = @"Data Source=.;Initial Catalog=CRUD;Persist Security Info=True;User ID=sa;Password=vagrant123,.";//连接字符串  
                                                                                                                                     //const string connStr = @"Server=localhost;Option=16834;Database=myDataBase" ;

            // < add name = "ConnectionString" connectionString = "Server=localhost;Option=16834;Database=myDataBase" providerName = "MySql.Data.MySqlClient" />
            SqlConnection mySqlConnection = new SqlConnection();
            mySqlConnection.ConnectionString = connStr;//新建连接对象并设置其连接字符串属性  
            string sqlStr = @"SELECT *  from 
            Figure";//SQL语句  
            try
            {
                mySqlConnection.Open();//打开连接  
                SqlCommand mycmd = new SqlCommand(sqlStr, mySqlConnection);//新建SqlCommand对象  
                SqlDataReader sdr = mycmd.ExecuteReader();//ExecuteReader方法将 CommandText 发送到 Connection 并生成一个 SqlDataReader  
                Console.WriteLine("人物 \t\t\t属性\t\t\t星球");
                while (sdr.Read())
                {
                    Console.WriteLine("{0}\t\t\t{1}\t\t\t{2}", sdr[0], sdr[1], sdr[2]);//循环读取数据  

                }
                sdr.Close();//读取完毕即关闭  
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
