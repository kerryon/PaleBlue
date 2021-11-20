using UnityEngine;
using UnityEngine.UI;
using LylekGames;
using System.Collections;
using TMPro;

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
        //if (Resources.Load<Texture2D>("avatar"))
        //{
        //    DrawScript.drawScript.Save("avatar");
        //    yield return null;
        //}
        DrawScript.drawScript.Save("avatar");
        yield return new WaitForSeconds(1);
        Drawing.SetActive(false);
        SetSprite();
        Props.SetActive(true);
    }

    public void SetSprite()
    {
        SourceImage = Resources.Load<Texture2D>("avatar");
        MySprite = Sprite.Create(SourceImage, new Rect(0, 0, SourceImage.width, SourceImage.height), new Vector2(0, 0));
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
