using UnityEngine;
using UnityEngine.UI;

public class DestroyClone : MonoBehaviour
{
    private GameObject[] clone;
    private Button btn;
    public string prefabTag;

    void Start()
    {
        btn = GetComponent<Button>();
    }

    public void DestroyClones(string tag)
    {
        if (GameObject.FindGameObjectsWithTag(tag).Length > 0)
        {
            clone = GameObject.FindGameObjectsWithTag(tag);
            Destroy(clone[0]);
        }
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag(prefabTag).Length == 0)
        {
            btn.interactable = false;
        } else
        {
            btn.interactable = true;
        }

        if (GameObject.FindGameObjectsWithTag("Prefab1").Length > 10)
        {
            clone = GameObject.FindGameObjectsWithTag("Prefab1");
            Destroy(clone[0]);
        }
        if (GameObject.FindGameObjectsWithTag("Prefab2").Length > 10)
        {
            clone = GameObject.FindGameObjectsWithTag("Prefab2");
            Destroy(clone[0]);
        }
        if (GameObject.FindGameObjectsWithTag("Prefab3").Length > 10)
        {
            clone = GameObject.FindGameObjectsWithTag("Prefab3");
            Destroy(clone[0]);
        }

    }
}
