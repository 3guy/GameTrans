using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ThrFrd.GameTrans.Tool
{
    /// <summary>
    /// AES加密解密
    /// </summary>
    public class AesAlgorithm
    {
        /// <summary>
        /// 获取密钥
        /// </summary>
        private static string Key
        {
            get { return @"dfcRh{+oESB]6,YF}+)O[)O[RH]b9dq7"; }
        }

        /// <summary>
        /// 获取向量
        /// </summary>
        private static string IV
        {
            get { return "K$+~f6%k3.)L0q*-xqz"; }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr">明文字符串</param>
        /// <returns>密文</returns>
        public static string Encrypt(string plainStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = "";
            Rijndael aes = Rijndael.Create();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (
                        CryptoStream cStream =
                            new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch
            {
            }
            aes.Clear();

            return encrypt;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr">明文字符串</param>
        /// <param name="returnNull">加密失败时是否返回 null，false 返回 String.Empty</param>
        /// <returns>密文</returns>
        public static string Encrypt(string plainStr, bool returnNull)
        {
            string encrypt = Encrypt(plainStr);
            return returnNull ? encrypt : encrypt == null ? String.Empty : encrypt;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encryptStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);

            string decrypt = null;
            Rijndael aes = Rijndael.Create();
            try
            {
                byte[] byteArray = Convert.FromBase64String(encryptStr);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (
                        CryptoStream cStream =
                            new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch
            {
            }
            aes.Clear();

            return decrypt;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <param name="returnNull">解密失败时是否返回 null，false 返回 String.Empty</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encryptStr, bool returnNull)
        {
            string decrypt = Decrypt(encryptStr);
            return returnNull ? decrypt : (decrypt == null ? String.Empty : decrypt);
        }
    }
    public enum EncryptMode
    {
        md5, sha1, sha256, sha384, sha512, base64
    }
    public class HashAlgorithm
    {
        public static string Encrypt(string encryptString, EncryptMode encryptMode, Encoding encoding)
        {
            string returnString = "";
            System.Security.Cryptography.HashAlgorithm hash;
            byte[] sString = encoding.GetBytes(encryptString);
            switch (encryptMode.ToString())
            {
                case "md5":
                    hash = new MD5CryptoServiceProvider();
                    byte[] md5data = hash.ComputeHash(sString);
                    hash.Clear();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < md5data.Length; i++)
                    {
                        sb.Append(md5data[i].ToString("x").PadLeft(2, '0'));
                    }
                    returnString = sb.ToString();
                    break;
                case "sha1":
                    hash = new SHA1CryptoServiceProvider();
                    returnString = Convert.ToBase64String(hash.ComputeHash(sString));
                    break;
                case "sha256":
                    hash = new SHA256Managed();
                    returnString = Convert.ToBase64String(hash.ComputeHash(sString));
                    break;
                case "sha384":
                    hash = new SHA384Managed();
                    returnString = Convert.ToBase64String(hash.ComputeHash(sString));
                    break;
                case "sha512":
                    hash = new SHA512Managed();
                    returnString = Convert.ToBase64String(hash.ComputeHash(sString));
                    break;
                default:
                    returnString = String.Empty;
                    break;
            }
            return returnString;
        }
    }
    public sealed class Crc32
    {
        readonly static uint CrcSeed = 0xFFFFFFFF;

        readonly static uint[] CrcTable = new uint[] {
			0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419,
			0x706AF48F, 0xE963A535, 0x9E6495A3, 0x0EDB8832, 0x79DCB8A4,
			0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07,
			0x90BF1D91, 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
			0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7, 0x136C9856,
			0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9,
			0xFA0F3D63, 0x8D080DF5, 0x3B6E20C8, 0x4C69105E, 0xD56041E4,
			0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
			0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940, 0x32D86CE3,
			0x45DF5C75, 0xDCD60DCF, 0xABD13D59, 0x26D930AC, 0x51DE003A,
			0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599,
			0xB8BDA50F, 0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
			0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D, 0x76DC4190,
			0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F,
			0x9FBFE4A5, 0xE8B8D433, 0x7807C9A2, 0x0F00F934, 0x9609A88E,
			0xE10E9818, 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
			0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E, 0x6C0695ED,
			0x1B01A57B, 0x8208F4C1, 0xF50FC457, 0x65B0D9C6, 0x12B7E950,
			0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3,
			0xFBD44C65, 0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
			0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB, 0x4369E96A,
			0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5,
			0xAA0A4C5F, 0xDD0D7CC9, 0x5005713C, 0x270241AA, 0xBE0B1010,
			0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
			0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17,
			0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD, 0xEDB88320, 0x9ABFB3B6,
			0x03B6E20C, 0x74B1D29A, 0xEAD54739, 0x9DD277AF, 0x04DB2615,
			0x73DC1683, 0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
			0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1, 0xF00F9344,
			0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB,
			0x196C3671, 0x6E6B06E7, 0xFED41B76, 0x89D32BE0, 0x10DA7A5A,
			0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
			0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1,
			0xA6BC5767, 0x3FB506DD, 0x48B2364B, 0xD80D2BDA, 0xAF0A1B4C,
			0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF,
			0x4669BE79, 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
			0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F, 0xC5BA3BBE,
			0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31,
			0x2CD99E8B, 0x5BDEAE1D, 0x9B64C2B0, 0xEC63F226, 0x756AA39C,
			0x026D930A, 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
			0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38, 0x92D28E9B,
			0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21, 0x86D3D2D4, 0xF1D4E242,
			0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1,
			0x18B74777, 0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
			0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45, 0xA00AE278,
			0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7,
			0x4969474D, 0x3E6E77DB, 0xAED16A4A, 0xD9D65ADC, 0x40DF0B66,
			0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
			0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605,
			0xCDD70693, 0x54DE5729, 0x23D967BF, 0xB3667A2E, 0xC4614AB8,
			0x5D681B02, 0x2A6F2B94, 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B,
			0x2D02EF8D
		};

        internal static uint ComputeCrc32(uint oldCrc, byte value)
        {
            return (uint)(Crc32.CrcTable[(oldCrc ^ value) & 0xFF] ^ (oldCrc >> 8));
        }

        uint crc;

        public long Value
        {
            get { return (long)crc; }
            set { crc = (uint)value; }
        }
        public void Reset()
        {
            crc = 0;
        }
        public void Update(int value)
        {
            crc ^= CrcSeed;
            crc = CrcTable[(crc ^ value) & 0xFF] ^ (crc >> 8);
            crc ^= CrcSeed;
        }
        public void Update(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            Update(buffer, 0, buffer.Length);
        }
        public void Update(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero");
            }

            if (offset < 0 || offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            crc ^= CrcSeed;

            while (--count >= 0)
            {
                crc = CrcTable[(crc ^ buffer[offset++]) & 0xFF] ^ (crc >> 8);
            }

            crc ^= CrcSeed;
        }
    }
    public class EncodeByBase64
    {
        static char[] _Base64Code = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b',
            'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3',
            '4', '5', '6', '7', '8', '9', '_', '-', '$'
        };
        public static string Encode(string pristine)
        {
            try
            {
               
                byte empty = (byte)0;
                System.Collections.ArrayList byteMessage = new
                  System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes
                  (pristine));
                System.Text.StringBuilder outmessage;
                int messageLen = byteMessage.Count;
                int page = messageLen / 3;
                int use = 0;
                if ((use = messageLen % 3) > 0)
                {
                    for (int i = 0; i < 3 - use; i++)
                        byteMessage.Add(empty);
                    page++;
                }
                outmessage = new System.Text.StringBuilder(page * 4);
                for (int i = 0; i < page; i++)
                {
                    byte[] instr = new byte[3];
                    instr[0] = (byte)byteMessage[i * 3];
                    instr[1] = (byte)byteMessage[i * 3 + 1];
                    instr[2] = (byte)byteMessage[i * 3 + 2];
                    int[] outstr = new int[4];
                    outstr[0] = instr[0] >> 2;
                    outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                    if (!instr[1].Equals(empty))
                        outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                    else
                        outstr[2] = 64;
                    if (!instr[2].Equals(empty))
                        outstr[3] = (instr[2] & 0x3f);
                    else
                        outstr[3] = 64;
                    outmessage.Append(_Base64Code[outstr[0]]);
                    outmessage.Append(_Base64Code[outstr[1]]);
                    outmessage.Append(_Base64Code[outstr[2]]);
                    outmessage.Append(_Base64Code[outstr[3]]);
                }
                return outmessage.ToString();
            }
            catch 
            {
                return String.Empty;
            }
        }

        public static string Decode(string artifactitious)
        {
            try
            {
                string Base64Code = String.Concat(_Base64Code);
                int page = artifactitious.Length / 4;
                System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
                char[] message = artifactitious.ToCharArray();
                for (int i = 0; i < page; i++)
                {
                    byte[] instr = new byte[4];
                    instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                    instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                    instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                    instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                    byte[] outstr = new byte[3];
                    outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                    if (instr[2] != 64)
                    {
                        outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                    }
                    else
                    {
                        outstr[2] = 0;
                    }
                    if (instr[3] != 64)
                    {
                        outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                    }
                    else
                    {
                        outstr[2] = 0;
                    }
                    outMessage.Add(outstr[0]);
                    if (outstr[1] != 0)
                        outMessage.Add(outstr[1]);
                    if (outstr[2] != 0)
                        outMessage.Add(outstr[2]);
                }
                byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
                return System.Text.Encoding.Default.GetString(outbyte);
            }
            catch
            {
                return String.Empty;
            }
        }
    }
    public class Cryptogram
    {
        private static readonly byte[] pKEY = { 218, 239, 227, 22, 31, 53, 120, 224, 223, 223, 171, 210, 140, 158, 47, 86, 122, 39, 238, 95, 47, 138, 44, 155 };
        private static readonly byte[] pIV = { 8, 3, 2, 7, 9, 0, 1, 6 };

        private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

        static Cryptogram()
        {
            //des.Mode= System.Security.Cryptography.CipherMode.ECB ;
            //des.Padding = System.Security.Cryptography.PaddingMode.PKCS7 ;
            des.Mode = System.Security.Cryptography.CipherMode.CBC;
            des.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
        }

        /// <summary>
        /// 对指定字符串进行加密(固定密钥,用于用户密码加密)
        /// </summary>
        /// <param name="toEncryptStr"></param>
        /// <returns></returns>
        public static string CommonEncrypt(string toEncryptStr)
        {
            return CommonEncrypt(toEncryptStr, pKEY);
        }
        public static string CommonEncrypt(string toEncryptStr,string key)
        {
            return CommonEncrypt(toEncryptStr, Encoding.UTF8.GetBytes(key));
        }
        private static string CommonEncrypt(string toEncryptStr,byte[] key)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";
            if (toEncryptStr == "") return "";
            try
            {
                byte[] Encrypted;

                if (Encrypt(key, pIV, ConvertStringToByteArray(toEncryptStr), out Encrypted))
                {
                    return ToBase64String(Encrypted);
                }
                else
                    return "";
            }
            catch (Exception)
            {
                throw new Exception(ErrorNum + "CommonEncrypt（使用固定Key对字符串进行加密）-》" + ErrorDesc);
            }
        }

        public static string CommonDecrypt(string toDecryptStr)
        {
            return CommonDecrypt(toDecryptStr, pKEY);
        }
        public static string CommonDecrypt(string toDecryptStr, string key)
        {
            return CommonDecrypt(toDecryptStr, Encoding.UTF8.GetBytes(key));
        }
        /// <summary>
        /// 对指定字符串进行解密
        /// </summary>
        /// <param name="toDecryptStr"></param>
        /// <returns></returns>
        private static string CommonDecrypt(string toDecryptStr, byte[] key)
        {
            if (toDecryptStr == null || toDecryptStr == "")
            {
                return "";
            }
            try
            {
                byte[] Decrypted;

                if (Decrypt(key, pIV, FromBase64String(toDecryptStr), out Decrypted))
                {
                    return ConvertByteArrayToString(Decrypted);
                }
                else
                    return "";
            }
            catch (Exception e)
            {
                LogHelper.Write(CommonLogger.Application, LogLevel.Error, String.Format("{0},IV:{1},{2}{3}", toDecryptStr, Encoding.UTF8.GetString(key), Environment.NewLine, e.Message));
            }
            return "";
        }


        private static bool Encrypt(byte[] KEY, byte[] IV, byte[] TobeEncrypted, out  byte[] Encrypted)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";
            if (KEY == null || IV == null)
                throw new Exception(ErrorNum + "Encrypt（加密）-》" + ErrorDesc);
            Encrypted = null;
            try
            {
                byte[] tmpiv = { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 8; ii++)
                {
                    tmpiv[ii] = IV[ii];
                }
                byte[] tmpkey = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 24; ii++)
                {
                    tmpkey[ii] = KEY[ii];
                }
                ICryptoTransform tridesencrypt = des.CreateEncryptor(tmpkey, tmpiv);
                Encrypted = tridesencrypt.TransformFinalBlock(TobeEncrypted, 0, TobeEncrypted.Length);
                des.Clear();
                return true;
            }
            catch (Exception)
            {
                throw new Exception(ErrorNum + "GenerateKey（生成key）-》" + ErrorDesc);
            }
        }
        private static bool Decrypt(byte[] KEY, byte[] IV, byte[] TobeDecrypted, out  byte[] Decrypted)
        {
            int ErrorNum = -1;
            string ErrorDesc = "";

            Decrypted = null;
            try
            {
                byte[] tmpiv = { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 8; ii++)
                {
                    tmpiv[ii] = IV[ii];
                }
                byte[] tmpkey = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int ii = 0; ii < 24; ii++)
                {
                    tmpkey[ii] = KEY[ii];
                }
                ICryptoTransform tridesdecrypt = des.CreateDecryptor(tmpkey, tmpiv);
                Decrypted = tridesdecrypt.TransformFinalBlock(TobeDecrypted, 0, TobeDecrypted.Length);
                des.Clear();
            }
            catch (Exception)
            {
                throw new Exception(ErrorNum + "GenerateKey（生成key）-》" + ErrorDesc);
            }
            return true;
        }
        private static string ToBase64String(byte[] buf)
        {
            return System.Convert.ToBase64String(buf);
        }
        private static byte[] FromBase64String(string s)
        {
            return System.Convert.FromBase64String(s);
        }
        private static byte[] ConvertStringToByteArray(String s)
        {
            return System.Text.Encoding.GetEncoding("utf-8").GetBytes(s);//gb2312
        }
        private static string ConvertByteArrayToString(byte[] buf)
        {
            return System.Text.Encoding.GetEncoding("utf-8").GetString(buf);
        }
    }
    public sealed class RSAAlgorithm
    {
        /// <summary>
        /// 创建公钥、私钥
        /// </summary>
        /// <param name="PrivateKeyPath">私钥地址</param>
        /// <param name="PublicKeyPath">公钥地址</param>
        public static void CreateRSAKey(string PrivateKeyPath, string PublicKeyPath)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            SaveKey(PrivateKeyPath, provider.ToXmlString(true));//保存私钥文件
            SaveKey(PublicKeyPath, provider.ToXmlString(false));//保存公钥文件
        }
        private static void SaveKey(string path, string key)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(key);
            sw.Close();
            stream.Close();
        }
        private static string ReadKey(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="p_strKeyPrivate">私钥</param>
        /// <param name="m_strHashbyteSignature">需签名的数据</param>
        /// <returns>签名后的值</returns>
        public string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {
            string keyString = ReadKey(p_strKeyPrivate);
            if (!String.IsNullOrEmpty(keyString))
            {
                RSACryptoServiceProvider oRSA3 = new RSACryptoServiceProvider();
                oRSA3.FromXmlString(keyString);
                byte[] AOutput = oRSA3.SignData(Encoding.UTF8.GetBytes(m_strHashbyteSignature), "MD5");
                return Convert.ToBase64String(AOutput);
            }
            return String.Empty;
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="p_strKeyPublic">公钥</param>
        /// <param name="p_strHashbyteDeformatter">待验证签名文本</param>
        /// <param name="p_strDeformatterData">验证签名字符</param>
        /// <returns>签名是否符合</returns>
        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, 
            string p_strDeformatterData)
        {
            string keyString = ReadKey(p_strKeyPublic);
            if (!String.IsNullOrEmpty(keyString))
            {
                RSACryptoServiceProvider oRSA = new RSACryptoServiceProvider();
                oRSA.FromXmlString(keyString);
                bool bVerify = oRSA.VerifyData(Encoding.UTF8.GetBytes(p_strHashbyteDeformatter), 
                    "MD5", Convert.FromBase64String(p_strDeformatterData));
                return bVerify;
            }
            return false;
        }
    }
}
