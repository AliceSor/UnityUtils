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
        public T persistentData;
        public bool isFirstApplicationLauch = true;
        public bool enabled = true;

        private string jsonString;

        void OnEnable()
        {
            if (enabled)
            {
                Debug.Log(this.name + " Enabled");

                LoadProgres();
                persistentData = GetData();
                Application.quitting += SaveProgres;
            }
        }

        void OnDestroy()
        {
            if (enabled)
            {
                Debug.Log(this.name + " Destroyed");

                SaveProgres();
            }
        }

        private T GetData()
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
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + filename);
                bf.Serialize(file, jsonString);
                file.Close();
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
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + filename, FileMode.Open);
                    jsonString = (string)bf.Deserialize(file);
                    file.Close();
                    isFirstApplicationLauch = false;
                    Debug.Log("Found progress file");
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
            else
            {
                jsonString = "";
            }
        }// LoadProgres

        public void DeleteProgress()
        {
            if (File.Exists(Application.persistentDataPath + filename))
            {
                try
                {
                    File.Delete(Application.persistentDataPath + filename);
                   // persistentData = new T();
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
        }// DeleteProgress

    }// SaverSO<T> 

}// namespace SavingSystem
