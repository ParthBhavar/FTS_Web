using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace  FTS.Model.Common
{
    public class Encrypt_Decrypt
    {
        public static string Encrypt(string stringToEncrypt)
        {
            byte[] key = { };
            byte[] iV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            try
            {
                string encryptionKey = "GPHC@2O22#";
                key = System.Text.Encoding.UTF8.GetBytes(Left(encryptionKey, 8));
                DESCryptoServiceProvider encrypt = new DESCryptoServiceProvider();
                byte[] inputByteArrayEncrypt = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encrypt.CreateEncryptor(key, iV), CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArrayEncrypt, 0, inputByteArrayEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                var encryptedId = Convert.ToBase64String(memoryStream.ToArray()).Replace("/", "-").Replace("+", " ");
                return encryptedId;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string Decrypt(string stringToDecrypt)
        {

            byte[] key = { };
            byte[] iV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
            ///// byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            byte[] inputByteArray;
            try
            {
                string decryptionKey = "GPHC@2O22#";
                stringToDecrypt = stringToDecrypt.Replace(" ", "+");
                stringToDecrypt = stringToDecrypt.Replace("-", "/");
                key = System.Text.Encoding.UTF8.GetBytes(Left(decryptionKey, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, iV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }
        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }
    }
}
