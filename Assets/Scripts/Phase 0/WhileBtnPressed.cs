using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Common;

public class WhileBtnPressed : MonoBehaviour, IPointerUpHandler
{
    public GameObject planet;
    bool isAdding = false;
    bool isRemoving = false;

    void Update()
    {
        if (isAdding)
        {
            planet.GetComponent<LeanManualRescale>().AddScaleA(0.1f);
            return;
        }
        if (isRemoving)
        {
            planet.GetComponent<LeanManualRescale>().AddScaleA(-0.1f);
            return;
        }

        //Feedbacksystem
    }

    public void AddMass()
    {
        isAdding = true;
    }

    public void RemoveMass()
    {
        isRemoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isAdding = false;
        isRemoving = false;
    }
}