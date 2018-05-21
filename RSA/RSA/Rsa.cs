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

		public static int[] ExtendedEuclideanAlgorithm(int lhs, int rhs)
		{
			int[] result = new int[3];
			if (lhs < rhs)
			{
				int temp = lhs;
				lhs = rhs;
				rhs = temp;
			}
			int r = rhs;
			int q = 0;
			int x0 = 1;
			int y0 = 0;
			int x1 = 0;
			int y1 = 1;
			int x = 0, y = 0;
			while (r > 1)
			{
				r = lhs % rhs;
				q = lhs / rhs;
				x = x0 - q * x1;
				y = y0 - q * y1;
				x0 = x1;
				y0 = y1;
				x1 = x;
				y1 = y;
				lhs = rhs;
				rhs = r;
			}
			result[0] = r;
			result[1] = x;
			result[2] = y;
			return result;
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
			return ExtendedEuclideanAlgorithm(e, fi)[2];
        }

        public static int Encode(int message, int e, int n)
        {
            return ModPow(message, e, n);
        }

        public static int Decode(int message, int d, int n)
        {
            return ModPow(message, d, n);
        }

        public static int ModPow(int num, int degree, int mod)
        {
            return (degree == 0) ? 1 : (((degree & 1) != 0) ? num : 1) *
				ModPow((num * num) % mod, degree / 2, mod) % mod;
        }

		public static int Euler(int num)
		{
			int result = num;
			for (int i = 2; i * i <= num; ++i)
				if (num % i == 0)
				{
					while (num % i == 0)
						num /= i;
					result -= result / i;
				}
			if (num > 1)
				result -= result / num;
			return result;
		}

		public static Key GenerateSecretKeyByOpen(Key openKey)
		{
			Key key;
			key.n = openKey.n;
			key.e = ExtendedEuclideanAlgorithm(openKey.e, Euler(openKey.n))[2];
			return key;
		}
    }
}
