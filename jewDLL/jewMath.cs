using System;

namespace jewDLL
{
    public class jewMath
    {
        public static double Connection(double num1, double num2)
        {
            double result = num1 + num2;
            return result;
        }

        public static double Subtraction(double num1, double num2)
        {
            double result = num1 - num2;
            return result;
        }

        public static double Multiply(double num1, double num2)
        {
            double result = num1 * num2;
            return result;
        }

        public static double Distinction(double num1, double num2)
        {
            double result = num1 / num2;
            return result;
        }

        public static double Modulo(double num1, double num2)
        {
            double result = num1 % num2;
            return result;
        }

        public static double Power(double num1, double num2)
        {
            double result = Math.Pow(num1, num2);
            return result;
        }
    }
}
