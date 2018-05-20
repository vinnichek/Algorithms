using System;
using RSA;

namespace RSAConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int p = 3, q = 7;
            int n = p * q;
            int fi = (p - 1) * (q - 1);
            int e = Rsa.CalculateE(fi);
            //int d = Rsa.CalculateD(p, q);
            int d = 17;
            var openKey = Rsa.GenerateOpenKey(n, fi);
            var secretKey = Rsa.GenerateSecretKey(n, d);

            var encodedMessage = Rsa.Encode(19, openKey.e, openKey.n);
            Console.WriteLine(encodedMessage);

            var decodedMessage = Rsa.Decode(encodedMessage, secretKey.e, secretKey.n);
            Console.WriteLine(decodedMessage);
        }
    }
}