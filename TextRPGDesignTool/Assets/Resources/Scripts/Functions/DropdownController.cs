using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public ItemManager itemManager;
    public Transform par;
    public GameObject newDropdownObj;
    public GameObject dropdownPref;

    private Item item;
    private Dropdown statDropdown;
    private Dropdown degreeDropdown;

    private Color[] colorList = { Color.red, Color.blue, Color.green, Color.magenta };

    int statCount = 0;
    int degreeCount = 0;

    void Start()
    {
        item = itemManager.curItem;
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
        if (degreeDropdown.value == 0 || item.statDegree.ContainsKey(statDropdown.options[statCount].text) && item.statDegree[statDropdown.options[statCount].text] > 0)
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

        item.statDegree[statDropdown.options[statCount].text] = degreeCount;

        ResetNewDropDown();
    }

    public void DeleteDropdown(Transform tr)
    {
        Debug.Log(tr.Find("Text").GetComponent<Text>().text);

        item.statDegree[tr.Find("Text").GetComponent<Text>().text] = 0;

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
