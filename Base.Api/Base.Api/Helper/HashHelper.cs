using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.WebPages;
using Newtonsoft.Json;

namespace Base.Api.Helper
{
    public static class HashHelper
    {
        public static string Base64Encode(this string originString, string hashWord)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(originString + hashWord);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static int DecodeToWebId(this string companyKeyBase64)
        {
            if (companyKeyBase64.IsEmpty())
            {
                return -1;
            }
            var decode = companyKeyBase64.Base64Decode().Replace("CompanyKey", "");
            int.TryParse(decode, out var result);
            return result;
        }

        public static string EncryptWithPrivateKeyInAes<T>(T model, string privateKey, byte[] iv)
        {
            var objectToString = JsonConvert.SerializeObject(model);
            FormatByteInputToCorrectLengthForAesStandard(privateKey, iv, out var privateKeyIn32Bytes, out var ivIn16Bytes);
            return Convert.ToBase64String(EncryptStringToBytesInAes(objectToString, privateKeyIn32Bytes, ivIn16Bytes));
        }
        
        public static T DecryptWithPrivateKeyInAes<T>(string cipher, string privateKey, byte[] iv)
        {
            var cipherInByte = System.Convert.FromBase64String(cipher);
            FormatByteInputToCorrectLengthForAesStandard(privateKey, iv, out var privateKeyIn32Bytes, out var ivIn16Bytes);
            return JsonConvert.DeserializeObject<T>(
                DecryptStringFromBytesInAes(cipherInByte, privateKeyIn32Bytes, ivIn16Bytes));
        }

        private static byte[] FillByteToTargetLength(byte[] sourceByte, int targetLength)
        {
            var byteWithTargetLength = new byte[targetLength];
            Array.Copy(sourceByte, byteWithTargetLength, targetLength > sourceByte.Length ? sourceByte.Length : targetLength);
            return byteWithTargetLength;
        }

        private static void FormatByteInputToCorrectLengthForAesStandard(string privateKey, byte[] iv, out byte[] privateKeyIn32Bytes,
            out byte[] ivIn16Bytes)
        {
            var privateKeyByte = Encoding.UTF8.GetBytes(privateKey);
            privateKeyIn32Bytes = FillByteToTargetLength(privateKeyByte, 32);
            ivIn16Bytes = FillByteToTargetLength(iv, 16);
        }

        #region AESMethodFromMSDN

        static byte[] EncryptStringToBytesInAes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        static string DecryptStringFromBytesInAes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        #endregion
    }
}
