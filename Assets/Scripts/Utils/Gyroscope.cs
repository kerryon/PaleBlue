using System;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    public Material MenuDis;
    public GameObject Planet;
    private Vector3 currentEulerAngles;

    void Start()
    {
        currentEulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            currentEulerAngles = Input.gyro.attitude.eulerAngles / 7;
            MenuDis.SetVector("_Rotation", currentEulerAngles);
            Planet.transform.localRotation = GyroToUnity(Input.gyro.attitude);
        }
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
