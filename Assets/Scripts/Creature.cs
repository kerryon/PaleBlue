using UnityEngine;
using UnityEngine.UI;
using LylekGames;
using System.Collections;

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

    IEnumerator SaveAndLoadAvatar() // Delete wenn vorhanden einbauen
    {
        while (!Resources.Load<Texture2D>("avatar"))
        {
            DrawScript.drawScript.Save("avatar");
            yield return null;
        }
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

    public void SetStatsToCards()
    {
        Props.SetActive(false);
        Card.SetActive(true);
    }
}
