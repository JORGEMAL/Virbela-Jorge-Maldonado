using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;


//public class Properties 
//{
//    public float xPos;
//    public float yPos;
//    public float zPos;
//    public string tag;
//}

//[Serializable]
//public class Parameters
//{
//    public List<Properties> propertiesList = new List<Properties>();
//}

//public class DataManager
//{
//    private string saveLocation = Application.persistentDataPath + "/GameData.json";

//    // Save to Json (Serialize)
//    public void SaveData(List<GameObject> objects)
//    {
//        Parameters parameters = new Parameters();
//        Properties properties = new Properties();

//        foreach (GameObject obj in objects)
//        {
//            properties.xPos = obj.transform.position.x;
//            properties.yPos = obj.transform.position.y;
//            properties.zPos = obj.transform.position.z;
//            properties.tag = obj.tag;
//            parameters.propertiesList.Add(properties);
//        }

//        string jsonResult = JsonUtility.ToJson(parameters);
//        File.WriteAllText(saveLocation, jsonResult);
//        //Debug.Log(jsonResult);
//        Debug.Log(saveLocation);
//    }

//    // Load data from Json (Deserialize)
//    public void LoadData()
//    {
//        string gameData = File.ReadAllText(saveLocation);
//        Parameters parameters = new Parameters();

//        parameters = JsonUtility.FromJson<Parameters>(gameData);
//}
