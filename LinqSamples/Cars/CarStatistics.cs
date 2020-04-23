using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class CarStatistics
    {
        private int _totalCombined = 0;
        private int _carCount = 0;

        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }
        public int Max { get; set; }
        public int Min { get; set; }
        public double Avg { get; set; }

        public CarStatistics Accumulate(Car car)
        {
            _carCount++;
            _totalCombined += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Avg = _totalCombined / _carCount;
            return this;
        }
    }
}
