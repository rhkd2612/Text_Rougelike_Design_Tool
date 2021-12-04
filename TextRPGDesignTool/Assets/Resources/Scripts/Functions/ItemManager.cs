using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPopup;
    public Item curItem = new Item();
    private string lastName = "";

    private void CurItemClear()
    {
        curItem = new Item();
    }

    public void SetItemCode(string code)
    {
        curItem.code = code;
    }

    public void ModifyItemSelect(Transform tr)
    {
        curItem = (Item)JsonManager.Instance.itemsList[tr.Find("Code").GetComponent<Text>().text];

        curItem.Print();
    }

    public void SetItemName(string name)
    {
        curItem.name = name;
    }

    public void SetItemExplain(string exp)
    {
        curItem.explain = exp;
    }

    public void ItemStatManager(string stat,int degree)
    {
        curItem.statDegree[stat] = degree;        
    }

    public void ItemConditionInsert(string stat, int degree)
    {
        curItem.statDegree[stat] = degree;
    }

    public void RemoveItem(Transform tr)
    {
        JsonManager.Instance.statsList.Remove(tr.Find("Code").GetComponent<Text>().text);
        Destroy(tr.gameObject);
    }

    public void InsertItem()
    {
        if(curItem.code == string.Empty || curItem.name == string.Empty || curItem.explain == string.Empty)
        {
            Debug.Log("Failed Create, Already Same Item exists or Code, Name or Explain is empty.");
            return;
        }

        if (JsonManager.Instance.itemsList.ContainsKey(curItem.code))
            Debug.Log("Overlapped item");
        else
            BodyController.Instance.AddBody(curItem, STATUS.ITEM, JsonManager.Instance.itemsList.IndexOfKey(curItem.code));

        curItem.Print();

        JsonManager.Instance.itemsList[curItem.code] = curItem;
        JsonManager.Instance.itemsList[curItem.code].Print();


        CurItemClear();

        itemPopup.SetActive(false);
    }
}
