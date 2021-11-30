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

    private void Start()
    {
        GameObject gb = infoes[(int)curStatus];

        foreach (var cur in JsonManager.Instance.statsDict)
        {
            GameObject newPref = Instantiate(gb, infoPars[(int)curStatus]);
            newPref.transform.localScale = new Vector3(1, 1, 1);
            newPref.transform.Find("Code").GetComponent<Text>().text = cur.Value.statCode;
            newPref.transform.Find("Name").GetComponent<Text>().text = cur.Value.statName;
            newPref.transform.Find("Explain").GetComponent<InputField>().text = cur.Value.statExplain;

            Debug.Log(cur.Value.statExplain);
            newPref.transform.Find("Toggle").GetComponent<Toggle>().isOn = cur.Value.isDefaultStat;
            newPref.SetActive(true);
        }
    }

    public void SetStatus(int i)
    {
        curStatus = (STATUS)i;
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
