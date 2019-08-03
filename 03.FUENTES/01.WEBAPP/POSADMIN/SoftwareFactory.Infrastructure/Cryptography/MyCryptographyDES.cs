using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SoftwareFactory.Infrastructure.Cryptography
{
  public class MyCryptographyDES
  {
    #region [ CONSTANTES ]

    /// <summary>
    /// Main Key for Cryptography
    /// </summary>
    private const string Key = "S0ftw4r3F4ct0ry.Crypt0gr4phy.P3ru.Ar3qu1p4.CJAA.S0ftw4r3F4ct0ry.Crypt0gr4phy.P3ru.Ar3qu1p4.CJAA";

    #endregion

    #region [ METODOS ]

    /// <summary>
    /// Encrypt Data using DES : Data Encryption Standard
    /// </summary>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static string EncryptData(string Data)
    {
      byte[] l_key = { };
      byte[] l_IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
      byte[] l_inputByteArray;

      try
      {
        l_key = Encoding.UTF8.GetBytes(Key);
        DESCryptoServiceProvider l_ObjDES = new DESCryptoServiceProvider();
        l_inputByteArray = Encoding.UTF8.GetBytes(Data);
        MemoryStream l_Objmst = new MemoryStream();
        CryptoStream l_Objcs = new CryptoStream(l_Objmst, l_ObjDES.CreateEncryptor(l_key, l_IV), CryptoStreamMode.Write);
        l_Objcs.Write(l_inputByteArray, 0, l_inputByteArray.Length);
        l_Objcs.FlushFinalBlock();

        return Convert.ToBase64String(l_Objmst.ToArray());
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// Decrypt Data using DES : Data Encryption Standard
    /// </summary>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static string DecryptData(string Data)
    {
      byte[] l_key = { };
      byte[] l_IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
      byte[] l_inputByteArray = new byte[Data.Length];

      try
      {
        l_key = Encoding.UTF8.GetBytes(Key);
        DESCryptoServiceProvider l_ObjDES = new DESCryptoServiceProvider();
        l_inputByteArray = Convert.FromBase64String(Data);
        MemoryStream l_Objmst = new MemoryStream();
        CryptoStream l_Objcs = new CryptoStream(l_Objmst, l_ObjDES.CreateDecryptor(l_key, l_IV), CryptoStreamMode.Write);
        l_Objcs.Write(l_inputByteArray, 0, l_inputByteArray.Length);
        l_Objcs.FlushFinalBlock();
        Encoding encoding = Encoding.UTF8;

        return encoding.GetString(l_Objmst.ToArray());
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    #endregion
  }
}
