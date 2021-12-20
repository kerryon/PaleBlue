using UnityEngine;

public class LevelInitiator : MonoBehaviour
{

    LevelLoader levelloader;

    public void NextChapter()
    {
        levelloader = GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>();
        levelloader.LoadNextLvl();
    }
}
