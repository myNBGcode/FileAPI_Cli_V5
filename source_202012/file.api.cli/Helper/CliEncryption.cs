using System;
using System.Security.Cryptography;
using System.Text;

namespace FileapiCli
{
    public static class CliEncryption
    {
        //This security key should be very complex and Random for encrypting the text. This playing vital role in encrypting the text.
        private const string SecurityKey = "_E8GF^9nJfRBb!aT^WaE6UF@^B5b*WaHbB#yZrCW_3mESb6jGxu_X7h?hfdvk5-4mb_ev5!uy?@v&tkAZXDrS$A%huw#kZy=m_L+E?=M@#Qeg&f!hWY=ZvVuHLsUH8#Z%*Enk@F$*aR?*vZj#t2=*3x*kxPpeYwzvFLu^px!X4SQ#EjJ25T-_m@Kpk@xYc=H$UPh@fcqWWBLsL3pbAtk_*m9VZ_T46#E@7?gAVL2xaka+&zz9Z5RHDm5hcgQcg8Q";

        //This method is used to convert the plain text to Encrypted/Un-Readable Text format.
        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            byte[] toEncryptedArray = Encoding.UTF8.GetBytes(PlainText);
            var sha256CryptoService = new SHA256CryptoServiceProvider();
            byte[] securityKeyArray = sha256CryptoService.ComputeHash(Encoding.UTF8.GetBytes(SecurityKey));
            sha256CryptoService.Clear();

            var aesCryptoService = new AesCryptoServiceProvider();
            aesCryptoService.Key = securityKeyArray;
            aesCryptoService.Mode = CipherMode.ECB; // also test the CBC
            aesCryptoService.Padding = PaddingMode.PKCS7;

            var crytpoTransform = aesCryptoService.CreateEncryptor();
            byte[] resultArray = crytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            aesCryptoService.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //This method is used to convert the Encrypted/Un-Readable Text back to readable  format.
        public static string DecryptCipherTextToPlainText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);
            var sha256CryptoService = new SHA256CryptoServiceProvider();
            byte[] securityKeyArray = sha256CryptoService.ComputeHash(Encoding.UTF8.GetBytes(SecurityKey));
            sha256CryptoService.Clear();

            var aesCryptoService = new AesCryptoServiceProvider();
            aesCryptoService.Key = securityKeyArray; 
            aesCryptoService.Mode = CipherMode.ECB;  // also test the CBC
            aesCryptoService.Padding = PaddingMode.PKCS7;

            var crytpoTransform = aesCryptoService.CreateDecryptor();
            byte[] resultArray = crytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            aesCryptoService.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
