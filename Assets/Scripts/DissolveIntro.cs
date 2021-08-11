using UnityEngine;

public class DissolveIntro : MonoBehaviour
{

    public Material material;
    public GameObject canvas;

    private int currentLevelIndex;

    float fade = -0.9f;
    float multiplier = 0.001f;

    void Awake()
    {
        material.SetFloat("_EllipseSize", fade);
        currentLevelIndex = ES3.Load("CLI", 3);
    }

    void Start()
    {
        if (currentLevelIndex > 3)
        {
            Destroy(canvas);
        }
    }

    void Update()
    {
        fade += Time.deltaTime * multiplier;

        if (multiplier <= 0.1)
        {
            multiplier += 0.001f;
        }

            if (fade >= 1f)
            {
                fade = 1f;
                Destroy(canvas);
            }

            material.SetFloat("_EllipseSize", fade);
    }
}
