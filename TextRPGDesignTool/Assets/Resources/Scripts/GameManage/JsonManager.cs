using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

[System.Serializable]
public class JItemClassArray
{
    public Item[] jItemClasses;
}

[System.Serializable]
public class JStatClassArray
{
    public Stat[] jStatClasses;
}

public class Info
{

}

public class JsonManager : MonoBehaviour
{
    private static JsonManager instance = null;
    
    public Dictionary<string, Stat> statsDict;
    public Dictionary<string, Item> itemsDict;

    public string statFileName = "StatInfo";
    public string itemFileName = "ItemsInfo";


    public static JsonManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        statsDict = new Dictionary<string, Stat>();
        itemsDict = new Dictionary<string, Item>();

        if (System.IO.File.Exists(statFileName + ".json"))
        {
            Debug.Log("StatJsonFile Exists");

            var jStats = LoadJsonFile<JStatClassArray>(Application.dataPath, itemFileName);

            foreach (var cur in jStats.jStatClasses)
            {
                statsDict[cur.statCode] = cur;
            }
        }
        else
        {
            Debug.Log("statDict Create");

            statsDict["HP"] = new Stat("HP", "체력", "많이 맞을 수 있다");
            statsDict["POWER"] = new Stat("POWER", "공격력", "많이 때릴 수 있다");
        }

        if (System.IO.File.Exists(itemFileName + ".json"))
        {
            Debug.Log("ItemJsonFile Exists");

            var jItems = LoadJsonFile<JItemClassArray>(Application.dataPath, itemFileName);

            foreach (var cur in jItems.jItemClasses)
            {
                itemsDict[cur.itemCode] = cur;
                cur.Print();
            }
        }
    }

    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void JsonDataSave()
    {
        foreach (var cur in itemsDict)
        {
            string jsonData = ObjectToJson(cur.Value);
            CreateJsonFile(Application.dataPath, itemFileName, jsonData);
        }
    }
}
