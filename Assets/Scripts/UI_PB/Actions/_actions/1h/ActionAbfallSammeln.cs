using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionAbfallSammeln : MonoBehaviour
{
    public GameObject text;
    public GameObject itemInfo;

    private bool trashPicked = false;

    [Header("Scriptable Objects")]
    public ScriptableObjectItem[] itemInfoText;

    void Start()
    {
        transform.GetChild(1).GetComponent<SpaceGraphicsToolkit.SgtTerrainPrefabSpawner>().SharedMaterial = GameObject.FindGameObjectWithTag("Atmosphere").GetComponent<SpaceGraphicsToolkit.SgtSharedMaterial>();

        itemInfo.SetActive(false);
    }

    public void PickTrash()
    {
        if (text.activeSelf)
        {
            StartCoroutine(FadeTextOut());
        }

        PickRandomInfo();
        itemInfo.SetActive(true);

        if (!trashPicked)
        {
            Variables.Instance.h_waste -= 10000f;
            trashPicked = true;
        }
    }

    public void ExitAction()
    {
        trashPicked = false;
        GetComponentInParent<ActionList>().DestroyAction();
    }

    IEnumerator FadeTextOut()
    {
        text.GetComponent<Animator>().SetTrigger("FABTextTrigger");
        yield return new WaitForSeconds(1);
        text.SetActive(false);
    }

    public void PickRandomInfo()
    {
        int r = Random.Range(0, itemInfoText.Length);
        itemInfo.GetComponentInChildren<TMP_Text>().text = itemInfoText[r].description;
    }
}
