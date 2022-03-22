using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Endcard : MonoBehaviour
{
    private float waterUseRate_itemCount;
    private float reproductionRate_itemCount;
    private float waterStorageRate_itemCount;

    public Image randomEventIcon;
    public GameObject ConsequenceIconHolder;
    private int randomEventNum;

    public GameObject imgPrefab;
    public GameObject prefabHolder;
    public Sprite[] imgPrefabSprite;

    public Sprite[] randomEventSprite;
    public Sprite[] ConsequenceSprite;

    List<float> list = new List<float>();

    public TMP_Text _alive;
    public TMP_Text _dead;

    void Start()
    {
        waterUseRate_itemCount = Variables.Instance.waterUseRate * 10;
        reproductionRate_itemCount = Variables.Instance.reproductionRate * 10;
        waterStorageRate_itemCount = Variables.Instance.waterStorageRate * 10;

        randomEventNum = ES3.Load("randomEvent", 0);
        randomEventIcon.sprite = randomEventSprite[randomEventNum];

        for (int i = 0; i < 3; i++)
        {
            ConsequenceIconHolder.transform.GetChild(i).GetComponent<Image>().sprite = ConsequenceSprite[Random.Range(0, ConsequenceSprite.Length)]; // Richtig berechen
        }

        list.Add(waterUseRate_itemCount);
        list.Add(reproductionRate_itemCount);
        list.Add(waterStorageRate_itemCount);

        _alive.text = "Überlebende\n<font=Fonts/Config-text>" + Variables.Instance.human.ToString("n0").Replace(",", ".") + "</font>";
        _dead.text = "Tote (gesamt)\n<font=Fonts/Config-text>" + Variables.Instance.deaths.ToString("n0").Replace(",", ".") + "</font>";

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i]; j++)
            {
                imgPrefab.GetComponent<Image>().sprite = imgPrefabSprite[i];
                GameObject parameter = Instantiate(imgPrefab, prefabHolder.transform.GetChild(i));
                parameter.name = "p_" + i;
            }
        }
    }
}
