using System;
using System.Collections.Generic;
//using System.Data;
//using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

namespace Dapper_Using
{
    class Program
    {
        static string connectionString = "server=localhost;User Id=sa;password=123456;Database=datasync";

        static void Main(string[] args)
        {
            #region 新增单条记录

            Person person = new Person();
            person.Name = "9999";
            person.Remark = "标记";
            Insert(person);

            #endregion


            #region 批量新增

            List<Person> persons = new List<Person>();
            Person person1 = new Person();
            person1.Name = "one";
            persons.Add(person1);
            Person person2 = new Person();
            person2.Name = "two";
            persons.Add(person2);
            Insert(persons);

            #endregion

            var personList = Query();

            foreach (var item in personList)
            {

                //Delete(item);
                var conditionsQuery = Query(item);
            }


            //Delete(personList);

            var personsList = QueryIn();

            int[] ids = {50, 51, 52, 53, 54, 55};

            var personsList2 = QueryIn(ids);

        }



        #region 新增

        /// <summary>
        /// 新增单条记录
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int Insert(Person person)
        {
            //using (IDbConnection connection = new MySqlConnection(connectionString))
            //{
            //    return connection.Execute("insert into Person(Name,Remark) values(@Name,@Remark)", person);
            //}
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.Execute("insert into Person(Name) values(@Name)", person);
            }

        }

        /// <summary>
        /// 批量新增Person数据，返回影响行数
        /// </summary>
        /// <param name="persons"></param>
        /// <returns>影响行数</returns>
        public static int Insert(List<Person> persons)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.Execute("insert into Person(Name,Remark) values(@Name,@Remark)", persons);
            }
        }

        #endregion



        #region 查询

        /// <summary>
        /// 无参查询所有数据
        /// </summary>
        /// <returns></returns>
        public static List<Person> Query()
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.Query<Person>("select * from Person").ToList();
            }
        }

        /// <summary>
        /// 查询指定数据
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static Person Query(Person person)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.Query<Person>("select * from Person where id=@ID", person).SingleOrDefault();
            }
        }


        #region In

        /// <summary>
        /// In操作
        /// </summary>
        public static List<Person> QueryIn()
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var sql = "select * from Person where id in @ids";
                //参数类型是Array的时候，dappper会自动将其转化
                return connection.Query<Person>(sql, new { ids = new int[2] { 53, 54 } }).ToList();
            }
        }

        //可再进行封装，条件定死了不科学
        public static List<Person> QueryIn(int[] ids)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var sql = "select * from Person where id in @ids";
                //参数类型是Array的时候，dappper会自动将其转化
                return connection.Query<Person>(sql, new { ids }).ToList();
            }
        }


        #endregion


        #endregion



        #region 删除

        /// <summary>
        /// 单条删除
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int Delete(Person person)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.Execute("delete from Person where id=@ID", person);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        public static int Delete(List<Person> persons)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.Execute("delete from Person where id=@ID", persons);
            }
        }

        #endregion


    }
}
