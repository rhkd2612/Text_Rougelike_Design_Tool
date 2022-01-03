using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

[System.Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
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

    public virtual void Serialize() { }

    public virtual void DeSerialize() { }
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

    public override void Serialize()
    {

    }
}

[System.Serializable]
public class Item : Info
{
    public bool isConsume = false;
    public bool isShownAllScenes = false;
    public SortedList<string, int> statDegree;
    public SortedList<string, bool> showEvents;

    [SerializeField]
    public List<string> statDegreeKeys;
    [SerializeField]
    public List<int> statDegreeValues;
    [SerializeField]
    public List<string> showEventsKeys;
    [SerializeField]
    public List<bool> showEventsValues;

    public Item()
    {
        code = "";
        name = "";
        explain = "";
        statDegree = new SortedList<string, int>();
        showEvents = new SortedList<string, bool>();

        statDegreeKeys = new List<string>();
        statDegreeValues = new List<int>();

        showEventsKeys = new List<string>();
        showEventsValues = new List<bool>();
    }

    public Item(string c, string n, string e, SortedList<string, int> sD, SortedList<string, bool> cO, bool isC, bool isS)
    {
        isConsume = isC;
        isShownAllScenes = isS;
        code = c;
        name = n;
        explain = e;
        statDegree = sD;
        showEvents = cO;

        if (sD == null)
            statDegree = new SortedList<string, int>();
        else
            statDegree = sD;

        if (cO == null)
            showEvents = new SortedList<string, bool>();
        else
            showEvents = cO;

        statDegreeKeys = new List<string>();
        statDegreeValues = new List<int>();

        showEventsKeys = new List<string>();
        showEventsValues = new List<bool>();
    }

    public override void Print()
    {
        Debug.Log("Code = " + code);
        Debug.Log("Name = " + name);
        Debug.Log("Explain = " + explain);
        Debug.Log("isConsume = " + isConsume);
        Debug.Log("isShownAllScenes" + isShownAllScenes);

        if (statDegree.Count > 0)
            foreach (var idx in statDegree)
            {
                Debug.Log(string.Format("statDegree[{0}] = {1}", idx.Key, idx.Value));
            }

        if (showEvents.Count > 0)
            foreach (var idx in showEvents)
            {
                Debug.Log(string.Format("showEvents[{0}] = {1}", idx.Key, idx.Value));
            }
    }

    public override void Serialize()
    {
        statDegreeKeys = new List<string>();
        statDegreeValues = new List<int>();

        showEventsKeys = new List<string>();
        showEventsValues = new List<bool>();

        foreach (var c in statDegree)
        {
            statDegreeKeys.Add(c.Key);
            statDegreeValues.Add(c.Value);
        }

        foreach (var c in showEvents)
        {
            showEventsKeys.Add(c.Key);
            showEventsValues.Add(c.Value);
        }
    }

    public override void DeSerialize()
    {
        for (int i = 0; i < statDegreeKeys.Count; i++)
            statDegree[statDegreeKeys[i]] = statDegreeValues[i];

        for (int i = 0; i < showEventsKeys.Count; i++)
            showEvents[showEventsKeys[i]] = showEventsValues[i];
    }
}

[System.Serializable]
public class Character : Info
{
    public SortedList<string, int> statDegree;
    public SortedList<string, bool> showEvents;

    [SerializeField]
    public List<string> statDegreeKeys;
    [SerializeField]
    public List<int> statDegreeValues;
    [SerializeField]
    public List<string> showEventsKeys;
    [SerializeField]
    public List<bool> showEventsValues;

    public Character()
    {
        code = "";
        name = "";
        explain = "";
        statDegree = new SortedList<string, int>();
        showEvents = new SortedList<string, bool>();

        showEvents["0"] = true;

        statDegreeKeys = new List<string>();
        statDegreeValues = new List<int>();

        showEventsKeys = new List<string>();
        showEventsValues = new List<bool>();
    }

    public Character(string c, string n, string e, SortedList<string, int> sD, SortedList<string, bool> cO)
    {
        code = c;
        name = n;
        explain = e;
        statDegree = sD;
        showEvents = cO;

        if (sD == null)
            statDegree = new SortedList<string, int>();
        else
            statDegree = sD;

        if (cO == null)
            showEvents = new SortedList<string, bool>();
        else
            showEvents = cO;

        statDegreeKeys = new List<string>();
        statDegreeValues = new List<int>();

        showEventsKeys = new List<string>();
        showEventsValues = new List<bool>();
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

        if (showEvents.Count > 0)
            foreach (var idx in showEvents)
            {
                Debug.Log(string.Format("showEvents[{0}] = {1}", idx.Key, idx.Value));
            }
    }

    public override void Serialize()
    {
        statDegreeKeys = new List<string>();
        statDegreeValues = new List<int>();

        showEventsKeys = new List<string>();
        showEventsValues = new List<bool>();

        foreach (var c in statDegree)
        {
            statDegreeKeys.Add(c.Key);
            statDegreeValues.Add(c.Value);
        }

        foreach (var c in showEvents)
        {
            showEventsKeys.Add(c.Key);
            showEventsValues.Add(c.Value);
        }
    }

