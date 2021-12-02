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

[System.Serializable]
public class Info
{
    public string code;
    public string name;
    public string explain;

    public virtual void Print() { }

    public virtual void SaveInJson() { }
}

[System.Serializable]
public class Stat : Info
{
    public bool isDefaultStat;

    public Stat(bool isSet)
    {
        if (isSet)
        {
            code = "";
            name = "";
            explain = "";
            isDefaultStat = true;
        }
    }

    public Stat(string c, string n, string e, bool isDefault = true)
    {
        code = c;
        name = n;
        explain = e;
        isDefaultStat = isDefault;
    }

    public override void Print()
    {
        Debug.Log("Code = " + code);
        Debug.Log("Name = " + name);
        Debug.Log("Explain = " + explain);
        Debug.Log("isDefaultStat = " + isDefaultStat);
    }
}

[System.Serializable]
public class Item : Info
{
    public SortedList<string, int> statDegree;
    public SortedList<string, bool> canShowEvents;
    public Item()
    {
        code = "";
        name = "";
        explain = "";
        statDegree = new SortedList<string, int>();
        canShowEvents = new SortedList<string, bool>();

        canShowEvents["0"] = true;
    }

    public Item(bool isSet)
    {
        if (isSet)
        {
            code = "";
            name = "";
            explain = "";
            statDegree = new SortedList<string, int>();
            canShowEvents = new SortedList<string, bool>();

            canShowEvents["0"] = true;
        }
    }

    public override void Print()
    {
        Debug.Log("Code = " + code);
        Debug.Log("Name = " + name);
        Debug.Log("Explain = " + explain);

        foreach (var idx in statDegree)
        {
            Debug.Log(string.Format("statDegree[{0}] = {1}", idx.Key, idx.Value));
        }

        foreach (var idx in canShowEvents)
        {
            Debug.Log(string.Format("canShowEvents[{0}] = {1}", idx.Key, idx.Value));
        }
    }
}


public class JsonManager : MonoBehaviour
{
    private static JsonManager instance = null;
    
    public SortedList<string, Info> statsList;
    public SortedList<string, Info> itemsList;

    public List<SortedList<string, Info>> sLists = new List<SortedList<string, Info>>();

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

        statsList = new SortedList<string, Info>();
        itemsList = new SortedList<string, Info>();

        sLists.Add(statsList);
        sLists.Add(itemsList);
        // + char, event

        if (System.IO.File.Exists(statFileName + ".json"))
        {
            Debug.Log("StatJsonFile Exists");

            var jStats = LoadJsonFile<JStatClassArray>(Application.dataPath, itemFileName);

            foreach (var cur in jStats.jStatClasses)
            {
                statsList[cur.code] = cur;
            }
        }
        else
        {
            Debug.Log("statDict Create");

            statsList["HP"] = new Stat("HP", "체력", "많이 맞을 수 있다");
            statsList["POWER"] = new Stat("POWER", "공격력", "많이 때릴 수 있다");
        }

        if (System.IO.File.Exists(itemFileName + ".json"))
        {
            Debug.Log("ItemJsonFile Exists");

            var jItems = LoadJsonFile<JItemClassArray>(Application.dataPath, itemFileName);

            foreach (var cur in jItems.jItemClasses)
            {
                itemsList[cur.code] = cur;
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
        foreach (var cur in itemsList)
        {
            string jsonData = ObjectToJson(cur.Value);
            CreateJsonFile(Application.dataPath, itemFileName, jsonData);
        }
    }
}
