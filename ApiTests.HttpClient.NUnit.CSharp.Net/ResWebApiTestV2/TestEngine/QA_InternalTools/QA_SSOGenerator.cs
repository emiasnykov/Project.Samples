using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ResWebApiTest.TestEngine.QA_InternalTools
{
    /// <summary>
    /// SSO encryption/decryption generator class used in Api tests
    /// </summary>
    public class QA_SSOGenerator
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// Encrypts string using AES 
        /// </summary>
        /// <param name="_Input">String to be encrypted</param>
        /// <returns>Encrypted string</returns>
        public static string AESEncrypt(string _Input)
        {
            const string SHARED_SECRET = "*************************"; //20 chars alphanumeric [0-9a-zA-Z]
            const int SALT_SIZE = 128;

            if (string.IsNullOrWhiteSpace(_Input))
                return string.Empty;

            string result = "";

            byte[] dataToEncrypt = Encoding.Unicode.GetBytes(_Input);
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(SHARED_SECRET, SALT_SIZE))
            {
                byte[] salt = pbkdf2.Salt;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = pbkdf2.GetBytes(aes.KeySize / 8);
                    aes.GenerateIV();
                    using (MemoryStream cipherStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(cipherStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                        }

                        byte[] cipher = cipherStream.ToArray();
                        byte[] saltedCipher = new byte[salt.Length + cipher.Length + aes.IV.Length];

                        Array.Copy(salt, 0, saltedCipher, 0, salt.Length);
                        Array.Copy(aes.IV, 0, saltedCipher, salt.Length, aes.IV.Length);
                        Array.Copy(cipher, 0, saltedCipher, salt.Length + aes.IV.Length, cipher.Length);

                        result = HttpServerUtility.UrlTokenEncode(saltedCipher);
                    }

                    aes.Clear();
                }
            }

            return result;
        }

        /// <summary>
        /// Decrypts URL-encoded, AES-encrypted string
        /// </summary>
        /// <param name="_Input">String to be decrypted</param>
        /// <returns>Decrypted string</returns>
        public static string AESDecrypt(string _Input)
        {
            const string SHARED_SECRET = "*************************"; //20 chars alphanumeric [0-9a-zA-Z]
            const int SALT_SIZE = 128;
            const int IV_SIZE = 16;

            string result = "";

            if (string.IsNullOrWhiteSpace(_Input))
                return string.Empty;

            byte[] saltedCipher = HttpServerUtility.UrlTokenDecode(_Input);

            if (saltedCipher.Length < SALT_SIZE + IV_SIZE)
                return string.Empty;

            byte[] salt = new byte[SALT_SIZE];
            byte[] iv = new byte[IV_SIZE];
            byte[] cipher = new byte[saltedCipher.Length - salt.Length - iv.Length];

            Array.Copy(saltedCipher, 0, salt, 0, salt.Length);
            Array.Copy(saltedCipher, salt.Length, iv, 0, iv.Length);
            Array.Copy(saltedCipher, salt.Length + iv.Length, cipher, 0, saltedCipher.Length - salt.Length - iv.Length);

            using (Rfc2898DeriveBytes pbkdf = new Rfc2898DeriveBytes(SHARED_SECRET, salt))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = pbkdf.GetBytes(aes.KeySize / 8);
                    aes.IV = iv;
                    using (MemoryStream cipherStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(cipherStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(cipher, 0, cipher.Length);
                        }

                        result = Encoding.Unicode.GetString(cipherStream.ToArray());
                    }

                    aes.Clear();
                }
            }

            return result;
        }

        #endregion Public methods
    }
}
