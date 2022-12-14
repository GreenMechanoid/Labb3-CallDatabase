
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Labb3_CallDatabase.Data
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
                        SQLQuery = "SELECT StaffID, Fullname, Occupation FROM Staff ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 2:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Teacher'";
                        SQLQuery = "SELECT StaffID, Fullname, Occupation FROM Staff Where Occupation = 'Teacher' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 3:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Secretary'";
                        SQLQuery = "SELECT StaffID, Fullname, Occupation FROM Staff Where Occupation = 'Secretary' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 4:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Principal'";
                        SQLQuery = "SELECT StaffID, Fullname, Occupation FROM Staff Where Occupation = 'Principal' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 5:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation = 'Janitor'";
                        SQLQuery = "SELECT StaffID, Fullname, Occupation FROM Staff Where Occupation = 'Janitor' ORDER BY StaffID";
                        TryConnection(SQLCountString, SQLQuery);
                        Thread.Sleep(5000);
                        goto default;
                    case 6:
                        SQLCountString = "SELECT Count(*) FROM Staff Where Occupation LIKE '%Cafeteria%'";
                        SQLQuery = "SELECT StaffID, Fullname, Occupation FROM Staff Where Occupation LIKE '%Cafeteria%' ORDER BY StaffID";
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
                        Console.WriteLine("StaffID: {0}\tName: {1}\tOccupation: {2}", dr[0], dr[1], dr[2]);
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
                        Console.WriteLine("Student: {0}\tCoursename: {1}\tGrade: {2}\tGradeingDate: {3}", dr[0], dr[1], dr[2], dr[3]);
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
                        Console.WriteLine("Coursename: {0}  Lowest Grade: {1} Average Grade: {2} Top Grade: {3} ", dr[0], dr[1], dr[2], dr[3]);
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
            SQLQuery = "SELECT Students.Fullname,Course.Coursename ,Grade.Grade ,Grade.GradingDate FROM Grade inner join Students on Grade.StudentID=Students.StudentID left join Course on Course.CourseID=Grade.GradeID WHERE GradingDate BETWEEN DATEADD(mm, DATEDIFF(mm,0,getdate())-1, 0) AND DATEADD(mm, 1, DATEADD(mm, DATEDIFF(mm,0,getdate())-1, 0))";
            TryConnection(SQLCountString, SQLQuery);
        }

        public void GetCourseGrades()
        {
            string SQLCountString, SQLQuery;

            SQLCountString = "SELECT Count(*) FROM Grade Group by Grade.CourseID";
            SQLQuery = "SELECT Course.Coursename ,MIN(Grade.Grade) as 'Lowest Grade', MAX(Grade.Grade) 'Average Grade',AVG(Grade.Grade) 'Top Grade' FROM Grade INNER JOIN Course on Course.CourseID = Grade.CourseID Group by Course.Coursename";
            TryConnection(SQLCountString, SQLQuery);
        }
    }
}
