using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum LookAtMode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private LookAtMode lookAtMode;

    private void LateUpdate()
    {
        switch (lookAtMode)
        {
            case LookAtMode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;

            case LookAtMode.LookAtInverted:
                var dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;

            case LookAtMode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;

            case LookAtMode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
