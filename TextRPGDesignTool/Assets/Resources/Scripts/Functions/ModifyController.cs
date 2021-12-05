using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyController : MonoBehaviour
{
    public STATUS status;
    string code;
    SortedList<string, Info> myList;

    public void SetList()
    {
        code = transform.Find("Code").GetComponent<Text>().text;
        myList = JsonManager.Instance.sLists[(int)status];
    }

    public void ModifyName(string name)
    {
        myList[code].name = name;
    }

    public void ModifyExplain(string explain)
    {
        myList[code].explain = explain;

        myList[code].Print();
    }

    public void ModifySetDefault(bool b)
    {
        ((Stat)myList[code]).isDefaultStat = b;

        myList[code].Print();
    }
}
