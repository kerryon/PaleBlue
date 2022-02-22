using UnityEngine;

public class InitTextAnim : MonoBehaviour
{
    public InfoSwipe swipeScript;
    private int page;
    private bool switcher = true;

    void Update()
    {
        page = swipeScript.currentPage;
        if (page == 2 && switcher)
        {
            switcher = false;
            GetComponentInParent<Animator>().SetTrigger("TriggerText_1");
        }

        if (page == 3 && !switcher)
        {
            switcher = true;
            GetComponentInParent<Animator>().SetTrigger("TriggerText_2");
        }
    }
}
