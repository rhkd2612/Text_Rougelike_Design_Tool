using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public GameObject statPopup;
    public Stat curStat = new Stat();

    private void CurStatClear()
    {
        curStat = new Stat();
    }

    public void SetStatCode(string code)
    {
        curStat.code = code;
    }

    public void SetStatName(string name)
    {
        curStat.name = name;
    }

    public void SetStatExplain(string exp)
    {
        curStat.explain = exp;
    }

    public void SetDefaultStat(bool b)
    {
        curStat.isDefaultStat = b;
    }

    public void RemoveStat(Transform tr)
    {
        string text = tr.Find("Code").GetComponent<Text>().text;

        if (!JsonManager.Instance.statsList.ContainsKey(text))
            Debug.Log("There's no key in List");

        JsonManager.Instance.statsList.Remove(text);
        Destroy(tr.gameObject);
    }

    public void InsertStat()
    {
        if (curStat.code == string.Empty || curStat.name == string.Empty || curStat.explain == string.Empty || JsonManager.Instance.itemsList.ContainsKey(curStat.code))
        {
            Debug.Log("Failed Create, Already Same Stat exists or Code, Name or Explain is empty.");
            return;
        }

        JsonManager.Instance.statsList[curStat.code] = curStat;
        JsonManager.Instance.statsList[curStat.code].Print();

        BodyController.Instance.AddBody(curStat,STATUS.STAT, JsonManager.Instance.statsList.IndexOfKey(curStat.code));

        CurStatClear();
        statPopup.SetActive(false);
    }
}
