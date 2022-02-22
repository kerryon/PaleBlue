using System.Collections;
using UnityEngine;

public class SwipeDetectorOnFinger : MonoBehaviour
{
    public GameObject CameraDragObject;
    public GameObject SwipeObject;
    public GameObject TrailObject;

    public void OnInitiation()
    {
        Invoke(nameof(InitiationDelay), .5f);
    }

    private void InitiationDelay()
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
        GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>().AppendHistory(9);
        CameraDragObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(TrailObject);
        Destroy(SwipeObject);
    }
}
