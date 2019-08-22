using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public class RijndaelEncryption
{

    public string Decrypt(byte[] soup, string key)
    {
        try
        {
            byte[] iv = Encoding.ASCII.GetBytes("0123456789012345"); //must be 16 chars
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            // Create a new instance of the Rijndael
            // class.  This generates a new key and initialization 
            // vector (IV).
            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.Key = keyBytes;
                myRijndael.IV = iv;

                // Encrypt the string to an array of bytes.
                //byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);
                //return encrypted;
                // Decrypt the bytes to a string.
                string roundtrip = DecryptStringFromBytes(soup, myRijndael.Key, myRijndael.IV);
                return roundtrip;
                ////Display the original data and the decrypted data.
                //Debug.Log("Original:   {0} " + original);
                //Debug.Log("Round Trip: {0} " + roundtrip);

            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            return null;
        }
    }

    public byte[] Encrypt(string original, string key)//must be 32 chars
    {
        try
        {
            byte[] iv = Encoding.ASCII.GetBytes("0123456789012345"); //must be 16 chars
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            // Create a new instance of the Rijndael
            // class.  This generates a new key and initialization 
            // vector (IV).
            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.Key = keyBytes;
                myRijndael.IV = iv;

                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);
                return encrypted;
                // Decrypt the bytes to a string.
                //string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

                ////Display the original data and the decrypted data.
                //Debug.Log("Original:   {0} " + original);
                //Debug.Log("Round Trip: {0} " + roundtrip);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            return null;
        }
    }

    static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;
        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
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

    static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
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

}
