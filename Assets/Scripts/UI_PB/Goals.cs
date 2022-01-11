using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefabWrapper;
    public ScriptableObjectGoals[] _goals;

    public void CreateGoals(int goalCount)
    {
        for (int i = 0; i < goalCount; i++)
        {
            GameObject newGoal = Instantiate(prefab);
            newGoal.SetActive(true);
            newGoal.name = "goal";
            newGoal.transform.SetParent(prefabWrapper.transform, false);
        }
    }

    void OnEnable()
    {
        CreateGoals((int)Variables.Instance.timespan.TotalDays + 1);
    }

    void OnDisable()
    {
        for (int i = 0; i < prefabWrapper.transform.childCount; i++)
        {
            Destroy(prefabWrapper.transform.GetChild(i).gameObject);
        }
    }
}
