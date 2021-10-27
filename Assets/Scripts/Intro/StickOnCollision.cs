using UnityEngine;

public class StickOnCollision : MonoBehaviour
{
    public GameObject startArrow;
    public GameObject text;

    public Intro collisionCounter;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // creates joint
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            // sets joint position to point of contact
            joint.anchor = collision.contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;

            collisionCounter.collisionCount += 1;

            if (collisionCounter.collisionCount == 1)
            {
                text.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (collisionCounter.collisionCount > 12)
        {
            startArrow.SetActive(true);
        }
    }
}
