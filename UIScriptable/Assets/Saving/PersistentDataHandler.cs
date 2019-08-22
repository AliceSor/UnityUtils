﻿using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SavingSystem
{
    public class Test
    {
        public Vector3 v3;
        private string priv = "private";
        public string pub;
        public string str;
        protected string prot = "protecred";
        public List<int> lst;
        public PersistentData OGGG;
        public bool enabled = true;
    }


    [CreateAssetMenu(fileName = "PersistentDataHandler", menuName = "Custom/Saving/PersistentDataHandler")]
    public class PersistentDataHandler : ScriptableObject
    {
        public string filename = "PersistentData";
        public PersistentData persistentData;

        public bool isFirstApplicationLauch = true;

        //In case sript will be enabled when changing scene
        private bool firstLoad = true;
        public bool enabled = false;

        void OnEnable()
        {
            if (enabled)
            {
                LoadProgres();
                Application.quitting += SaveProgres;
            }
        }

        public T GetData<T>()
        {
            var t = new Test();
            List<int> l = new List<int>() { 1, 2, 3 };
            t.lst = l;
            t.OGGG = new PersistentData();

            // var j = JsonConvert.SerializeObject(t, Formatting.Indented);
            var j1 = JsonUtility.ToJson(t);

            T res = JsonUtility.FromJson<T>(j1);
           // T res = JsonConvert.DeserializeObject<T>(j.ToString());
            return res;
        }

        void OnDestroy()
        {
            Debug.Log("PersistentDataHandler Destroyed");

            SaveProgres();
        }

        public void SaveProgres()
        {
            Debug.Log("Saving progress");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + filename);
                bf.Serialize(file, persistentData);
                file.Close();
                Debug.Log("Progress saved");
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }

        public void LoadProgres()
        {
            if (File.Exists(Application.persistentDataPath + filename))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + filename, FileMode.Open);
                    persistentData = (PersistentData)bf.Deserialize(file);
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
                persistentData = new PersistentData();
            }
        }

        public void DeleteProgress()
        {
            if (File.Exists(Application.persistentDataPath + filename))
            {
                try
                {
                    File.Delete(Application.persistentDataPath + filename);
                    persistentData = new PersistentData();
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
        }

    }//PersistensDataHandler
}