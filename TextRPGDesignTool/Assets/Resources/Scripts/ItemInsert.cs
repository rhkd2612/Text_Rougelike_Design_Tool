using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInsert : MonoBehaviour
{
    public Item curItem;
    private string lastName = "";

    private void Start()
    {
        curItemClear();
    }

    public void curItemClear()
    {
        curItem = new Item();
    }

    void NameChangeCheck()
    {
        if (curItem.itemCode != lastName)
            curItemClear();
    }

    public void ItemStatInsert(string stat,int degree)
    {
        NameChangeCheck();

        curItem.statDegree[stat] = degree;        
    }

    public void ItemConditionInsert(string stat, int degree)
    {
        NameChangeCheck();

        curItem.statDegree[stat] = degree;
    }
}
