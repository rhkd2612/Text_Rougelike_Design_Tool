using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyController : MonoBehaviour
{
    public STATUS status;
    string code;
    SortedList<string,Info> myInfo;

    void Start()
    {
        code = transform.Find("Code").GetComponent<Text>().text;
        myInfo = JsonManager.Instance.sLists[(int)status];
    }

    public void ModifyName(string name)
    {
        myInfo[code].name = name;
    }

    public void ModifyExplain(string explain)
    {
        //myInfo[code].explain = explain;
    }

    public void ModifySetDefault(bool b)
    {
        //((Stat)myInfo[code]).isDefaultStat = b;
    }
}
