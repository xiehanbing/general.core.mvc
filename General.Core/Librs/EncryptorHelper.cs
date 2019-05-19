﻿using System.Security.Cryptography;
using System.Text;

namespace General.Core.Librs
{
    /// <summary>
    /// 加解密帮助类
    /// </summary>
    public class EncryptorHelper
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            } 
            return sBuilder.ToString();
        }
    }
}