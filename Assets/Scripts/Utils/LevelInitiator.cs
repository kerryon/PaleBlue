using UnityEngine;

public class LevelInitiator : MonoBehaviour
{

    LevelLoader levelloader;
    //Menu menu;

    public void NextChapter()
    {
        levelloader = GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>();
        //menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();

        levelloader.LoadNextLvl();
        //menu.AppendHistory(0);
    }
}
