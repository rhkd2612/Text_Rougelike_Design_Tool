using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{
    public GameObject charPopup;
    public Character curCharacter = new Character();

    private void CurCharacterClear()
    {
        curCharacter = new Character();
    }

    public void SetCharacterCode(string code)
    {
        curCharacter.code = code;
    }

    public void ModifyCharacterSelect(Transform tr)
    {
        curCharacter = (Character)JsonManager.Instance.charsList[tr.Find("Code").GetComponent<Text>().text];
    }

    public void SetCharacterName(string name)
    {
        curCharacter.name = name;
    }

    public void SetCharacterExplain(string exp)
    {
        curCharacter.explain = exp;
    }

    public void CharacterStatManager(string stat, int degree)
    {
        curCharacter.statDegree[stat] = degree;
    }

    public void CharacterConditionInsert(string stat, int degree)
    {
        curCharacter.statDegree[stat] = degree;
    }

    public void RemoveCharacter(Transform tr)
    {
        string text = tr.Find("Code").GetComponent<Text>().text;

        if (!JsonManager.Instance.charsList.ContainsKey(text))
            Debug.Log("There's no key in List");

        JsonManager.Instance.charsList.Remove(text);
        Destroy(tr.gameObject);
    }

    public void InsertCharacter()
    {
        if (curCharacter.code == string.Empty || curCharacter.name == string.Empty || curCharacter.explain == string.Empty)
        {
            Debug.Log("Failed Create, Already Same Character exists or Code, Name or Explain is empty.");
            return;
        }

        if (JsonManager.Instance.charsList.ContainsKey(curCharacter.code))
            Debug.Log("Overlapped char");
        else
            BodyController.Instance.AddBody(curCharacter, STATUS.CHARACTER, JsonManager.Instance.charsList.IndexOfKey(curCharacter.code));

        JsonManager.Instance.charsList[curCharacter.code] = curCharacter;
        JsonManager.Instance.charsList[curCharacter.code].Print();
        BodyController.Instance.ModifyBody(curCharacter, STATUS.CHARACTER);

        CurCharacterClear();

        charPopup.SetActive(false);
    }
}
