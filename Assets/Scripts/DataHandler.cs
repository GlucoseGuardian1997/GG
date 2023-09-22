using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public SquareDataSet SquareDataSet;
    public string RawJson;
    public string LoadFileName;

    [ContextMenu("Reset")]
    public void Reset()
    {
        LoadFileName = "";
        RawJson = "";
        SquareDataSet = null;
    }


    [ContextMenu("SaveFile")]
    public void SaveFile()
    {
        string destination = $"{Application.persistentDataPath}/{SquareDataSet.SaveName}.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        RawJson = JsonUtility.ToJson(SquareDataSet);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, RawJson);
        file.Close();
        Debug.Log($"File saved at {Application.persistentDataPath}");

        LoadFileName = SquareDataSet.SaveName;
        LoadFile();

    }


    [ContextMenu("LoadFile")]
    public void LoadFile()
    {
        string destination = $"{Application.persistentDataPath}/{LoadFileName}.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        string data = (string)bf.Deserialize(file);
        SquareDataSet = JsonUtility.FromJson<SquareDataSet>(data);
        file.Close();

    }

}


[Serializable]
public class SquareDataSet
{
    public string SaveName;
    public int BaseGirdSizeX ;
    public int BaseGirdSizeY ;
    public List<SquareData> SquareData;
}


[Serializable]
public class SquareData
{ 

    public int PosX;
    public int PosY;

    public float rotZ;

    public float scaleX;
    public float scaleY;

    public string color = "FF0000";
    public string name;

}