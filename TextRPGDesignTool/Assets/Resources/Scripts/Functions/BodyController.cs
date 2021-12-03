using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum STATUS
{
    STAT,
    ITEM,
    CHARACTER,
    EVENT,
}

public class BodyController : MonoBehaviour
{
    public STATUS curStatus = STATUS.STAT;

    public GameObject[] popups;
    public GameObject[] bodies;

    public GameObject[] infoes;
    public Transform[] infoPars;

    public Transform itemMainInfo;

    int bodyCount;

    private static BodyController instance = null;
    public static BodyController Instance
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
    }

    private void Start()
    {
        bodyCount = bodies.Length;

        ChangeBody();
        CreateBody();
    }

    void CreateBody()
    {
        for (int i = 0; i < bodyCount; i++)
        {
            if (i >= JsonManager.Instance.sLists.Count)
                break;

            var sl = JsonManager.Instance.sLists[i];
            int sCount = 0;

            Debug.Log(sl.Count);

            foreach (var cur in sl)
                AddBody(cur.Value, (STATUS)i, sCount++);
        }
    }

    public void AddBody(Info cur, STATUS status, int nth)
    {
        GameObject newPref = Instantiate(infoes[(int)status], infoPars[(int)status]);
        newPref.transform.localScale = new Vector3(1, 1, 1);
        newPref.name = infoes[(int)status].name + nth.ToString();
        newPref.transform.SetSiblingIndex(nth);
        newPref.transform.Find("Code").GetComponent<Text>().text = cur.code;
        newPref.transform.Find("Name").GetComponent<Text>().text = cur.name;
        newPref.transform.Find("Explain").GetComponent<InputField>().text = cur.explain;
        newPref.SetActive(true);

        switch (status)
        {
            case STATUS.STAT:
                newPref.transform.Find("Toggle").GetComponent<Toggle>().isOn = ((Stat)cur).isDefaultStat;
                break;
            case STATUS.ITEM:
                break;
            case STATUS.CHARACTER:
                break;
            case STATUS.EVENT:
                break;
        }
    }

    public void SetStatus(int i)
    {
        curStatus = (STATUS)i;
        ChangeBody();
    }

    public void ChangeBody()
    {
        for (int i = 0; i < bodies.Length; i++)
            if (i != (int)curStatus)
                bodies[i].SetActive(false);

        bodies[(int)curStatus].SetActive(true);
    }

    public void ShowPopups()
    {
        popups[(int)curStatus].SetActive(true);
    }

    public void ShowItemInfo(Transform tr)
    {
        Item curItem = (Item)JsonManager.Instance.itemsList[tr.Find("Code").GetComponent<Text>().text];

        curItem.Print();

        itemMainInfo.Find("InputFields").Find("code").GetComponent<InputField>().text = curItem.code;
        itemMainInfo.Find("InputFields").Find("name").GetComponent<InputField>().text = curItem.name;
        itemMainInfo.Find("InputFields").Find("ingame_explain").GetComponent<InputField>().text = curItem.explain;

        if (curItem.statDegree.Count > 0)
        {
            foreach (var c in curItem.statDegree)
                itemMainInfo.Find("Stat").GetComponent<DropdownController>().InsertNewDropdown(c.Key, c.Value);
        }
    }

    public void ShutIteminfo(Transform stats)
    {
        var now = stats.GetChild(0);

        while (stats.childCount > 0 && now.name != "NewDropdown")
        {
            Destroy(now);

            if (stats.childCount == 0)
                break;

            now = stats.GetChild(0);
        }
    }
}
