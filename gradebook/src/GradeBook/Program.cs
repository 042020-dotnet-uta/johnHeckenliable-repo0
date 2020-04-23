using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook gradeBook = new DiskBook("My Grade Book");
            gradeBook.GradeAdded += OnGradeAdded;

            EnterGrades(gradeBook);

            var stats = gradeBook.GetStatistics();

            Console.WriteLine($"Highest Grade = {stats.High:N1}");
            Console.WriteLine($"Lowest Grade = {stats.Low:N1}");
            Console.WriteLine($"Average Grade = {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        private static void EnterGrades(IBook gradeBook)
        {
            do
            {
                Console.Write("Enter a grade (Enter 'q' to quit): ");
                var input = Console.ReadLine();

                if ("q" == input)
                    break;

                try
                {
                    var grade = double.Parse(input);
                    gradeBook.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }

        static void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("A grade was added");
        }
    }
}