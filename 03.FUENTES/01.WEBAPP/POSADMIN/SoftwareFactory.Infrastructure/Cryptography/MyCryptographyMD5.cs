using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SoftwareFactory.Infrastructure.Cryptography
{
  public class MyCryptographyMD5
  {
    #region [ METODOS ]

    /// <summary>
    /// Encrypt Data using MD5 : Convert String a 32 character hexadecimal string.
    /// </summary>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static string EncryptData(string Data)
    {
      // Create a new instance of the MD5CryptoServiceProvider object.
      MD5 l_md5Hasher = MD5.Create();
      // Convert the input string to a byte array and compute the hash.
      byte[] l_data = l_md5Hasher.ComputeHash(Encoding.Default.GetBytes(Data));
      // Create a new Stringbuilder to collect the bytes and create a string.
      StringBuilder l_sBuilder = new StringBuilder();
      // Loop through each byte of the hashed data and format each one as a hexadecimal string.
      for (int i = 0; i < l_data.Length; i++)
      { l_sBuilder.Append(l_data[i].ToString("x2")); }
      // Return the hexadecimal string.
      return l_sBuilder.ToString();
    }

    /// <summary>
    /// Verify a hash against a string. 
    /// </summary>
    /// <param name="Data"></param>
    /// <param name="Hash"></param>
    /// <returns></returns>
    public static bool Verify(string Data, string Hash)
    {
      // Hash the input.
      String l_hashOfInput = EncryptData(Data);
      // Create a StringComparer an compare the hashes.
      StringComparer l_comparer = StringComparer.OrdinalIgnoreCase;
      if (0 == l_comparer.Compare(l_hashOfInput, Hash))
      { return true; }
      else
      { return false; }
    }

    #endregion
  }
}
