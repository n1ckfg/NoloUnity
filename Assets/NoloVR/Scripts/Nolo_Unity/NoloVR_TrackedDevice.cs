/*************************************************************
 * 
 *  Copyright(c) 2017 Lyrobotix.Co.Ltd.All rights reserved.
 *  NoloVR_TrackedDevice.cs
 *   
*************************************************************/

using UnityEngine;

public class NoloVR_TrackedDevice : MonoBehaviour {

    public NoloDeviceType deviceType;
    private GameObject vrCamera;
    void Start()
    {
        //get vrcamera
        vrCamera = NoloVR_System.GetInstance().VRCamera;
    }
	void Update () {
        if (NoloVR_Playform.GetInstance().GetPlayformError() != NoloError.None)
        {
            return;
        }
        UpdatePose();
    }
    private float camerayaw;
    private float noloyaw;
    private float resetyaw;
    private float presetyaw;
    private float resultyaw;


    void UpdatePose()
    {
        var pose = NoloVR_Controller.GetDevice(deviceType).GetPose();
        
        if (deviceType == NoloDeviceType.Hmd)
        {
            if (vrCamera == null)
            {
                Debug.LogError("Not find your vr camera");
                transform.localPosition = pose.pos;
                return;
            }
            //Correct the camera yaw
            noloyaw = pose.rot.eulerAngles.y;
            resetyaw = noloyaw - camerayaw > 0 ? noloyaw - camerayaw : noloyaw - camerayaw + 360;
            presetyaw = transform.localRotation.eulerAngles.y;
            resultyaw = resetyaw - presetyaw;
            transform.localRotation = NoloVR_Utils.GetRecenterRot(pose.rot, presetyaw, resultyaw);

            //Correct the camera position
            camerayaw = vrCamera.transform.localRotation.eulerAngles.y;
            var cameraLoaclPosition = transform.localRotation * vrCamera.transform.localPosition;
            transform.localPosition = pose.pos - cameraLoaclPosition;
        }
        else
        {
            transform.localPosition = pose.pos;
            transform.localRotation = pose.rot;
        }
    }
}
