using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

    public class EncryptHelper
    {
        /// <summary>
        /// ���ַ�������MD5��SHA1���ܲ���
        /// </summary>
        /// <param name="cleanString">�����ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string Encrypt(string cleanString, string strPasswordFormat)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName(strPasswordFormat)).ComputeHash(clearBytes);
            return BitConverter.ToString(hashedBytes);
        }



        //Ĭ����Կ����    
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        
        /// <summary>    
        /// DES�����ַ���    
        /// </summary>    
        /// <param name="encryptString">�����ܵ��ַ���</param>    
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>    
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>    
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey,

rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>    
        /// DES�����ַ���    
        /// </summary>    
        /// <param name="decryptString">�����ܵ��ַ���</param>    
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>    
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>    
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey,

rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        //�����ļ�    
        private static void EncryptData(String inName, String outName, byte[] desKey, byte[]

    desIV)
        {
            //Create the file streams to handle the input and output files.    
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.    
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.    
            long rdlen = 0;              //This is the total number of bytes written.    
            long totlen = fin.Length;    //This is the total length of the input file.    
            int len;                     //This is the number of bytes to be written at a time.    

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV),

    CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.    
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        //�����ļ�    
        private static void DecryptData(String inName, String outName, byte[] desKey, byte[]

    desIV)
        {
            //Create the file streams to handle the input and output files.    
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.    
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.    
            long rdlen = 0;              //This is the total number of bytes written.    
            long totlen = fin.Length;    //This is the total length of the input file.    
            int len;                     //This is the number of bytes to be written at a time.    

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV),

    CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.    
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }
    }
