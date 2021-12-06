using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Common;

public class Difficulty : MonoBehaviour
{
    private Image difficultySlider;
    private float value;

    private float waterUseValue = 0.05f;
    private float reproductionValue = 0.03f;
    private float waterStorageValue = 0.02f;

    public Image waterUseValueColor;
    public Image reproductionValueColor;
    public Image waterStorageValueColor;

    public TMP_Text waterUseValueCount;
    public TMP_Text reproductionValueCount;
    public TMP_Text waterStorageValueCount;

    public LeanSpawn waterUseSpawner;
    public LeanSpawn reproductionSpawner;
    public LeanSpawn waterStorageSpawner;

    public GameObject continueBtn;

    private Color32 blue = new Color32(96, 154, 255, 10);
    private Color32 red = new Color32(255, 96, 96, 10);

    private int prefab01;
    private int prefab02;
    private int prefab03;

    private bool randomize;

    void Start()
    {
        value = 0;
        difficultySlider = GetComponent<Image>();
        difficultySlider.fillAmount = value;

        continueBtn.SetActive(false);
    }

    public void ChangeDifficulty()
    {
        randomize = false;
        StartCoroutine(DifficultyCounter());
    }

    public void ChangeDifficultyRandom()
    {
        randomize = true;
        StartCoroutine(DifficultyCounter());
    }

    private IEnumerator DifficultyCounter()
    {
        if (!randomize)
        {
            yield return new WaitForEndOfFrame();
            prefab01 = GameObject.FindGameObjectsWithTag("Prefab1").Length;
            prefab02 = GameObject.FindGameObjectsWithTag("Prefab2").Length;
            prefab03 = GameObject.FindGameObjectsWithTag("Prefab3").Length;
        }
        else
        {
            prefab01 = Random.Range(0, 10);
            prefab02 = Random.Range(0, 10);
            prefab03 = Random.Range(0, 10);

            DestroyAll("Prefab1");
            DestroyAll("Prefab2");
            DestroyAll("Prefab3");

            for (int i = 0; i < prefab01; i++)
            {
                waterUseSpawner.Spawn();
            }
            for (int i = 0; i < prefab02; i++)
            {
                reproductionSpawner.Spawn();
            }
            for (int i = 0; i < prefab03; i++)
            {
                waterStorageSpawner.Spawn();
            }
        }

        if (!continueBtn.activeSelf)
        {
            continueBtn.SetActive(true);
        }

        value = (waterUseValue * prefab01) + (reproductionValue * prefab02) + (waterStorageValue * prefab03);
        Variables.Instance.waterUseRate = 0.1f * prefab01;
        Variables.Instance.reproductionRate = 0.1f * prefab02;
        Variables.Instance.waterStorageRate = 0.1f * prefab03;

        if (value < 0)
        {
            value = 0;
        }
        else if (value > 1)
        {
            value = 1;
        }

        if (value < 0.3)
        {
            gameObject.GetComponentInChildren<TMP_Text>().text = "Einfach";
        }

        if (value > 0.3 && value < 0.7)
        {
            gameObject.GetComponentInChildren<TMP_Text>().text = "Medium";
        }

        if (value > 0.7)
        {
            gameObject.GetComponentInChildren<TMP_Text>().text = "Schwer";
        }

        difficultySlider.fillAmount = value;
        DisplayDifficultyValues(prefab01, prefab02, prefab03);
    }

    public void DisplayDifficultyValues(int prefab01, int prefab02, int prefab03)
    {
        waterUseValueCount.text = "Wasserverbrach\n—\n" + "<font=Fonts/Config-Bold>" + prefab01.ToString() + "</font>";
        reproductionValueCount.text = "Reproduktion\n—\n" + "<font=Fonts/Config-Bold>" + prefab02.ToString() + "</font>";
        waterStorageValueCount.text = "Auskommen ohne Wasser\n—\n" + "<font=Fonts/Config-Bold>" + prefab03.ToString() + "</font>";

        waterUseValueColor.color = Color.Lerp(blue, red, (float)prefab01 / 10);
        reproductionValueColor.color = Color.Lerp(blue, red, (float)prefab02 / 10);
        waterStorageValueColor.color = Color.Lerp(blue, red, (float)prefab03 / 10);
    }

    void DestroyAll(string tag)
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }
    }
}