    public override void DeSerialize()
    {
        for (int i = 0; i < statDegreeKeys.Count; i++)
            statDegree[statDegreeKeys[i]] = statDegreeValues[i];

        for (int i = 0; i < showEventsKeys.Count; i++)
            showEvents[showEventsKeys[i]] = showEventsValues[i];
    }
}

public class JsonManager : MonoBehaviour
{
    private static JsonManager instance = null;

    public SortedList<string, Info> statsList;
    public SortedList<string, Info> itemsList;
    public SortedList<string, Info> charsList;

    List<Stat> jStatList = new List<Stat>();
    List<Item> jItemList = new List<Item>();
    List<Character> jCharList = new List<Character>();

    public List<SortedList<string, Info>> sLists = new List<SortedList<string, Info>>();

    public string statFileName = "StatInfo";
    public string itemFileName = "ItemsInfo";
    public string charFileName = "CharsInfo";


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
        charsList = new SortedList<string, Info>();

        sLists.Add(statsList);
        sLists.Add(itemsList);
        sLists.Add(charsList);

        if (System.IO.File.Exists(string.Format("{0}/{1}.json", Application.dataPath, statFileName)))
        {
            Debug.Log("StatJsonFile Exists");

            var str = LoadJsonFileWithString(Application.dataPath, statFileName);
            jStatList = JsonUtility.FromJson<Serialization<Stat>>(str).ToList();

            foreach (var stat in jStatList)
            {
                stat.DeSerialize();
                statsList.Add(stat.code, stat);
            }
        }
        else
        {
            Debug.Log("StatJsonFile Not Exist");

            statsList["HP"] = new Stat("HP", "체력", "많이 맞을 수 있다", false);
            statsList["POWER"] = new Stat("POWER", "공격력", "많이 때릴 수 있다", false);
        }

        if (System.IO.File.Exists(string.Format("{0}/{1}.json", Application.dataPath, itemFileName)))
        {
            Debug.Log("ItemJsonFile Exists");

            var str = LoadJsonFileWithString(Application.dataPath, itemFileName);
            jItemList = JsonUtility.FromJson<Serialization<Item>>(str).ToList();

            foreach (var item in jItemList)
            {
                item.DeSerialize();
                itemsList.Add(item.code, item);
            }
        }
        else
        {
            Debug.Log("ItemJsonFile Not Exist");

            itemsList["temp1"] = new Item("temp1", "임시 아이템이다.", "아이템 그렇게 만드는 거 아닌데", null, null,false,false);
        }

        if (System.IO.File.Exists(string.Format("{0}/{1}.json", Application.dataPath, charFileName)))
        {
            Debug.Log("CharJsonFile Exists");

            var str = LoadJsonFileWithString(Application.dataPath, charFileName);
            jCharList = JsonUtility.FromJson<Serialization<Character>>(str).ToList();

            foreach (var character in jCharList)
            {
                character.DeSerialize();
                charsList.Add(character.code, character);
            }
        }
        else
        {
            Debug.Log("CharJsonFile Not Exist");

            charsList["백수"] = new Character("백수", "임시 직업이다.", "직업 그렇게 만드는 거 아닌데", null, null);
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

    string LoadJsonFileWithString(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return jsonData;
    }

    public void JsonDataSave()
    {
        DeleteAllFiles();
        string jsonData = string.Empty;

        #region STAT_SAVE
        foreach (var cur in statsList)
        {
            Stat iCur = (Stat)cur.Value;
            iCur.Serialize();

            iCur.Print();

            jStatList.Add(iCur);
        }

        jsonData = ObjectToJson(new Serialization<Stat>(jStatList));
        CreateJsonFile(Application.dataPath, statFileName, jsonData);
        #endregion

        #region ITEM_SAVE
        foreach (var cur in itemsList)
        {
            Item iCur = (Item)cur.Value;
            iCur.Serialize();

            jItemList.Add(iCur);
        }

        jsonData = ObjectToJson(new Serialization<Item>(jItemList));
        CreateJsonFile(Application.dataPath, itemFileName, jsonData);
        #endregion

        #region CHARACTER_SAVE
        foreach (var cur in charsList)
        {
            Character iCur = (Character)cur.Value;
            iCur.Serialize();

            jCharList.Add(iCur);
        }

        jsonData = ObjectToJson(new Serialization<Character>(jCharList));
        CreateJsonFile(Application.dataPath, charFileName, jsonData);
        #endregion
    }

    void DeleteAllFiles()
    {
        jStatList = new List<Stat>();
        jItemList = new List<Item>();
        jCharList = new List<Character>();

        if (File.Exists(string.Format("{0}/{1}.json", Application.dataPath, statFileName)))
            File.Delete(string.Format("{0}/{1}.json", Application.dataPath, statFileName));

        if (File.Exists(string.Format("{0}/{1}.json", Application.dataPath, itemFileName)))
            File.Delete(string.Format("{0}/{1}.json", Application.dataPath, itemFileName));

        if (File.Exists(string.Format("{0}/{1}.json", Application.dataPath, charFileName)))
            File.Delete(string.Format("{0}/{1}.json", Application.dataPath, charFileName));
    }
}
