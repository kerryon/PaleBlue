using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    public Material MenuDis;
    public GameObject CameraPivot;
    private Vector3 currentEulerAngles;
    private Quaternion correctionEulerAngles;

    void Start()
    {
        currentEulerAngles = new Vector3(0, 0, 0);
        correctionEulerAngles = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            currentEulerAngles = Input.gyro.attitude.eulerAngles / 8;
            MenuDis.SetVector("_Rotation", currentEulerAngles);

            CameraPivot.transform.localRotation = correctionEulerAngles * GyroToUnity(Input.gyro.attitude);
        }
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
