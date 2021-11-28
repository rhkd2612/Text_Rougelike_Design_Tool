using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

[System.Serializable]
public class JItemClass
{
    public string itemCode; // itemCode
    public string itemName; // itemNameInKorean

    public Dictionary<string, int> statDegree;
    public Dictionary<string, bool> canShowEvents;
    public JItemClass() { }

    public JItemClass(bool isSet)
    {
        itemCode = "4hwnx01jdxj";
        itemName = "의택이의 초록오른손";

        statDegree["hp"] = 3;
        statDegree["power"] = 5;

        canShowEvents["0"] = true;
    }

    public void Print()
    {
        Debug.Log("itemCode = " + itemCode);
        Debug.Log("itemName = " + itemName);

        foreach(var idx in statDegree)
        {
            Debug.Log(string.Format("statDegree[{0}] = {1}", idx.Key, idx.Value));
        }

        foreach (var idx in canShowEvents)
        {
            Debug.Log(string.Format("canShowEvents[{0}] = {1}", idx.Key, idx.Value));
        }
    }
}


[System.Serializable]
public class JItemClassArray
{
    public JItemClass[] jItemClasses;
}

public class JsonManager : MonoBehaviour
{
    public Dictionary<string, JItemClass> itemsJsonDict;
    public string itemFileName = "ItemsInfo";


    void Start()
    {
        if (System.IO.File.Exists(itemFileName + ".json"))
        {
            Debug.Log("ItemJsonFile Exists");

            var jItems = LoadJsonFile<JItemClassArray>(Application.dataPath, itemFileName);

            foreach (var cur in jItems.jItemClasses)
            {
                itemsJsonDict[cur.itemCode] = cur;
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

    public void ItemInsert(JItemClass item)
    {
        itemsJsonDict[item.itemCode] = item;
    }

    public void JsonDataSave()
    {
        foreach (var cur in itemsJsonDict)
        {
            string jsonData = ObjectToJson(cur.Value);
            CreateJsonFile(Application.dataPath, itemFileName, jsonData);
        }
    }
}
