using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySql_Connection
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = "server=localhost;User Id=sa;password=123456;Database=datasync";
            MySqlConnection mycon = new MySqlConnection(constr);
            mycon.Open();
            MySqlCommand mycmd = new MySqlCommand("insert into datasyn_log(comId) values('90')", mycon);
            mycmd.ExecuteNonQuery();
            //mycmd =new MySqlCommand("update datasyn_log set comId='100' where comId='90'",mycon);
            //mycmd.ExecuteNonQuery();
            mycmd = new MySqlCommand("delete from datasyn_log  where comId='100'", mycon);
            mycmd.ExecuteNonQuery();

            //if (mycmd.ExecuteNonQuery() > 0)
            //{
            //    Console.WriteLine("数据插入成功！");
            //}
            //Console.ReadLine();
            mycon.Close();
        }
    }
}
