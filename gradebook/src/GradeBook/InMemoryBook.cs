using System;
using System.Collections.Generic;

namespace GradeBook
{
    public class InMemoryBook : Book
    {
        public override event GradeAddedDelegate GradeAdded;
        List<double> grades;

        public InMemoryBook(string name) : base(name)
        {
            Name = name;
            grades = new List<double>();
        }

        public override void AddGrade(double grade)
        {
            //Validate grade
            if(grade >= 0 && grade <= 100)
            {
                //Add grade to the list of grades
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }
        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                case 'F':
                    AddGrade(50);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            foreach(var grade in grades)
            {
                result.Add(grade);
            }
            return result;
        }
    }
}