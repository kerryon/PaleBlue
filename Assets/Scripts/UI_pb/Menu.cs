using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    private bool toggleBool = false;
    public GameObject menuBtn;
    public GameObject menuScreen;
    public Animator menuTrigger;
    public Animator menuBtnTrigger;
    public Animator transition;

    public Animator textboxTrigger;
    public GameObject Textbox, Levels;
    public GameObject NameDisplay;

    public void Awake()
    {
        menuBtn.gameObject.SetActive(false);
        menuScreen.gameObject.SetActive(false);
        Textbox.gameObject.SetActive(false);

        for (int i = 0; i < Levels.transform.childCount; i++)
        {
            Levels.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(ShowMenuButton());
        NewScene();
        NameDisplay.GetComponent<TMP_Text>().text = (string)ES3.Load("NAME");
    }

    IEnumerator ShowMenuButton()
    {
        yield return new WaitForSeconds(15);

        menuBtn.gameObject.SetActive(true);
    }

    public void NewScene()
    {
        for (int i = 0; i < Variables.Instance.currentLevelIndex - 1; ++i)
        {
            Levels.transform.GetChild(i).gameObject.SetActive(true);
        }
        menuScreen.gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        StartCoroutine(MenuToggle());
    }

    IEnumerator MenuToggle()
    {
        menuBtnTrigger.SetTrigger("MenuBtnClicked");

        if (toggleBool == true)
        {
        menuTrigger.SetTrigger("MenuTrigger");
        yield return new WaitForSeconds(1);
        }
        toggleBool = !toggleBool;
        menuScreen.gameObject.SetActive(toggleBool);
    }

    public void BackToMenu()
    {
        StartCoroutine(MenuTransition());
    }

    IEnumerator MenuTransition()
    {
        transition.SetTrigger("LevelLoadStart");
        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void ToggleTextbox()
    {
        StartCoroutine(TextboxToggle());
    }

    IEnumerator TextboxToggle()
    {
        textboxTrigger.SetTrigger("MenuInfoToggle");

        yield return new WaitForSeconds(1);

        Textbox.gameObject.SetActive(false);
    }
}
