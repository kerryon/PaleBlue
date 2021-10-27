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

    void Start()
    {
        value = 0;
        difficultySlider = GetComponent<Image>();
        difficultySlider.fillAmount = value;
    }

    public void ChangeDifficulty(float changeValue)
    {
        if (GameObject.FindGameObjectsWithTag("Prefab1").Length > 10)
        {
            return;
        } else if (GameObject.FindGameObjectsWithTag("Prefab2").Length > 10) 
        {
            return;
        } else if (GameObject.FindGameObjectsWithTag("Prefab3").Length > 10)
        {
            return;
        } else
        {
            value += changeValue;
            Variables.Instance.waterUseRate = waterUseValue * GameObject.FindGameObjectsWithTag("Prefab1").Length;
            Variables.Instance.reproductionRate = reproductionValue * GameObject.FindGameObjectsWithTag("Prefab2").Length;
            Variables.Instance.waterStorageRate = waterStorageValue * GameObject.FindGameObjectsWithTag("Prefab3").Length;
        }
            
        if (value < 0)
        {
            value = 0;
        } else if (value > 1)
        {
            value = 1;
        }
        difficultySlider.fillAmount = value;

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
    }
}
