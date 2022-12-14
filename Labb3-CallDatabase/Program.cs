using Labb3_CallDatabase.Data;
using Microsoft.Data.SqlClient;

namespace Labb3_CallDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            SQLCalls sql = new SQLCalls();
            EFCalls efcalls = new EFCalls();
            bool keepLooping = true, correctInput = false;
            int navigation = 0;
            do
            {
                
                switch (navigation)
                {
                    default:
                        navigation = 0; keepLooping = true; correctInput = false;
                        do
                        {
                        Console.WriteLine("Main Menu . Please choose a option");
                        Console.WriteLine("1: Listing of Employees"); //SQL
                        Console.WriteLine("2: Listing of Students"); //Entity
                        Console.WriteLine("3: Listing class of students");//Entity
                        Console.WriteLine("4: List Grades set previous month"); //SQL
                        Console.WriteLine("5: List Average grade in all classes"); //SQL
                        Console.WriteLine("6: Add new Student"); //SQL
                        Console.WriteLine("7: Add new Employee");//Entity
                        Console.WriteLine("9: Exit program.");
                        int.TryParse(Console.ReadLine(), out navigation); // error checking on input, keeps looping until correct input
                        if (navigation == 1 || navigation == 2 || navigation == 3 ||
                            navigation == 4 || navigation == 5 || navigation == 6 ||
                            navigation == 7 || navigation == 9)
                        {
                                correctInput = true;
                        }
                        else
                        {
                                Console.Clear(); // clears to not flood the console with same info
                                Console.WriteLine("Wrong input, Try again.");
                        }

                        } while (!correctInput);
                        break;
                    case 1:
                        Console.Clear();
                        sql.GetEmployees();
                        goto default;
                    case 2:
                        goto default;
                    case 3:
                        goto default;
                    case 4:
                        sql.GetGradesMonth();
                        Thread.Sleep(5000);
                        Console.Clear();
                        goto default;
                    case 5:
                        sql.GetCourseGrades();
                        goto default;
                    case 6:
                        goto default;
                    case 7:
                        goto default;
                    case 9:
                        keepLooping = false;
                        break;
                }


            } while (keepLooping); // infinite loop until user wants to exit
        }
    }
}