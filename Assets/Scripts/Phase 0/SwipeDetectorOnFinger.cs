using System.Collections;
using UnityEngine;

public class SwipeDetectorOnFinger : MonoBehaviour
{
    public GameObject CameraDragObject;
    public GameObject SwipeObject;
    public GameObject TrailObject;

    public void OnInitiation()
    {
        SwipeObject.SetActive(true);
        TrailObject.SetActive(true);
    }

    public void OnFinger()
    {
        StartCoroutine(Functionality());
    }

    private IEnumerator Functionality()
    {
        CameraDragObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(TrailObject);
        Destroy(SwipeObject);
        
    }
}
