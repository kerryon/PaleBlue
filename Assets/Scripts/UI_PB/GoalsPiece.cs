using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalsPiece : MonoBehaviour
{
    public TMP_Text description;
    Goals goals;
    private int[] seed;
    List<int> usedValues = new List<int>();

    void Start()
    {
        goals = GameObject.FindGameObjectWithTag("Goals").GetComponent<Goals>();
        seed = new int[goals._goals.Length];

        Random.InitState((int)(Variables.Instance.waterUseRate + Variables.Instance.reproductionRate + Variables.Instance.waterStorageRate * 1000));
        for (int i = 0; i < goals._goals.Length; i++)
        {
            seed[i] = UniqueRandomInt(0, goals._goals.Length);
        }

        description.text = goals._goals[seed[transform.GetSiblingIndex()]].description;

        //Reset Seed
        Random.InitState(System.Environment.TickCount);
    }

    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (usedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        usedValues.Add(val);
        return val;
    }
}
