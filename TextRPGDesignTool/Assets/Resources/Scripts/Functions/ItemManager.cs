using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPopup;
    public Item curItem = new Item();

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

    public void SetItemIsConsume(bool isC)
    {
        curItem.isConsume = isC;
    }

    public void SetItemShowAllScenes(bool isShowAllScene)
    {
        curItem.isShownAllScenes = isShowAllScene;
    }

    public void ItemStatManager(string stat, int degree)
    {
        curItem.statDegree[stat] = degree;
    }

    public void ItemConditionInsert(string stat, bool b)
    {
        curItem.showEvents[stat] = b;
    }

    public void RemoveItem(Transform tr)
    {
        string text = tr.Find("Code").GetComponent<Text>().text;

        if (!JsonManager.Instance.itemsList.ContainsKey(text))
            Debug.Log("There's no key in List");

        JsonManager.Instance.itemsList.Remove(text);
        Destroy(tr.gameObject);
    }

    public void InsertItem()
    {
        if (curItem.code == string.Empty || curItem.name == string.Empty || curItem.explain == string.Empty)
        {
            Debug.Log("Failed Create, Already Same Item exists or Code, Name or Explain is empty.");
            return;
        }

        if (JsonManager.Instance.itemsList.ContainsKey(curItem.code))
            Debug.Log("Overlapped item");
        else
            BodyController.Instance.AddBody(curItem, STATUS.ITEM, JsonManager.Instance.itemsList.IndexOfKey(curItem.code));

        JsonManager.Instance.itemsList[curItem.code] = curItem;
        JsonManager.Instance.itemsList[curItem.code].Print();
        BodyController.Instance.ModifyBody(curItem, STATUS.ITEM);

        CurItemClear();
        itemPopup.SetActive(false);
    }
}
