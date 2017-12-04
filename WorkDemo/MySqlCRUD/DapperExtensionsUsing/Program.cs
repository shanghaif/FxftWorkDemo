using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using MySql.Data.MySqlClient;

namespace DapperExtensionsUsing
{
    class Program
    {
        //string connStr = "server=localhost;User Id=sa;password=123456;Database=datasync";
        static string _connectionString = @"server=localhost;User Id=sa;password=123456;Database=datasync";

        static void Main(string[] args)
        {
            //using (MySqlConnection cn = new MySqlConnection(_connectionString))
            //{
            //    cn.Open();
            //    var predicate = Predicates.Field<Person>(f => f.Active, Operator.Eq, true);
            //    IEnumerable<Person> list = cn.GetList<Person>(predicate);
            //    cn.Close();
            //}

            //using (MySqlConnection cn = new MySqlConnection(_connectionString))
            //{
            //    cn.Open();
            //    Person person = new Person { FirstName = "Foo", LastName = "Bar" };
            //    int id = cn.Insert(person);
            //    cn.Close();
            //}


            //using (MySqlConnection cn = new MySqlConnection(_connectionString))
            //{
            //    cn.Open();
            //    var predicate = Predicates.Field<Person>(f => f.FirstName, Operator.Eq, "小白");
            //    IEnumerable<Person> list = cn.GetList<Person>(predicate);
            //    cn.Close();
            //}


        }
    }
}
