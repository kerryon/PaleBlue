using UnityEngine;

public class AddForce : MonoBehaviour
{
    private Vector2 velocity;

    void Start()
    {
        velocity = new Vector2(Random.Range(-15, 15), Random.Range(-15, 15));
        gameObject.GetComponent<Rigidbody2D>().AddForce(velocity, ForceMode2D.Impulse);
    }
}
