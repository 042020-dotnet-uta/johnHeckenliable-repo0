using System;

namespace GradeBook
{
    public class Statistics
    {
        int count;
        double sum;
        
        public double High;
        public double Low;

        public double Average
        {
            get
            {
                return sum / count;
            }
        }
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90.0:
                        return 'A';
                    case var d when d >= 80.0:
                        return 'B';
                    case var d when d >= 70.0:
                        return 'C';
                    case var d when d >= 60.0:
                        return 'D';
                    default:
                        return 'F';
                }
            }
        }

        public Statistics()
        {
            High = double.MinValue;
            Low = double.MaxValue;

            count = 0;
            sum = 0.0;
        }

        public void Add(double number)
        {
            sum += number;
            count++;

            High = Math.Max(number, High);
            Low = Math.Min(number, Low);
        }
    }
}