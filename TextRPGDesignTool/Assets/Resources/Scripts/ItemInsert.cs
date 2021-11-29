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
        statDegree = new Dictionary<string, int>();
        canShowEvents = new Dictionary<string, bool>();

        itemCode = "4hwnx01jdxj";
        itemName = "의택이의 초록오른손";
        itemExplain = "의택이가 고추장아찌를 조져서 손이 초래졌다.. 오른손과 데이트를 하지 못한 의택이는 힘과 체력이 넘친다..!";

        statDegree["hp"] = 3;
        statDegree["power"] = 5;

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
        }
    }

    public void Print()
    {
        Debug.Log("itemCode = " + itemCode);
        Debug.Log("itemName = " + itemName);

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

public class ItemInsert : MonoBehaviour
{
    public Item curItem = new Item(true);
    private string lastName = "";

    private void Start()
    {
        curItem.Print();
    }

    private void curItemClear()
    {
        curItem = new Item(true);
    }

    void NameChangeCheck()
    {
        if (curItem.itemCode != lastName)
            curItemClear();
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

    public void ItemStatInsert(string stat,int degree)
    {
        curItem.statDegree[stat] = degree;        
    }

    public void ItemConditionInsert(string stat, int degree)
    {
        curItem.statDegree[stat] = degree;
    }

    public void InsertItem()
    {
        JsonManager.Instance.itemsDict[curItem.itemCode] = curItem;
        JsonManager.Instance.itemsDict[curItem.itemCode].Print();

        curItemClear();
    }
}
