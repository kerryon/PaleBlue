using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionForschen : MonoBehaviour
{
    public GameObject result;
    public GameObject startUI;
    public GameObject exitUI;
    public TextAsset json;

    [System.Serializable]
    public class Topic
    {
        public string name;
        public string description;
    }

    [System.Serializable]
    public class TopicList
    {
        public Topic[] topic;
    }

    public TopicList topicList = new TopicList();

    public void Research()
    {
        topicList = JsonUtility.FromJson<TopicList>(json.text);

        int randomField = Random.Range(0, 9);
        StartCoroutine(WaitForResult(randomField));

        switch (randomField)
        {
            case 0:
                Variables.Instance.h_conflict -= 20000f;
                break;
            case 1:
                Variables.Instance.h_luxury -= 20000f;
                break;
            case 2:
                Variables.Instance.h_industry -= 20000f;
                break;
            case 3:
                Variables.Instance.h_agriculture -= 20000f;
                break;
            case 4:
                Variables.Instance.h_waste -= 20000f;
                break;
            case 5:
                Variables.Instance.h_urbanisation -= 20000f;
                break;
            case 6:
                Variables.Instance.h_energy -= 20000f;
                break;
            case 7:
                Variables.Instance.h_overfishing -= 20000f;
                break;
            case 8:
                Variables.Instance.h_wasteWater -= 20000f;
                break;
            case 9:
                Variables.Instance.h_waterStructure -= 20000f;
                break;
        }
    }

    IEnumerator WaitForResult(int n)
    {
        startUI.SetActive(false);

        yield return new WaitForSeconds(3);

        exitUI.SetActive(true);
        result.SetActive(true);
        result.GetComponentInChildren<TMP_Text>().text = topicList.topic[n].description;
    }

    public void ExitAction()
    {
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
