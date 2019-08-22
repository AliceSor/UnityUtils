using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

namespace SavingSystem
{
    public class SaverSO<T> : ScriptableObject where T : class
    {
        public string filename = "PersistentData";
        //Can be null
        public T persistentData;
        [HideInInspector]//not sure about it
        public bool isFirstApplicationLauch = true;
        public bool autoLoad = true;
        public bool encrypt = true;

        private string key = "123451234512345123451234512345DF";

        void OnEnable()
        {
            if (autoLoad)
            {
                Debug.Log(this.name + " Enabled");

                LoadProgres();
                Application.quitting += SaveProgres;

                RijndaelEncryption crypto = new RijndaelEncryption();
                byte[] soup = crypto.Encrypt("Some text to encrypt", "123451234512345123451234512345DF");
                string res = crypto.Decrypt(soup, "123451234512345123451234512345DF");
                //FileStream file = File.Create(Application.persistentDataPath + filename);
                //File.WriteAllBytes(file, soup);
                //byte[] soup = File.ReadAllBytes(Application.persistentDataPath + filename);
                Debug.Log(res);
            }
        }

        void OnDestroy()
        {
            if (autoLoad)
            {
                Debug.Log(this.name + " Destroyed");

                SaveProgres();
            }
        }

        private T GetDataFromJson(string jsonString)
        {
            //TODO : try - catch
            T res;
            if (!string.IsNullOrEmpty(jsonString))
                res = JsonUtility.FromJson<T>(jsonString);
            else
                return null;
            return res;
        }

        public void SaveProgres()
        {
            Debug.Log("Saving progress");
            try
            {
                string jsonString = JsonUtility.ToJson(persistentData);
                string path = Application.persistentDataPath + filename;
                if (encrypt)
                {
                    RijndaelEncryption crypto = new RijndaelEncryption();
                    byte[] soup = crypto.Encrypt(jsonString, key);

                    File.WriteAllBytes(path, soup);
                }
                else
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Create(path);

                    bf.Serialize(file, jsonString);
                    file.Close();
                }
                
                Debug.Log("Progress saved");
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }//SaveProgres

        private void LoadProgres()
        {
            if (File.Exists(Application.persistentDataPath + filename))
            {
                try
                {
                    string path = Application.persistentDataPath + filename;
                    string jsonString;

                    if (encrypt)
                    {
                        RijndaelEncryption crypto = new RijndaelEncryption();
                        
                        byte[] soup = File.ReadAllBytes(Application.persistentDataPath + filename);
                        jsonString = crypto.Decrypt(soup, key);
                    }
                    else
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Open(Application.persistentDataPath + filename, FileMode.Open);
                        jsonString = (string)bf.Deserialize(file);
                        file.Close();
                    }
                    
                    isFirstApplicationLauch = false;
                    persistentData = GetDataFromJson(jsonString);
                    Debug.Log("Found progress file");
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
            else
            {
                persistentData = Activator.CreateInstance<T>();
            }
        }// LoadProgres

        public void DeleteProgress()
        {
            if (File.Exists(Application.persistentDataPath + filename))
            {
                try
                {
                    File.Delete(Application.persistentDataPath + filename);
                    persistentData = Activator.CreateInstance<T>();
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
        }// DeleteProgress

    }// SaverSO<T> 

}// namespace SavingSystem
