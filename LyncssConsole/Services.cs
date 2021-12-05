/*
    Provides definitions and static methods used in the blockchain.
*/

using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LyncssConsole
{
    public static class Services
    {
        public const int RSA_KEY_SIZE_BITS = 2048;
        public const string LYNCSS_BLOCK_HASH_REGEX_STR = "^0713[0-9a-fA-F]{60}";
        public static Regex BlockHashRegex = new Regex(LYNCSS_BLOCK_HASH_REGEX_STR);
        public static byte[] GetSha256Hash(string s) 
        {
            byte[] hashBytes;
            if(String.IsNullOrEmpty(s)) 
            {
                return new byte[32]; // => 000....0
            }

            using(SHA256 sha256 = SHA256.Create()) 
            {
                byte[] bfr = Encoding.UTF8.GetBytes(s);
                hashBytes = sha256.ComputeHash(bfr);
            }

            return hashBytes;
        }
        public static string GetSha256HashHexString(string s) 
        {
            return Convert.ToHexString(GetSha256Hash(s));
        }

        public static byte[] SignStringDataToBytes(string s, RSAParameters parameters) // to sign the hash of a transaction or block body
        {
            byte[] signatureBytes;
            
            if(String.IsNullOrEmpty(s)) 
            {
                return new byte[256]; // => 000...0
            }

            using(RSA rsa = RSA.Create(parameters)) 
            {
               signatureBytes = rsa.SignData(Encoding.UTF8.GetBytes(s), 
                                             HashAlgorithmName.SHA256, 
                                             RSASignaturePadding.Pkcs1);
            }

            return signatureBytes;
        }

        public static string SignStringDataToString(string s, RSAParameters parameters)
        {
            return Convert.ToBase64String(SignStringDataToBytes(s, parameters));
        }

        public static bool ValidateSignature(string content, string signature, string modulus, string exponent)
        {
            bool isValidSignature = false;
            byte[] data = Encoding.UTF8.GetBytes(content);
            byte[] signatureBytes = Convert.FromBase64String(signature);

            RSAParameters parameters = new RSAParameters(); 
            parameters.Modulus = Convert.FromBase64String(modulus);
            parameters.Exponent = Convert.FromBase64String(exponent);         

            using(RSA rsa = RSA.Create(parameters))
            {
               isValidSignature = rsa.VerifyData(data, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }

            return isValidSignature;
        }

    }
}
