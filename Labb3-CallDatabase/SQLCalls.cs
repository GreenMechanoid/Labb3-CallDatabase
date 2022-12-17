// .Net22 Daniel Svensson
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Labb3_CallDatabase
{
    internal class SQLCalls
    {
        SqlCommand cmd;
        SqlConnection conn = new("Data Source=GREENMECHANOID;" + "Initial Catalog=Labb2-SchoolDatabase;" + "Trusted_Connection=true");
        public SQLCalls()
        {
            cmd = new()
            {
                CommandTimeout = 60,// if command is endless or unresponsive, it will abort after 60sec
                Connection = conn,
                CommandType = CommandType.Text
            };
        }
        public void GetEmployees()
        {
            int Selection = 0; bool CorrectSelection = false, keepLooping = true;
            string SQLCountString, SQLQuery;

            do
            {
                //Cases represents the diffrent options, and has local SQL Query to reflect that choise
                switch (Selection)
                {
                    default:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Do you wish to see all employees or just a certain type of employee?");
                            Console.WriteLine("1: All Employees");
                            Console.WriteLine("2: Only Teachers");
                            Console.WriteLine("3: Only secretaries");
                            Console.WriteLine("4: Only Principals");
                            Console.WriteLine("5: Only Janitors");
                            Console.WriteLine("6: Only Cafeteria Staff");
                            Console.WriteLine("9: Return to last menu");
                            int.TryParse(Console.ReadLine(), out Selection);
                            if (Selection == 1 || Selection == 2 || Selection == 3 ||
                            Selection == 4 || Selection == 5 || Selection == 6 || Selection == 9)
                            {
                                CorrectSelection = true;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input, Try again");
                            }
                        } while (!CorrectSelection);
                        break;
                    case 1:
                        SQLCountString = "SELECT Count(*) FROM Staff";
                        SQLQuery = "SELECT StaffID, Firstname, Lastname, Occupation FROM Staff ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 2:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Teacher'";
                        SQLQuery = "SELECT StaffID, Firstname, Lastname, Occupation FROM Staff Where Occupation = 'Teacher' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 3:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Secretary'";
                        SQLQuery = "SELECT StaffID, Firstname, Lastname, Occupation FROM Staff Where Occupation = 'Secretary' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 4:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Principal'";
                        SQLQuery = "SELECT StaffID, Firstname, Lastname, Occupation FROM Staff Where Occupation = 'Principal' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 5:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Janitor'";
                        SQLQuery = "SELECT StaffID, Firstname, Lastname, Occupation FROM Staff Where Occupation = 'Janitor' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 6:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation LIKE '%Cafeteria%'";
                        SQLQuery = "SELECT StaffID, Firstname, Lastname, Occupation FROM Staff Where Occupation LIKE '%Cafeteria%' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 9:
                        Console.Clear();
                        keepLooping = false;
                        break;
                }
            } while (keepLooping);

        }

        public void TryConnection(string SQLCountString, string SQLDataSelection)
        {
            SqlDataReader dr; // Middle hand for calls between C# and SQL Select request
            try
            {
                cmd.CommandText = SQLCountString;
                conn.Open();
                //If Statement Checks incoming string for what table it's about
                if (conn.State == ConnectionState.Open && SQLCountString.Contains("FROM Staff") == true)
                {
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    // For loop to read everything from the table  
                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read(); // Read one row from the table  
                        Console.WriteLine("StaffID: {0}\nName: {1} {2}\nOccupation: {3}\n-----------", dr[0], dr[1], dr[2], dr[3], dr[4]);
                    }
                }
                else if (conn.State == ConnectionState.Open && SQLCountString.Contains("FROM Grade WHERE GradingDate") == true)
                {
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read();
                        Console.WriteLine("Student: {0} {1}\nCoursename: {2}\nGrade: {3}\nGradeingDate: {4}\n--------", dr[0], dr[1], dr[2], dr[3], dr[4]);
                    }
                }
                else if (conn.State == ConnectionState.Open && SQLCountString.Contains("FROM Grade Group by Grade.CourseID") == true)
                {
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                    for (int i = 0; i <= iCount; i++)
                    {
                        dr.Read();
                        Console.WriteLine("Coursename: {0}\n  Lowest Grade: {1}\n Average Grade: {2}\n Top Grade: {3} \n-----------", dr[0], dr[1], dr[2], dr[3]);
                    }
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void GetGradesMonth()
        {
            string SQLCountString, SQLQuery;

            SQLCountString = "SELECT Count(*) FROM Grade WHERE GradingDate BETWEEN DATEADD(mm, DATEDIFF(mm,0,getdate())-1, 0) AND DATEADD(mm, 1, DATEADD(mm, DATEDIFF(mm,0,getdate())-1, 0))";
            SQLQuery = "SELECT Students.Firstname,Students.Lastname ,Course.Coursename ,Grade.Grade ,Grade.GradingDate FROM Grade inner join Students on Grade.StudentID=Students.StudentID left join Course on Course.CourseID=Grade.GradeID WHERE GradingDate BETWEEN DATEADD(mm, DATEDIFF(mm,0,getdate())-1, 0) AND DATEADD(mm, 1, DATEADD(mm, DATEDIFF(mm,0,getdate())-1, 0))";
            TryConnection(SQLCountString, SQLQuery);
        }

        public void GetCourseGrades()
        {
            string SQLCountString, SQLQuery;

            SQLCountString = "SELECT Count(*) FROM Grade Group by Grade.CourseID";
            SQLQuery = "SELECT Course.Coursename ,MIN(Grade.Grade) as 'Lowest Grade', MAX(Grade.Grade) 'Average Grade',AVG(Grade.Grade) 'Top Grade' FROM Grade INNER JOIN Course on Course.CourseID = Grade.CourseID Group by Course.Coursename";
            TryConnection(SQLCountString, SQLQuery);
        }

        public void AddStudent()
        {
            bool correctInput = false, keepLooping = true;
            string tempString;
            List<string> inputdata = new();


            do
            {
                if (inputdata.Count > 0)
                {
                    inputdata.Clear();
                }
                Console.WriteLine("input Students Firstname");
                inputdata.Add(Console.ReadLine());
                Console.WriteLine("input Students Lastname");
                inputdata.Add(Console.ReadLine());
                Console.WriteLine("Enter DateofBirth 'XXXX-XX-XX'");
                tempString = Console.ReadLine();
                if (tempString.Length != 10)
                {
                    Console.Clear();
                    do
                    {
                        Console.WriteLine("Wrong input. Needs to be in this format xxxx-xx-xx");
                        tempString = Console.ReadLine();
                        if (tempString.Length == 10)
                        {
                            correctInput = true;
                        }
                    } while (!correctInput);
                    correctInput = false;
                }
                inputdata.Add(tempString);
                Console.WriteLine("Enter Class number 'Example 2A'");
                inputdata.Add(Console.ReadLine());
                Console.WriteLine("please enter the Adress of the student 'Ex:Barley street 27'");
                inputdata.Add(Console.ReadLine());
                Console.WriteLine("and lastly Enter the Postal code of the adress");
                inputdata.Add(Console.ReadLine());
                Console.Clear();
                foreach (var item in inputdata)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("is this Information correct? Y/N");
                do
                {
                    ConsoleKey tempkey = Console.ReadKey(false).Key;
                    if (tempkey == ConsoleKey.Y)
                    {
                        correctInput = true;
                        keepLooping = false;
                    }
                    else if (tempkey == ConsoleKey.N)
                    {
                        Console.Clear();
                        Console.WriteLine("Let'start from the top then: \n");
                        break;
                    }
                } while (!correctInput);

            } while (keepLooping);

            try
            {
                //cmd.CommandText = "SELECT COUNT(*) FROM Students";
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    cmd.CommandText = $"INSERT INTO Students (Students.Firstname,Students.Lastname,Students.Dateofbirth,Students.Classnumber,Students.Adress,Students.PostalCode) " +
                        $"VALUES ('{inputdata[0].Replace("'", "''")}','{inputdata[1].Replace("'", "''")}',{inputdata[2]},'{inputdata[3].Replace("'", "''")}','{inputdata[4].Replace("'", "''")}','{inputdata[5].Replace("'", "''")}')";
                    cmd.ExecuteScalar();

                    Console.WriteLine($"Student: {inputdata[0] + " " + inputdata[1]} has been added to the Database");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
