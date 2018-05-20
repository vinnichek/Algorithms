using System;

namespace RSA
{
    public class Rsa
    {
        public struct Key
        {
            public int e;
            public int n;
        }

        public static int ExtendedEuclid(int a, int b, out int x, out int y)
        {
            if (b < a)
            {
                var item = a;
                a = b;
                b = item;
            }

            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            int gcd = ExtendedEuclid(b % a, a, out int newX, out int newY);
            x = newY - b / a * newX;
            y = newX;
            return gcd;
        }

        public static Key GenerateOpenKey(int n, int fi)
        {
            Key key;
            key.n = n;
            key.e = CalculateE(fi);
            return key;
        }

        public static Key GenerateSecretKey(int n, int d)
        {
            Key key;
            key.n = n;
            key.e = d;
            return key;
        }

        public static int CalculateE(int fi)
        {
            int max = fi - 1;
            int e = 1;
            for (int i = 1; i < max; i++)
            {
                if (IsPrime(i) && IsCoprime(i, fi))
                    return e = i;
            }
            return e;
        }

        public static bool IsPrime(int item)
        {
            if (item < 2)
                return false;

            for (int i = 2; i < item; i++)
            {
                if (item % i == 0)
                    return false;
            }
            return true;
        }

        public static bool IsCoprime(int lhs, int rhs)
        {
            int item;
            while (rhs != 0)
            {
                item = lhs % rhs;
                lhs = rhs;
                rhs = item;
            }

            if (Math.Abs(lhs) == 1)
            {
                return true;
            }
            return false;
        }

        public static int CalculateD(int fi, int e)
        {
            int d = 0;
            for (int i = 0; ; i++)
            {
                if ((e * i + 1) % fi == 0)
                {
                    d = (e * i + 1) / fi;
                    break;
                }
            }
            return d;
        }

        public static int Encode(int message, int e, int n)
        {
            return modPow(message, e, n);
        }

        public static int Decode(int message, int d, int n)
        {
            return modPow(message, d, n);
        }

        public static int modPow(int num, int degree, int mod)
        {
            return (degree == 0) ? 1 : (((degree & 1) != 0) ? num : 1) * 
                modPow((num * num) % mod, degree / 2, mod) % mod;
        }
    }
}