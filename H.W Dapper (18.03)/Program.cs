using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;

namespace H.W_Dapper__18._03_
{
    class Program
    {
        private const string _constring = @"Data source = WIN-C6IAG73172R\SQLEXPRESS; initial catalog = AlifAcademy; integrated security = true;";
        static void Main(string[] args)
        {
            while(true)
            {
                int number;
                Console.WriteLine("Выберите нужную вам Операцию:\n" +
                              "1.Показать все - нажмите 1;\n" +
                              "2.Добавить - нажмите 2;\n" +
                              "3.Обновить - нажмите 3;\n" +
                              "4.Удалить - нажмите 4;");
                number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        ShowAll();
                        break;
                    case 2:
                        Creat();
                        break;
                    case 3:
                        Console.Write("Введите Id:");
                        Update(int.Parse(Console.ReadLine()));
                        break;
                    case 4:
                        Console.Write("Введите Id:");
                        Delete(int.Parse(Console.ReadLine()));
                        break;
                    default:
                        Console.WriteLine("Введено некорректное число либо символ!");
                        break;


                }
            }
        }

        public static void ShowAll()
        {
            Console.Clear();
            try
            {
                using (IDbConnection connection = new SqlConnection(_constring))
                {
                    var result = connection.Query<Customers>("Select * from Customers").ToList();
                    foreach (var item in result)
                    {
                        Console.WriteLine($"FirstName: " + item.FirstName +
                            "\nLastName: " + item.LastName + "\nMidlleName: "
                            + item.MidlleName + "\nDateOfBirth: " + item.DateOfBirth + "\nDocumentNumber" + item.DocumentNumber);
                    }

                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static void Creat()
        {
            Console.Clear();
            var @newStudent = new Customers();
            Console.Write("Enter your FirsName = ");
            newStudent.FirstName = Console.ReadLine();
            Console.Write("Enter your LastName = ");
            newStudent.LastName = Console.ReadLine();
            Console.Write("Enter your MidldleName = ");
            newStudent.MidlleName = Console.ReadLine();
            Console.Write("Enter your Date Of Birth = ");
            newStudent.DateOfBirth = DateTime.Parse(Console.ReadLine());
            using (IDbConnection con = new SqlConnection(_constring))
            {

                var result = con.ExecuteScalar<Customers>("Insert into Customers(FirstName, LastName, MiddleName, DateOfBirth, DocumentNumber) " +
                                                       "values (@FirstName, @LastName, @MidlleName, @DateOfBirth, @DocumentNumber)", newStudent);

                Console.WriteLine(result);
                Console.WriteLine("Клиент успешно добавлен!");
                Console.ReadKey();
            }
        }

        public static void Update(int Id)
        {
            Console.Clear();
            var @newStudent = new Customers();
            Console.Write("Enter new FirstName = ");
            newStudent.FirstName = Console.ReadLine();
            Console.Write("Enter new LastName = ");
            newStudent.LastName = Console.ReadLine();
            Console.Write("Enter new MiddleName = ");
            newStudent.MidlleName = Console.ReadLine();
            using (IDbConnection con = new SqlConnection(_constring))
            {
                try
                {

                    var result = con.ExecuteScalar<Customers>($"Update Customers Set FirstName = @FirstName, LastName = @LastName,  MiddleName = @MidlleName where Id = '{Id}'", newStudent);


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка - {ex.Message}");
                }
                Console.WriteLine("Обновление прошло успешно!");
                Console.ReadKey();
            }
        }

        public static void Delete(int Id)
        {
            Console.Clear();
            using (IDbConnection con = new SqlConnection(_constring))
            {
                try
                {
                    var result = con.ExecuteScalar<Customers>($"Delete from Customers Where Id = {Id}");
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Ошибка - {ex.Message}");
                }

            }

            Console.WriteLine("Удаление прошло успешно!");
            Console.ReadKey();
        }

    }

    public class Customers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidlleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DocumentNumber { get; set; }
    }
}
