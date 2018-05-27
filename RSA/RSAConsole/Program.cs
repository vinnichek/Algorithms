using System;
using RSA;

namespace RSAConsole
{
	public class Program
	{
		public static void Main(string[] args)
		{
			int p = 5, q = 7;
			int n = p * q;
			int fi = (p - 1) * (q - 1);
			int e = Rsa.CalculateE(fi);

			var openKey = Rsa.GenerateOpenKey(n, fi);
			Console.WriteLine(openKey.e + " " + openKey.n);

			int d = Rsa.CalculateD(e, fi);
			var secretKey = Rsa.GenerateSecretKey(n, d);
			Console.WriteLine(secretKey.e + " " + secretKey.n);

			var encodedMessage = Rsa.Encode(5, openKey.e, openKey.n);
			Console.WriteLine(encodedMessage);

			var decodedMessage = Rsa.Decode(encodedMessage, secretKey.e, secretKey.n);
			Console.WriteLine(decodedMessage);

			var secretKey2 = Rsa.GenerateSecretKeyByOpen(openKey);
			Console.WriteLine(secretKey2.e + " " + secretKey2.n);
			Console.ReadKey();
		}
	}
}
