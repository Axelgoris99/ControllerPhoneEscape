using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    public delegate void PhoneTiltedForward();
    public static event PhoneTiltedForward onForward;

    Quaternion inverseFlat;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        inverseFlat = Quaternion.Inverse(Input.gyro.attitude);
    }


    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    // Update is called once per frame
    void Update()
    {
        Quaternion orientation = Input.gyro.attitude * inverseFlat;
        Debug.Log("Orientation" + orientation.eulerAngles);
    }
}
