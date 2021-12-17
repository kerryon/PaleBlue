using UnityEngine;
using UnityEngine.UI;
using LylekGames;
using System.Collections;
using TMPro;
using System.IO;

public class Creature : MonoBehaviour
{
    private Texture2D SourceImage;
    private Sprite MySprite;
    public GameObject Drawing;
    public GameObject Props;
    public GameObject Card;
    private GameObject[] Avatar;

    public void LoadSprite()
    {
        StartCoroutine(SaveAndLoadAvatar());
    }

    IEnumerator SaveAndLoadAvatar()
    {
        DrawScript.drawScript.Save("avatar");
        yield return new WaitForSeconds(1);
        Drawing.SetActive(false);
        SetSprite();
        Props.SetActive(true);
    }

    public void SetSprite()
    {
        byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "avatar.png"));
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(bytes);
        MySprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
        Avatar = GameObject.FindGameObjectsWithTag("Avatar");
        for (int i = 0; i < Avatar.Length; i++)
        {
            Avatar[i].GetComponent<Image>().sprite = MySprite;
        }
    }

    public void StatsToCards()
    {
        Props.SetActive(false);
        Card.SetActive(true);
    }

    public void SaveNameOfLife()
    {
        string name = gameObject.GetComponentInChildren<TMP_InputField>().text;
        ES3.Save("LIFE", name);
    }
}
