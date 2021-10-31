using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private Color32 blue = new Color32(96, 154, 255, 10);
    private Color32 red = new Color32(255, 96, 96, 10);

    private int prefab01;
    private int prefab02;
    private int prefab03;

    void Start()
    {
        value = 0;
        difficultySlider = GetComponent<Image>();
        difficultySlider.fillAmount = value;
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

    public void ChangeDifficulty(float changeValue)
    {
        prefab01 = GameObject.FindGameObjectsWithTag("Prefab1").Length;
        prefab02 = GameObject.FindGameObjectsWithTag("Prefab2").Length;
        prefab03 = GameObject.FindGameObjectsWithTag("Prefab3").Length;

        if (prefab01 > 10)
        {
            return;
        } else if (prefab02 > 10) 
        {
            return;
        } else if (prefab03 > 10)
        {
            return;
        } else
        {
            value += changeValue;
            Variables.Instance.waterUseRate = waterUseValue * prefab01;
            Variables.Instance.reproductionRate = reproductionValue * prefab02;
            Variables.Instance.waterStorageRate = waterStorageValue * prefab03;
        }
            
        if (value < 0)
        {
            value = 0;
        } else if (value > 1)
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
}
