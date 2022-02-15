using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBaum : MonoBehaviour
{
    public GameObject plantingScreen;
    public GameObject endScreen;
    public GameObject exitBtn;
    public GameObject startBtn;

    public void StartPlanting()
    {
        plantingScreen.SetActive(true);
        startBtn.GetComponent<Animator>().SetTrigger("FABTrigger");
        Invoke(nameof(StopPlanting), 10f);
    }

    private void Update()
    {
        if (plantingScreen.activeSelf)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    PlantTree();
                }
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                PlantTree();
            }
#endif
        }
    }

    private void StopPlanting()
    {
        plantingScreen.SetActive(false);
        endScreen.SetActive(true);
        exitBtn.SetActive(true);
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
