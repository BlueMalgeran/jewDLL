using System;

namespace jewDLL
{
    public class jewTools
    {
        public static bool Contains<T>(T content, T term)
        {
            bool result = content.ToString().Contains(term.ToString());
            return result;
        }

        public static int Digits<T>(T args)
        {
            int result = args.ToString().Length;
            return result;
        }
    }
}
