﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public ItemManager itemManager;
    public Transform par;
    public GameObject newDropdownObj;
    public GameObject dropdownPref;

    public bool isModify = false;

    private Dropdown statDropdown;
    private Dropdown degreeDropdown;

    private Color[] colorList = { Color.red, Color.blue, Color.green, Color.magenta };

    int statCount = 0;
    int degreeCount = 0;

    void Start()
    {
        if (isModify)
            itemManager.curItem = (Item)JsonManager.Instance.itemsList[itemManager.curItem.code];

        statDropdown = newDropdownObj.transform.Find("stat").GetComponent<Dropdown>();
        degreeDropdown = newDropdownObj.transform.Find("degree").GetComponent<Dropdown>();
        AddDropdownOptions();
    }

    void AddDropdownOptions()
    {
        statDropdown.ClearOptions();

        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();

        foreach(var s in JsonManager.Instance.statsList.Keys)
        {
            Dropdown.OptionData od = new Dropdown.OptionData(s);
            list.Add(od);
        }

        statDropdown.AddOptions(list);
    }

    void ResetNewDropDown()
    {
        statDropdown.value = 0;
        degreeDropdown.value = 0;
    }

    public void InsertNewDropdown()
    {
        if (itemManager.curItem.code == string.Empty || itemManager.curItem.name == string.Empty || itemManager.curItem.explain == string.Empty)
        {
            Debug.Log("Failed Create, Code, Name or Explain is empty.");
            return;
        }

        if (degreeDropdown.value == 0 || itemManager.curItem.statDegree.ContainsKey(statDropdown.options[statCount].text) && itemManager.curItem.statDegree[statDropdown.options[statCount].text] > 0)
        {
            Debug.Log("Failed Create, Already Same Stat exists or Stat Degree is 0.");
            return;
        }

        int pos = newDropdownObj.transform.GetSiblingIndex();

        Debug.Log(pos);

        GameObject newPref = Instantiate(dropdownPref,par);
        newPref.transform.SetSiblingIndex(pos);
        newPref.transform.Find("Text").GetComponent<Text>().text = statDropdown.options[statCount].text;
        newPref.transform.Find("DropDown").GetComponent<Dropdown>().value = degreeCount;
        newPref.transform.Find("Text").GetComponent<Text>().color = colorList[pos % colorList.Length];
        newPref.SetActive(true);

        itemManager.curItem.statDegree[statDropdown.options[statCount].text] = degreeCount;

        ResetNewDropDown();
    }

    public void InsertNewDropdown(string st, int deg)
    {
        int pos = newDropdownObj.transform.GetSiblingIndex();

        GameObject newPref = Instantiate(dropdownPref, par);
        newPref.transform.SetSiblingIndex(pos);
        newPref.transform.Find("Text").GetComponent<Text>().text = st;
        newPref.transform.Find("DropDown").GetComponent<Dropdown>().value = deg;
        newPref.transform.Find("Text").GetComponent<Text>().color = colorList[pos % colorList.Length];
        newPref.SetActive(true);
    }

    public void ModifyDropdownValue(Transform tr)
    {
        Debug.Log("ModifyDropdownValue");
        itemManager.curItem.Print();

        itemManager.curItem.statDegree[tr.Find("Text").GetComponent<Text>().text] = tr.Find("DropDown").GetComponent<Dropdown>().value;
    }
    public void DeleteDropdown(Transform tr)
    {
        itemManager.curItem.statDegree[tr.Find("Text").GetComponent<Text>().text] = 0;

        Destroy(tr.gameObject);

        ResetNewDropDown();
    }

    public void SetStat(int i)
    {
        statCount = i;
    }
    public void SetDegree(int i)
    {
        degreeCount = i;

        Debug.Log(i);
    }
}
