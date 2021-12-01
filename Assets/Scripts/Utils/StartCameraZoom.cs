using UnityEngine;

public class StartCameraZoom : MonoBehaviour
{
    void Start()
    {
        if (Variables.Instance.currentLevelIndex > 3)
        {
            Destroy(gameObject.GetComponent<Animator>());
            Destroy(gameObject.GetComponent<StartCameraZoom>());
        }
    }
}
