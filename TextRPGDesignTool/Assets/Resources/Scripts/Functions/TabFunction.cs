using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabFunction : MonoBehaviour
{
    public InputField[] inputfields;
    public Button createButton;
    int count = 0;

    private void OnEnable()
    {
        foreach (var i in inputfields)
            i.text = "";
    }
    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ++count;
                inputfields[count % inputfields.Length].ActivateInputField();
            }

            //if (Input.GetKeyDown(KeyCode.Return))
                //createButton.onClick.Invoke();
        }
    }
}
