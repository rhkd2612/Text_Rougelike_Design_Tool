using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

[System.Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;

    SortedList<TKey, TValue> target;
    public SortedList<TKey, TValue> ToSortedList() { return target; }

    public Serialization(SortedList<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new SortedList<TKey, TValue>(count);

        for (var i = 0; i < count; i++)
            target[keys[i]] = values[i];
    }
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

    public Stat()
    {
        code = "";
        name = "";
        explain = "";
        isDefaultStat = true;
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

    public Item(string c, string n, string e, SortedList<string, int> sD, SortedList<string, bool> cO)
    {
        code = c;
        name = n;
        explain = e;
        statDegree = sD;
        canShowEvents = cO;

        if(sD == null)
            statDegree = new SortedList<string, int>();
        else
            statDegree = sD;

        if (cO == null)
            canShowEvents = new SortedList<string, bool>();
        else
            canShowEvents = cO;
    }

    public override void Print()
    {
        Debug.Log("Code = " + code);
        Debug.Log("Name = " + name);
        Debug.Log("Explain = " + explain);

        if (statDegree.Count > 0)
            foreach (var idx in statDegree)
            {
                Debug.Log(string.Format("statDegree[{0}] = {1}", idx.Key, idx.Value));
            }

        if (canShowEvents.Count > 0)
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

        if (System.IO.File.Exists(string.Format("{0}/{1}.json",Application.dataPath, statFileName)))
        {
            Debug.Log("StatJsonFile Exists");

            //var jStats = LoadJsonFile<JStatClassArray>(Application.dataPath, itemFileName);

            //foreach (var cur in jStats.jStatClasses)
            //{
            //    statsList[cur.code] = cur;
            //}
        }
        else
        {
            Debug.Log("StatJsonFile Not Exist");

            statsList["HP"] = new Stat("HP", "체력", "많이 맞을 수 있다", false);
            statsList["POWER"] = new Stat("POWER", "공격력", "많이 때릴 수 있다", false);

            Debug.Log("sssss");
            sLists[0]["HP"].Print();
        }

        if (System.IO.File.Exists(string.Format("{0}/{1}.json", Application.dataPath, itemFileName)))
        {
            Debug.Log("ItemJsonFile Exists");

            //var jItems = LoadJsonFile<JItemClassArray>(Application.dataPath, itemFileName);

            //foreach (var cur in jItems.jItemClasses)
            //{
            //    itemsList[cur.code] = cur;
            //    cur.Print();
            //}

            // SortedList<string, int> temp = JsonUtility.FromJson<Serialization<string, int>>(JsonUtility.ToJson(new Serialization<string, int>(iCur.statDegree))).ToSortedList();

        }
        else
        {
            Debug.Log("ItemJsonFile Not Exist");

            itemsList["scv"] = new Item("scv", "1", "1", null, null);
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
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Append);
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
        DeleteAllFiles();

        foreach (var cur in itemsList)
        {
            Item iCur = (Item)cur.Value;

            iCur.Print();

            string jsonData = ObjectToJson(iCur);

            //jsonData = modifyJsonString(jsonData);
            //jsonData += JsonUtility.ToJson(new Serialization<string, int>(iCur.statDegree));
            //jsonData = modifyJsonString(jsonData);
            //jsonData += JsonUtility.ToJson(new Serialization<string, bool>(iCur.canShowEvents));

            CreateJsonFile(Application.dataPath, itemFileName, jsonData);
        }
    }

    string modifyJsonString(string text)
    {
        return text.Substring(0, text.Length - 1) + ',';
    }

    void DeleteAllFiles()
    {
        if(File.Exists(string.Format("{0}/{1}.json", Application.dataPath, statFileName)))
            File.Delete(string.Format("{0}/{1}.json", Application.dataPath, statFileName));

        if (File.Exists(string.Format("{0}/{1}.json", Application.dataPath, itemFileName)))
            File.Delete(string.Format("{0}/{1}.json", Application.dataPath, itemFileName));
    }
}
