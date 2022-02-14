using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBaum : MonoBehaviour
{
    public GameObject plantingScreen;
    public GameObject endScreen;

    public void StartPlanting()
    {
        plantingScreen.SetActive(true);
        Invoke(nameof(StopPlanting), 10f);
    }

    private void StopPlanting()
    {
        plantingScreen.SetActive(false);
        endScreen.SetActive(true);
    }

    public void PlantTree()
    {
        Variables.Instance.h_agriculture -= 1000f;
    }

    public void ExitAction()
    {
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
