using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    public Light targetLight;
    [SerializeField]
    private ARCameraManager cameraManager;
    // Start is called before the first frame update
    void Start()
    {
        cameraManager.frameReceived += FrameUpdate;
    }

    void FrameUpdate(ARCameraFrameEventArgs frame)
    {
        if (frame.lightEstimation.mainLightColor.HasValue)
            targetLight.color = frame.lightEstimation.mainLightColor.Value;
        if (frame.lightEstimation.mainLightDirection.HasValue)
            targetLight.transform.rotation = Quaternion.LookRotation(frame.lightEstimation.mainLightDirection.Value);
        if (frame.lightEstimation.averageColorTemperature.HasValue)
            targetLight.colorTemperature = frame.lightEstimation.averageColorTemperature.Value;
    }
}
