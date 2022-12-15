// .Net22 Daniel Svensson
using Labb3_CallDatabase.Data;
using Labb3_CallDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_CallDatabase
{
    internal class EFCalls
    {

        public void ListAllStudents()
        {
            int selection;
            
            bool keepLooping = true;
            Console.WriteLine("Select Sorting Method");
            Console.WriteLine("1: By Firstname");
            Console.WriteLine("2: By Lastname");

            using (var db = new Labb3Context())
            {
                do
                {
                    int.TryParse(Console.ReadLine(), out selection);

                    if (selection == 1) //sort by firstname
                    {
                        Console.WriteLine("in Ascending or Descending?");
                        Console.WriteLine("1: Ascending");
                        Console.WriteLine("2: Descending");
                        int.TryParse(Console.ReadLine(), out selection);
                        if (selection == 1)
                        {
                            var query = from b in db.Students
                                        orderby b.Firstname ascending
                                        select b;
                            keepLooping = false;
                            Console.WriteLine("All Students in the DB");
                            foreach (var item in query)
                            {
                                Console.WriteLine("Firstname: " + item.Firstname + " Lastname: " + item.Lastname + " \nDateofbirth: " + item.Dateofbirth + " \nClass: " + item.Classnumber + " \nAdress: " + item.Adress + " \nPostalCode: " + item.PostalCode + "\n-----------------");
                            }
                        }
                        else if (selection == 2)
                        {
                            var query = from b in db.Students
                                        orderby b.Firstname descending
                                        select b;
                            keepLooping = false;
                            Console.WriteLine("All Students in the DB");
                            foreach (var item in query)
                            {
                                Console.WriteLine("Firstname: " + item.Firstname + " Lastname: " + item.Lastname + " \nDateofbirth: " + item.Dateofbirth + " \nClass: " + item.Classnumber + " \nAdress: " + item.Adress + " \nPostalCode: " + item.PostalCode + "\n-----------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("wrong input.");
                            int.TryParse(Console.ReadLine(), out selection);
                        }
                    }
                    else if (selection == 2) // sort by lastname
                    {
                        Console.WriteLine("in Ascending or Descending?");
                        Console.WriteLine("1: Ascending");
                        Console.WriteLine("2: Descending");
                        int.TryParse(Console.ReadLine(), out selection);
                        if (selection == 1)
                        {
                            var query = from b in db.Students
                                        orderby b.Lastname ascending
                                        select b;
                            keepLooping = false;
                            Console.WriteLine("All Students in the DB");
                            foreach (var item in query)
                            {
                                Console.WriteLine("Firstname: " + item.Firstname + " Lastname: " + item.Lastname + " \nDateofbirth: " + item.Dateofbirth + " \nClass: " + item.Classnumber + " \nAdress: " + item.Adress + " \nPostalCode: " + item.PostalCode + "\n-----------------");
                            }
                        }
                        else if (selection == 2)
                        {
                            var query = from b in db.Students
                                        orderby b.Lastname descending
                                        select b;
                            keepLooping = false;
                            Console.WriteLine("All Students in the DB");
                            foreach (var item in query)
                            {
                                Console.WriteLine("Firstname: " + item.Firstname + " Lastname: " + item.Lastname + " \nDateofbirth: " + item.Dateofbirth + " \nClass: " + item.Classnumber + " \nAdress: " + item.Adress + " \nPostalCode: " + item.PostalCode + "\n-----------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("wrong input.");
                            int.TryParse(Console.ReadLine(), out selection);
                        }

                    }
                    else
                    {
                        Console.WriteLine("wrong input.");
                        int.TryParse(Console.ReadLine(), out selection);
                    }
                } while (keepLooping);


            }
        }

        public void ListAllInClass()
        {

            using (var db = new Labb3Context())
            {
                var query = from c in db.Courses
                            select c;
                foreach (var item in query)
                {
                    Console.WriteLine("CourseID: " + item.CourseId + " Coursename: " + item.Coursename);
                }
                Console.WriteLine("Please select the ID of the course you want to check.");
                int.TryParse(Console.ReadLine(), out int result);

                var query2 = from s in db.Students
                        join g in db.Grade on s.StudentId equals g.StudentId
                        join c in db.Courses on g.CourseId equals c.CourseId
                        where c.CourseId == result
                        select new 
                        {
                            c.Coursename,
                            s.Firstname,
                            s.Lastname
                        };

                foreach (var item in query2)
                {
                    Console.WriteLine($"Course Name: {item.Coursename} Studentname {item.Firstname} {item.Lastname}");
                }
            }

        }
    
        public void AddNewStaff()
        {


            using (var db = new Labb3Context())
            {
                Console.WriteLine("Please enter the First name of the new staff member");
                string fname = Console.ReadLine();
                Console.WriteLine("Please enter the Last name of the new staff member");
                string Lname = Console.ReadLine();
                Console.WriteLine("please enter the Occupation");
                string occupation = Console.ReadLine();
                Console.WriteLine("Enter the Salary (before tax)");
                float salary;
                DateTime date;
                do
                {
                    if (float.TryParse(Console.ReadLine(), out salary)) 
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("wrong input, try again");
                    }
                } while (true);
                Console.WriteLine("Enter Dateofbirth");
                do
                {
                    if (DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("wrong input, try again - XXXX-XX-XX");
                    }
                } while (true);
                Console.WriteLine("Please enter the adress of the new staff member - IE: bass street 25");
                string adress = Console.ReadLine();
                Console.WriteLine("Please enter the postalcode of the new staff member - IE: 88855");
                string postal = Console.ReadLine();

                var Staff = db.Set<staff>();
                Staff.Add(new staff { Firstname = fname, Lastname = Lname , Occupation = occupation, Salary = salary, Dateofbirth = date , Adress = adress, PostalCode = postal});
                try
                {
                    db.SaveChanges();
                    Console.WriteLine($"{fname} {Lname} has been added to the Database");
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    throw;
                }

            }

        }
    }
}
