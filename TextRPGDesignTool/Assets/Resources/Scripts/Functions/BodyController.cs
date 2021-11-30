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

    public void SetStatus(int i)
    {
        curStatus = (STATUS)i;
    }

    public void CreateNewObject()
    {
        popups[(int)curStatus].SetActive(true);
    }
}
