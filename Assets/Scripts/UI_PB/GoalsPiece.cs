using TMPro;
using UnityEngine;

public class GoalsPiece : MonoBehaviour
{
    public TMP_Text description;
    Goals goals;
    private int[] seed;

    void Start()
    {
        goals = GameObject.FindGameObjectWithTag("Goals").GetComponent<Goals>();
        seed = new int[goals._goals.Length];

        Random.InitState((int)(Variables.Instance.waterUseRate + Variables.Instance.reproductionRate + Variables.Instance.waterStorageRate * 1000));
        for (int i = 0; i < goals._goals.Length; i++)
        {
            seed[i] = Random.Range(0, goals._goals.Length);
        }

        description.text = goals._goals[seed[transform.GetSiblingIndex()]].description;

        //Reset Seed
        //Random.InitState(System.Environment.TickCount);
    }
}
