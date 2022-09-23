using UnityEngine;
public class camera_follow : MonoBehaviour
{
    public Transform target; // object for follow 
    public float smooth_speed = 0.125f; 
    public Vector3 offset; 

    void LateUpdate()
    {
        Vector3 desired_position = target.position + offset;
        Vector3 smoothed_position = Vector3.Lerp (Transform.position, desired_position,smooth_speed);
        Transform.position = smoothed_position;

        transform.LookAt(target); // rotation camera (3d effect)
    }
}