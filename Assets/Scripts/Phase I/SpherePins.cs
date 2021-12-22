using UnityEngine;

public class SpherePins : MonoBehaviour
{
    private int PinCount = 0;
    public int PinsMax;
    public GameObject prefab;
    public float PlanetRadius;
    public GameObject PlanetOrigin;

    [Header("Interval in Minuten")]
    public float interval;
    private float minutes;
 
    public Sprite[] sprites;

    void Start()
    {
        minutes = interval;
    }

    void Update()
    {
        if (PinCount < PinsMax)
        {
            if (Variables.Instance.timespan.TotalMinutes > minutes)
            {
                minutes += interval;
                CreatePinPrefab(PinCount);
                PinCount++;
            }
        }
    }

    public void CreatePinPrefab(int pinCount)
    {
        Vector3 onPlanet = Random.onUnitSphere * PlanetRadius;
        prefab.GetComponentInChildren<SpriteRenderer>().sprite = sprites[pinCount];
        GameObject newObject = Instantiate(prefab, onPlanet, Quaternion.identity);
        newObject.SetActive(true);
        newObject.name = pinCount.ToString();
        newObject.transform.LookAt(PlanetOrigin.transform.position);
        newObject.transform.rotation = newObject.transform.rotation * Quaternion.Euler(-90, 0, 0);
        newObject.transform.parent = gameObject.transform;

        var sheet = new ES3Spreadsheet();
        sheet.Load("history.csv");
        for (int i = 0; i < Variables.Instance.historyCount; i++)
        {
            if (pinCount + 4 == sheet.GetCell<int>(1, i))
            {
                newObject.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 96, 96, 255);
                return;
            }
        }
    }
}