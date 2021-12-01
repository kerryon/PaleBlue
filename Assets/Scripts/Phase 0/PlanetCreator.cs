using System.Collections;
using UnityEngine;
using SpaceGraphicsToolkit;

public class PlanetCreator : MonoBehaviour
{
    public GameObject planet;
    public Material planetMat;
    private Vector3 cometInitialPosition;
    private Vector3 cometNewPosition;
    private Quaternion cometInitialRotation;
    private Quaternion cometNewRotation;

    private Color planetBaseColor = Color.white;
    public Color planetNewColor;

    SgtPlanet sgtplanet;

    void Start()
    {
        cometNewPosition = new Vector3(0, 0, 0);
        cometNewRotation = Quaternion.Euler(0, 0, 0);
        planetMat.color = planetBaseColor;
    }

    void Update()
    {
        cometInitialPosition = gameObject.transform.position;
        cometInitialRotation = gameObject.transform.rotation;
    }

    public void DestroyAsteroid()
    {
        sgtplanet = planet.GetComponent<SgtPlanet>();
        StartCoroutine(AsteroidPositionLerper());
        
        planetMat.color = Color.Lerp(planetBaseColor, planetNewColor, Mathf.InverseLerp(70, 40, GameObject.FindGameObjectsWithTag("Asteroid").Length));
    }

    public IEnumerator AsteroidPositionLerper()
    {
        float i = 0f;
        float rate = (1f / 3f) * 1f;
        GetComponent<SgtFloatingOrbit>().enabled = false;
        GetComponent<SgtProceduralSpin>().enabled = false;
        while (i < 1f)
        {
            i += Time.deltaTime * rate;
            gameObject.transform.rotation = Quaternion.Lerp(cometInitialRotation, cometNewRotation, i);
            gameObject.transform.position = Vector3.Slerp(cometInitialPosition, cometNewPosition, i);

            sgtplanet.WaterLevel += 0.00002f;
            sgtplanet.Displacement -= 0.00002f;

            yield return null;
        }
        Destroy(gameObject);

    }
}
