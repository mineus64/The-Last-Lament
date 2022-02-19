// 

using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Crypto
{
  #region Variables

  #endregion
  #region General Methods

  #endregion
  #region Specific Methods
    public static string SHA512Crypt(string pass, string salt) 
    {
      string hash;

      var data = Encoding.UTF8.GetBytes(pass + salt);
      SHA512 shaM = new SHA512Managed();
      byte[] buffer = shaM.ComputeHash(data);

      hash = Encoding.UTF8.GetString(buffer);

      return hash;
    }
  #endregion
}
