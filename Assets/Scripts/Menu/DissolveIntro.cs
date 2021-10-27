using UnityEngine;

public class DissolveIntro : MonoBehaviour
{

    public Material material;
    public GameObject canvas;

    float fade = -0.9f;
    float multiplier = 0.001f;

    void Awake()
    {
        material.SetFloat("_EllipseSize", fade);
    }

    void Start()
    {
        if (Variables.Instance.currentLevelIndex > 3)
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
