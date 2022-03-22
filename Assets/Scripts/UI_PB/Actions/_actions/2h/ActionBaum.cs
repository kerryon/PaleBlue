using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionBaum : MonoBehaviour
{
    public GameObject plantingScreen;
    public GameObject endScreen;
    public GameObject exitBtn;
    public GameObject startBtn;
    public TMP_Text text;

    public GameObject PlanetOrigin;
    public GameObject treePrefab;

    private int treeCount;

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

        endScreen.GetComponentInChildren<TMP_Text>().text = "Du hast " + treeCount + " Wälder gepflanzt!";
        text.text = "Der Planet kann nun wieder besser atmen.";

        endScreen.SetActive(true);
        exitBtn.SetActive(true);
    }

    public void PlantTree()
    {
        Variables.Instance.h_agriculture -= 1000f;
    }

    public void CountTree()
    {
        treeCount += 1;
        CreateTreePrefab(treeCount);
    }

    private void CreateTreePrefab(int tree)
    {
        Vector3 onPlanet = Random.onUnitSphere * 100;
        GameObject newObject = Instantiate(treePrefab, onPlanet, Quaternion.identity, gameObject.transform);
        newObject.SetActive(true);
        newObject.name = tree.ToString();
        newObject.transform.LookAt(transform.position);
        newObject.transform.rotation = newObject.transform.rotation * Quaternion.Euler(-90, 0, 0);
    }

    public void ExitAction()
    {
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
