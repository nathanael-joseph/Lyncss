using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace LyncssConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string phrase = "Hello Lyncss!";
            Console.WriteLine(phrase);
            
            RSA rsa = RSA.Create();
            string signatureString = Services.SignStringDataToString(phrase,rsa.ExportParameters(true));

            Console.WriteLine($"signing [{phrase}] => [{signatureString}] bits");
            RSAParameters publicParams = rsa.ExportParameters(false);

            bool isValidSignature = Services.ValidateSignature(phrase, 
                                                               signatureString, 
                                                               Convert.ToBase64String(publicParams.Modulus), 
                                                               Convert.ToBase64String(publicParams.Exponent));

            Console.WriteLine($"validating the signature => {isValidSignature}");

            rsa.Dispose();
            
        }
    }
}
