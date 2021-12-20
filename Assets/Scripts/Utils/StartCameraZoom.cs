using UnityEngine;

public class StartCameraZoom : MonoBehaviour
{
    void Start()
    {
        if (Variables.Instance.currentLevelIndex > 4)
        {
            Destroy(gameObject.GetComponent<Animator>());
            Destroy(gameObject.GetComponent<StartCameraZoom>());
        }
    }
}
