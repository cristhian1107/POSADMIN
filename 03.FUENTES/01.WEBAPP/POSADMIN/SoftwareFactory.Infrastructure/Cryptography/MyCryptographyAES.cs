using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SoftwareFactory.Infrastructure.Cryptography
{
  public class MyCryptographyAES
  {
    #region [ CONSTANTES ]

    /// <summary>
    /// Main Key for Cryptography
    /// </summary>
    private const string Key = "S0ftw4r3F4ct0ry.Crypt0gr4phy.P3ru.Ar3qu1p4.CJAA.S0ftw4r3F4ct0ry.Crypt0gr4phy.P3ru.Ar3qu1p4.CJAA";

    #endregion

    #region [ METODOS ]

    /// <summary>
    /// Encrypt Data using AES : Advanced Encryption Standard
    /// </summary>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static string EncryptData(string Data)
    {
      RijndaelManaged l_objrij = new RijndaelManaged();
      //set the mode for operation of the algorithm   
      l_objrij.Mode = CipherMode.CBC;
      //set the padding mode used in the algorithm.   
      l_objrij.Padding = PaddingMode.PKCS7;
      //set the size, in bits, for the secret key.   
      l_objrij.KeySize = 0x80;
      //set the block size in bits for the cryptographic operation.    
      l_objrij.BlockSize = 0x80;
      //set the symmetric key that is used for encryption & decryption.    
      byte[] l_passBytes = Encoding.UTF8.GetBytes(Key);
      //set the initialization vector (IV) for the symmetric algorithm    
      byte[] l_EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

      int len = l_passBytes.Length;
      if (len > l_EncryptionkeyBytes.Length)
      { len = l_EncryptionkeyBytes.Length; }

      Array.Copy(l_passBytes, l_EncryptionkeyBytes, len);
      l_objrij.Key = l_EncryptionkeyBytes;
      l_objrij.IV = l_EncryptionkeyBytes;
      //Creates symmetric AES object with the current key and initialization vector IV.    
      ICryptoTransform l_objtransform = l_objrij.CreateEncryptor();
      byte[] l_textDataByte = Encoding.UTF8.GetBytes(Data);
      //Final transform the test string.  
      return Convert.ToBase64String(l_objtransform.TransformFinalBlock(l_textDataByte, 0, l_textDataByte.Length));
    }

    /// <summary>
    /// Decrypt Data using AES : Advanced Encryption Standard
    /// </summary>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static string DecryptData(string Data)
    {
      RijndaelManaged l_objrij = new RijndaelManaged();
      l_objrij.Mode = CipherMode.CBC;
      l_objrij.Padding = PaddingMode.PKCS7;

      l_objrij.KeySize = 0x80;
      l_objrij.BlockSize = 0x80;
      byte[] l_encryptedTextByte = Convert.FromBase64String(Data);
      byte[] l_passBytes = Encoding.UTF8.GetBytes(Key);
      byte[] l_EncryptionkeyBytes = new byte[0x10];
      int len = l_passBytes.Length;
      if (len > l_EncryptionkeyBytes.Length)
      { len = l_EncryptionkeyBytes.Length; }

      Array.Copy(l_passBytes, l_EncryptionkeyBytes, len);
      l_objrij.Key = l_EncryptionkeyBytes;
      l_objrij.IV = l_EncryptionkeyBytes;
      byte[] l_TextByte = l_objrij.CreateDecryptor().TransformFinalBlock(l_encryptedTextByte, 0, l_encryptedTextByte.Length);
      return Encoding.UTF8.GetString(l_TextByte);
    }

    #endregion
  }
}
