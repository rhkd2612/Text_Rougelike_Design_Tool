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

            foreach(var cur in sl)
            {
                AddBody(cur.Value, (STATUS)i, sCount++);
                bodySize[i]++;
            }
        }
    }

    public void AddBody(Info cur, STATUS status, int nth)
    {
        GameObject newPref = Instantiate(infoes[(int)status], infoPars[(int)curStatus]);
        newPref.transform.localScale = new Vector3(1, 1, 1);
        newPref.name = infoes[(int)status].name + nth.ToString();
        newPref.transform.SetSiblingIndex(nth);
        newPref.transform.Find("Code").GetComponent<Text>().text = cur.code;
        newPref.transform.Find("Name").GetComponent<Text>().text = cur.name;
        newPref.transform.Find("Explain").GetComponent<InputField>().text = cur.explain;
        newPref.SetActive(true);

        switch(status)
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

    public void ShowBodies()
    {
        bodies[(int)curStatus].SetActive(true);
    }

    public void ShowPopups()
    {
        popups[(int)curStatus].SetActive(true);
    }
}
