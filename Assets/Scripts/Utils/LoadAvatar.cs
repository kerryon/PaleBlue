using UnityEngine;
using UnityEngine.UI;

public class LoadAvatar : MonoBehaviour
{
    private Texture2D SourceImage;
    private Sprite MySprite;

    void Start()
    {
        SourceImage = Resources.Load<Texture2D>("avatar");
        MySprite = Sprite.Create(SourceImage, new Rect(0, 0, SourceImage.width, SourceImage.height), new Vector2(0, 0));
        gameObject.GetComponent<Image>().sprite = MySprite;
    }
}
