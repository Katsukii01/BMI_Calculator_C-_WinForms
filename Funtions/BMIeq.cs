using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektKordalski
{
    internal class BMIeq
    {
        public static double CalculateBMI(double height, double weight)
        {
            if (height <= 0 || weight <= 0)
            {
                throw new ArgumentException("Height and weight must be positive values.");
            }

            // obliecznie wskaźnika BMI
            double bmi = weight / Math.Pow(height / 100, 2);

            return bmi;
        }
    }
}
