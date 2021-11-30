using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemCode; // itemCode
    public string itemName; // itemNameInKorean
    public string itemExplain;

    public Dictionary<string, int> statDegree;
    public Dictionary<string, bool> canShowEvents;
    public Item() 
    {
        itemCode = "";
        itemName = "";
        itemExplain = "";
        statDegree = new Dictionary<string, int>();
        canShowEvents = new Dictionary<string, bool>();

        canShowEvents["0"] = true;
    }

    public Item(bool isSet)
    {
        if (isSet)
        {
            itemCode = "";
            itemName = "";
            itemExplain = "";
            statDegree = new Dictionary<string, int>();
            canShowEvents = new Dictionary<string, bool>();

            canShowEvents["0"] = true;
        }
    }

    public void Print()
    {
        Debug.Log("itemCode = " + itemCode);
        Debug.Log("itemName = " + itemName);
        Debug.Log("itemExplain = " + itemExplain);

        foreach (var idx in statDegree)
        {
            Debug.Log(string.Format("statDegree[{0}] = {1}", idx.Key, idx.Value));
        }

        foreach (var idx in canShowEvents)
        {
            Debug.Log(string.Format("canShowEvents[{0}] = {1}", idx.Key, idx.Value));
        }
    }
}

public class ItemManager : MonoBehaviour
{
    public GameObject itemPopup;
    public Item curItem = new Item(true);
    private string lastName = "";

    private void CurItemClear()
    {
        curItem = new Item(true);
    }

    public void SetItemCode(string code)
    {
        curItem.itemCode = code;
    }

    public void SetItemName(string name)
    {
        curItem.itemName = name;
    }

    public void SetItemExplain(string exp)
    {
        curItem.itemExplain = exp;
    }

    public void ItemStatManager(string stat,int degree)
    {
        curItem.statDegree[stat] = degree;        
    }

    public void ItemConditionInsert(string stat, int degree)
    {
        curItem.statDegree[stat] = degree;
    }

    public void InsertItem()
    {
        if(curItem.itemCode == string.Empty || curItem.itemName == string.Empty || curItem.itemExplain == string.Empty ||  JsonManager.Instance.itemsDict.ContainsKey(curItem.itemCode))
        {
            Debug.Log("Failed Create, Already Same Item exists or Code, Name or Explain is empty.");
            return;
        }

        JsonManager.Instance.itemsDict[curItem.itemCode] = curItem;
        JsonManager.Instance.itemsDict[curItem.itemCode].Print();

        CurItemClear();

        itemPopup.SetActive(false);
    }
}
