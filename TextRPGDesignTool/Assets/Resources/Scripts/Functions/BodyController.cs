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

    int bodyCount;
    int[] bodySize = { 0 };

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

        BodyCreate();
    }


    void BodyCreate()
    {
        for (int i = 0; i < bodyCount; i++)
        {
            GameObject gb = infoes[i];
            STATUS status = (STATUS)i;

            switch (status)
            {
                case STATUS.STAT:
                    foreach (var cur in JsonManager.Instance.statsDict)
                    {
                        StatBodyAdd(gb, cur);
                        bodySize[i]++;
                    }
                    break;
                case STATUS.ITEM:
                    foreach (var cur in JsonManager.Instance.itemsDict)
                    {
                        ItemBodyAdd(gb, cur);
                        bodySize[i]++;
                    }
                    break;
                    /*case STATUS.CHARACTER:
                        foreach (KeyValuePair<string, Char> cur in JsonManager.Instance.statsDict)
                            StatBodyAdd(gb, cur);
                        break;
                    case STATUS.EVENT:
                        foreach (KeyValuePair<string, Event> cur in JsonManager.Instance.statsDict)
                            EventBodyAdd(gb, cur);
                        break;
                    */
            }
        }
    }

    public void UpdateAllBodies()
    {
       
    }

    void StatBodyAdd(GameObject gb, KeyValuePair<string, Stat> cur)
    {
        GameObject newPref = Instantiate(gb, infoPars[(int)curStatus]);
        newPref.transform.localScale = new Vector3(1, 1, 1);
        newPref.transform.Find("Code").GetComponent<Text>().text = cur.Value.statCode;
        newPref.transform.Find("Name").GetComponent<Text>().text = cur.Value.statName;
        newPref.transform.Find("Explain").GetComponent<InputField>().text = cur.Value.statExplain;
        newPref.transform.Find("Toggle").GetComponent<Toggle>().isOn = cur.Value.isDefaultStat;
        newPref.SetActive(true);
    }

    void ItemBodyAdd(GameObject gb, KeyValuePair<string, Item> cur)
    {
        GameObject newPref = Instantiate(gb, infoPars[(int)curStatus]);
        newPref.transform.localScale = new Vector3(1, 1, 1);
        newPref.transform.Find("Code").GetComponent<Text>().text = cur.Value.itemCode;
        newPref.transform.Find("Name").GetComponent<Text>().text = cur.Value.itemName;
        newPref.transform.Find("Explain").GetComponent<InputField>().text = cur.Value.itemExplain;
        newPref.SetActive(true);
    }
    //void CharBodyAdd(GameObject gb, KeyValuePair<string, Char> cur)
    //{
    //    GameObject newPref = Instantiate(gb, infoPars[(int)curStatus]);
    //    newPref.transform.localScale = new Vector3(1, 1, 1);
    //    newPref.transform.Find("Code").GetComponent<Text>().text = cur.Value.statCode;
    //    newPref.transform.Find("Name").GetComponent<Text>().text = cur.Value.statName;
    //    newPref.transform.Find("Explain").GetComponent<InputField>().text = cur.Value.statExplain;
    //    newPref.transform.Find("Toggle").GetComponent<Toggle>().isOn = cur.Value.isDefaultStat;
    //    newPref.SetActive(true);
    //}

    //void EventBodyAdd(GameObject gb, KeyValuePair<string, Event> cur)
    //{
    //}

    public void SetStatus(int i)
    {
        curStatus = (STATUS)i;
    }

    public void ChangeBody()
    {
        for (int i = 0; i < bodies.Length; i++)
            if (i != (int)curStatus)
                bodies[i].SetActive(false);

        bodies[(int)curStatus].SetActive(true);
    }

    public void ShowBodies()
    {
        bodies[(int)curStatus].SetActive(true);
    }

    public void CreateNewObject()
    {
        popups[(int)curStatus].SetActive(true);
    }
}
