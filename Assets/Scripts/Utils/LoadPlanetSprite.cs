using UnityEngine;
using UnityEngine.UI;

public class LoadPlanetSprite : MonoBehaviour
{
    public Sprite[] PlanetSprites;
    private Sprite MySprite;

    void Start()
    {
        if (Variables.Instance.timespan.TotalHours < 3)
        {
            MySprite = PlanetSprites[0];
        }

        if (Variables.Instance.timespan.TotalHours > 3 && Variables.Instance.timespan.TotalHours < 4)
        {
            MySprite = PlanetSprites[0];
        }

        gameObject.GetComponent<Image>().sprite = MySprite;
    }
}
