using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[CreateAssetMenu(fileName = "PersistentDataHandler", menuName = "Custom/Saving/PersistentDataHandler")]
public class PersistentDataHandler : ScriptableObject
{
    public string filename = "PersistentData";
    public PersistentData persistentData;

    public bool isFirstApplicationLauch = true;

    //In case sript will be enabled when changing scene
    private bool firstLoad = true;

    void OnEnable()
    {
        Debug.Log("PersistentDataHandler Enabled");
        LoadProgres();
        Application.quitting += SaveProgres;
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
