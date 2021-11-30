using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public string statCode;
    public string statName;
    public string statExplain;
    public bool isDefaultStat;

    public Stat(bool isSet)
    {
        if (isSet)
        {
            statCode = "";
            statName = "";
            statExplain = "";
            isDefaultStat = true;
        }
    }

    public Stat(string code, string name, string explain, bool isDefault = true)
    {
        statCode = code;
        statName = name;
        statExplain = explain;
        isDefaultStat = isDefault;
    }

    public void Print()
    {
        Debug.Log("statCode = " + statCode);
        Debug.Log("statName = " + statName);
        Debug.Log("statExplain = " + statExplain);
        Debug.Log("isDefaultStat = " + isDefaultStat);
    }
}

public class StatManager : MonoBehaviour
{
    public GameObject statPopup;
    public Stat curStat = new Stat(true);
    private string lastName = "";

    private void CurStatClear()
    {
        curStat = new Stat(true);
    }

    public void SetStatCode(string code)
    {
        curStat.statCode = code;
    }

    public void SetStatName(string name)
    {
        curStat.statName = name;
    }

    public void SetStatExplain(string exp)
    {
        curStat.statExplain = exp;
    }

    public void SetDefaultStat(bool b)
    {
        curStat.isDefaultStat = b;
    }

    public void InsertStat()
    {
        if (curStat.statCode == string.Empty || curStat.statName == string.Empty || curStat.statExplain == string.Empty || JsonManager.Instance.itemsDict.ContainsKey(curStat.statCode))
        {
            Debug.Log("Failed Create, Already Same Stat exists or Code, Name or Explain is empty.");
            return;
        }

        JsonManager.Instance.statsDict[curStat.statCode] = curStat;
        JsonManager.Instance.statsDict[curStat.statCode].Print();

        CurStatClear();
        statPopup.SetActive(false);
    }
}
