using UnityEngine;

public class DissolveIntro : MonoBehaviour
{

    public Material material;

    float fade = -0.9f;
    float multiplier = 0.001f;

    void Start()
    {
        if (Variables.Instance.currentLevelIndex > 4)
        {
            Destroy(gameObject);
        }
        else
        {
            material.SetFloat("_EllipseSize", fade);
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
                Destroy(gameObject);
            }

            material.SetFloat("_EllipseSize", fade);
    }
}
