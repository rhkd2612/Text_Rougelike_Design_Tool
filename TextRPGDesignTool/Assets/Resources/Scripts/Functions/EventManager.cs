using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject eventPopup;
    public Event curEvent = new Event();

    private void CurEventClear()
    {
        curEvent = new Event();
    }

    public void SetEventCode(string code)
    {
        curEvent.code = code;

        Debug.Log("yes1");
    }

    public void SetEventName(string name)
    {
        curEvent.name = name;
        Debug.Log("yes2");
    }

    public void SetEventExplain(string exp)
    {
        curEvent.explain = exp;
        Debug.Log("yes3");
    }

    public void SetEventSelection(string select)
    {
        curEvent.selection = select;

        Debug.Log("yes4");
    }

    public void RemoveEvent(Transform tr)
    {
        string text = tr.Find("Code").GetComponent<Text>().text;

        if (!JsonManager.Instance.eventsList.ContainsKey(text))
            Debug.Log("There's no key in List");

        JsonManager.Instance.eventsList.Remove(text);
        Destroy(tr.gameObject);
    }

    public void InsertEvent()
    {
        if (curEvent.code == string.Empty || curEvent.name == string.Empty || curEvent.explain == string.Empty || curEvent.selection == string.Empty)
        {
            Debug.Log("Failed Create, Already Same Event exists or Code, Name or Explain is empty.");
            return;
        }

        if (JsonManager.Instance.eventsList.ContainsKey(curEvent.code))
            Debug.Log("Overlapped Event");
        else
            BodyController.Instance.AddBody(curEvent, STATUS.EVENT, JsonManager.Instance.eventsList.IndexOfKey(curEvent.code));

        JsonManager.Instance.eventsList[curEvent.code] = curEvent;

        JsonManager.Instance.eventsList[curEvent.code].Print();

        CurEventClear();
        eventPopup.SetActive(false);
    }
}
