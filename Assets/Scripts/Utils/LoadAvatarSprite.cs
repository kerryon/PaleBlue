using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadAvatarSprite : MonoBehaviour
{
    private Sprite MySprite;

    void Start()
    {
        byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "avatar.png"));
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(bytes);
        MySprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
        gameObject.GetComponent<Image>().sprite = MySprite;
    }
}
