using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    public Transform head_Transform;
    public Transform camera_Tranform;

    [Header("Head Bobbing")]
    public float bob_Frequency = 5f;
    public float bob_HorizontalAmplitude = .1f;
    public float bob_VerticalAmplitude = .1f;

    [Range(0, 1)] public float head_BobSmoothing = .1f;

    public bool isWalking;
    float walking_Time;
    Vector3 target_Camera_Position;

    private void FixedUpdate()
    {
        if (!isWalking)
        {
            walking_Time = 0;
        }
        else
        {
            walking_Time += Time.deltaTime;
        }

        //calculate the cam target pos
        target_Camera_Position = head_Transform.position + CalculateHeadBobOffSet(walking_Time);

        camera_Tranform.position = Vector3.Lerp(camera_Tranform.position, target_Camera_Position, head_BobSmoothing);

        if ((camera_Tranform.position - target_Camera_Position).magnitude <= .001)
        {
            camera_Tranform.position = target_Camera_Position;
        }
    }

    Vector3 CalculateHeadBobOffSet(float t)
    {

        float horizontal_Offset = 0;
        float vertical_Offset = 0;
        Vector3 offset = Vector3.zero;

        if (t > 0 )
        {
            horizontal_Offset = Mathf.Cos(t * bob_Frequency) * bob_HorizontalAmplitude;
            vertical_Offset = Mathf.Sin(t * bob_Frequency * 2) * bob_VerticalAmplitude;

            offset = head_Transform.right * horizontal_Offset + head_Transform.up * vertical_Offset;
        }
        return offset;
    }
}
