using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public STATUS status;

    public ItemManager itemManager;
    public CharManager charManager;
    public Transform par;
    public Transform inputfieldPar;
    public GameObject newDropdownObj;
    public GameObject dropdownPref;
    public GameObject newInputfieldObj;
    public GameObject inputfieldPref;

    [SerializeField]
    private Dropdown statDropdown;
    private Dropdown degreeDropdown;

    private Color[] colorList = { Color.red, Color.blue, Color.green, Color.magenta };

    int statCount = 0;
    int degreeCount = 0;

    void OnEnable()
    {
        statDropdown = newDropdownObj.transform.Find("stat").GetComponent<Dropdown>();
        degreeDropdown = newDropdownObj.transform.Find("degree").GetComponent<Dropdown>();
        AddDropdownOptions();
    }

    void AddDropdownOptions()
    {
        statDropdown.ClearOptions();

        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();

        foreach (var s in JsonManager.Instance.statsList.Keys)
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

    void ResetNewInputField()
    {
        newInputfieldObj.transform.Find("InputField").GetComponent<InputField>().text = string.Empty;
    }

    public void InsertNewDropdown()
    {
        if (!JsonManager.Instance.statsList.ContainsKey(statDropdown.options[statCount].text))
        {
            Debug.Log("There's no key");
            return;
        }

        switch (status)
        {
            case STATUS.ITEM:
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

                    GameObject newPref = Instantiate(dropdownPref, par);
                    newPref.transform.SetSiblingIndex(pos);
                    newPref.transform.Find("Text").GetComponent<Text>().text = statDropdown.options[statCount].text;
                    newPref.transform.Find("DropDown").GetComponent<Dropdown>().value = degreeCount;
                    newPref.transform.Find("Text").GetComponent<Text>().color = colorList[pos % colorList.Length];
                    newPref.SetActive(true);

                    itemManager.curItem.statDegree[statDropdown.options[statCount].text] = degreeCount;
                }
                break;
            case STATUS.CHARACTER:
                {
                    if (charManager.curCharacter.code == string.Empty || charManager.curCharacter.name == string.Empty || charManager.curCharacter.explain == string.Empty)
                    {
                        Debug.Log("Failed Create, Code, Name or Explain is empty.");
                        return;
                    }

                    if (degreeDropdown.value == 0 || charManager.curCharacter.statDegree.ContainsKey(statDropdown.options[statCount].text) && charManager.curCharacter.statDegree[statDropdown.options[statCount].text] > 0)
                    {
                        Debug.Log("Failed Create, Already Same Stat exists or Stat Degree is 0.");
                        return;
                    }

                    int pos = newDropdownObj.transform.GetSiblingIndex();

                    Debug.Log(pos);

                    GameObject newPref = Instantiate(dropdownPref, par);
                    newPref.transform.SetSiblingIndex(pos);
                    newPref.transform.Find("Text").GetComponent<Text>().text = statDropdown.options[statCount].text;
                    newPref.transform.Find("DropDown").GetComponent<Dropdown>().value = degreeCount;
                    newPref.transform.Find("Text").GetComponent<Text>().color = colorList[pos % colorList.Length];
                    newPref.SetActive(true);

                    charManager.curCharacter.statDegree[statDropdown.options[statCount].text] = degreeCount;
                }
                break;
        }

        ResetNewDropDown();
    }

    public void InsertNewDropdown(string st, int deg)
    {
        if (!JsonManager.Instance.statsList.ContainsKey(st))
            return;

        int pos = newDropdownObj.transform.GetSiblingIndex();

        GameObject newPref = Instantiate(dropdownPref, par);
        newPref.transform.SetSiblingIndex(pos);
        newPref.transform.Find("Text").GetComponent<Text>().text = st;
        newPref.transform.Find("DropDown").GetComponent<Dropdown>().value = deg;
        newPref.transform.Find("Text").GetComponent<Text>().color = colorList[pos % colorList.Length];
        newPref.SetActive(true);
    }

    public void InsertNewInputField()
    {
        int pos = newInputfieldObj.transform.GetSiblingIndex();
        string nText = newInputfieldObj.transform.Find("InputField").GetComponent<InputField>().text;

        Debug.Log(nText);

        if (nText == string.Empty)
        {
            Debug.Log("Text is Empty");
            return;
        }

        if (itemManager.curItem.showEvents.ContainsKey(nText))
        {
            Debug.Log(string.Format("Text {0} is already exists.",nText));
            return;
        }

        if (itemManager.curItem.code == string.Empty || itemManager.curItem.name == string.Empty || itemManager.curItem.explain == string.Empty)
        {
            Debug.Log("Failed Create, Code, Name or Explain is empty.");
            return;
        }

        GameObject newPref = Instantiate(inputfieldPref, inputfieldPar);
        newPref.transform.SetSiblingIndex(pos);
        newPref.transform.Find("InputField").GetComponent<InputField>().text = nText;
        newPref.SetActive(true);

        itemManager.curItem.showEvents[nText] = true;

        ResetNewInputField();
    }

    public void InsertNewInputField(string st, bool b)
    {
        int pos = newInputfieldObj.transform.GetSiblingIndex(); ;

        GameObject newPref = Instantiate(inputfieldPref, inputfieldPar);
        newPref.transform.SetSiblingIndex(pos);
        newPref.transform.Find("InputField").GetComponent<InputField>().text = st;
        newPref.SetActive(true);
    }

    public void ModifyDropdownValue(Transform tr)
    {
        switch (status)
        {
            case STATUS.ITEM:
                itemManager.curItem.Print();

                itemManager.curItem.statDegree[tr.Find("Text").GetComponent<Text>().text] = tr.Find("DropDown").GetComponent<Dropdown>().value;
                break;
            case STATUS.CHARACTER:
                charManager.curCharacter.Print();

                charManager.curCharacter.statDegree[tr.Find("Text").GetComponent<Text>().text] = tr.Find("DropDown").GetComponent<Dropdown>().value;
                break;
        }
    }

    public void DeleteDropdown(Transform tr)
    {
        switch (status)
        {
            case STATUS.ITEM:
                itemManager.curItem.statDegree.Remove(tr.Find("Text").GetComponent<Text>().text);
                break;
            case STATUS.CHARACTER:
                charManager.curCharacter.statDegree.Remove(tr.Find("Text").GetComponent<Text>().text);
                break;
        }

        Destroy(tr.gameObject);

        ResetNewDropDown();
    }

    public void DeleteInputField(Transform tr)
    {
        itemManager.curItem.showEvents.Remove(tr.Find("InputField").GetComponent<InputField>().text);

        Destroy(tr.gameObject);
    }

    public void SetStat(int i)
    {
        statCount = i;
    }

    public void SetDegree(int i)
    {
        degreeCount = i;
    }
}
