using Microsoft.Data.SqlClient;

namespace Labb3_CallDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection MyCon = new SqlConnection("Data Source=GREENMECHANOID;" + "Initial Catalog=Labb2-SchoolDatabase;" + "Trusted_Connection=true");

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
                        Console.WriteLine("1: Listing of personell"); //SQL
                        Console.WriteLine("2: Listing of students"); //Entity
                        Console.WriteLine("3: Listing class of students");//Entity
                        Console.WriteLine("4: List Grades set previous month"); //SQL
                        Console.WriteLine("5: List Average grade in all classes"); //SQL
                        Console.WriteLine("6: Add new Student"); //SQL
                        Console.WriteLine("7: Add new personell");//Entity
                        Console.WriteLine("9: Exit program.");
                        int.TryParse(Console.ReadLine(), out navigation);
                        if (navigation == 1 || navigation == 2 || navigation == 3 ||
                            navigation == 4 || navigation == 5 || navigation == 6 ||
                            navigation == 7 || navigation == 9)
                        {
                                correctInput = true;
                        }
                        else
                        {
                                Console.WriteLine("Wrong input, Try again.");
                        }

                        } while (!correctInput);
                        break;
                    case 1:
                        goto default;
                    case 2:
                        goto default;
                    case 3:
                        goto default;
                    case 4:
                        goto default;
                    case 5:
                        goto default;
                    case 6:
                        goto default;
                    case 7:
                        goto default;
                    case 9:
                        keepLooping = false;
                        break;
                }


            } while (keepLooping);
        }
    }
}