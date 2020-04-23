using System;
using System.IO;
using System.Collections.Generic;

namespace GradeBook
{
    public class DiskBook : Book
    {
        public override event GradeAddedDelegate GradeAdded;

        public DiskBook(string name) : base(name)
        {
            Name = name;
            //grades = new List<double>();
        }

        public override void AddGrade(double grade)
        {
            //Validate grade
            if (grade >= 0 && grade <= 100)
            {
                using (var writer = File.AppendText($"{Name}.txt"))
                {
                    //Add grade to the file of grades
                    writer.WriteLine(grade);

                    if (GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                //read a grade from the file
                var line = reader.ReadLine();
                while (null != line)
                { 
                    var number = double.Parse(line);
                    result.Add(number);

                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }
}