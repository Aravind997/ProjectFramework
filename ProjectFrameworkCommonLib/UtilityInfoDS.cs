using System;
using System.Security.Cryptography;
using System.Text;

namespace ProjectFrameworkCommonLib
{
    public class PFCrypt
    {
        private static string _key;

        /// <summary>
        /// Constructor
        /// </summary>
        public PFCrypt()
        {
        }

        /// <summary>
        /// Key for encryption
        /// </summary>
        public static string Key
        {
            set
            {
                _key = value;
            }
        }

        /// <summary>
        /// Encrypt the given string using the default key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt)
        {
            string strTempKey = _key;
            byte[] bytePassHash, byteBuff;

            TripleDESCryptoServiceProvider desCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();

            bytePassHash = hashMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            hashMd5 = null;
            desCrypto.Key = bytePassHash;
            desCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
            return Convert.ToBase64String(desCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));

        }

        /// <summary>
        /// Decrypt the given string using the default key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted)
        {
            string strTempKey = _key;
            byte[] bytePassHash, byteBuff;

            TripleDESCryptoServiceProvider desCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();

            bytePassHash = hashMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            hashMd5 = null;
            desCrypto.Key = bytePassHash;
            desCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = Convert.FromBase64String(strEncrypted);
            string strDecrypted = ASCIIEncoding.ASCII.GetString(desCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            desCrypto = null;

            return strDecrypted;
        }

        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt, string strKey)
        {
            TripleDESCryptoServiceProvider desCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();

            byte[] bytePassHash, byteBuff;
            string strTempKey = strKey;

            bytePassHash = hashMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            hashMd5 = null;
            desCrypto.Key = bytePassHash;
            desCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
            return Convert.ToBase64String(desCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted, string strKey)
        {
            TripleDESCryptoServiceProvider desCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();

            byte[] bytePassHash, byteBuff;
            string strTempKey = strKey;

            bytePassHash = hashMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            hashMd5 = null;
            desCrypto.Key = bytePassHash;
            desCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = Convert.FromBase64String(strEncrypted);
            string strDecrypted = ASCIIEncoding.ASCII.GetString(desCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            desCrypto = null;

            return strDecrypted;
        }
    }
}
