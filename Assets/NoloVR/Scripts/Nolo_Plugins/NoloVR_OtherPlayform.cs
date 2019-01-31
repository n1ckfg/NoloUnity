using UnityEngine;
using System.Collections;
using System;

public class NoloVR_OtherPlayform : NoloVR_Playform {

    public override void Authentication(string appKey)
    {
        Debug.Log("NoloVR_OtherPlayform Authentication");
    }

    public override void DisconnectDevice()
    {
        Debug.Log("NoloVR_OtherPlayform DisconnectDevice");
    }

    public override void DisConnectedCallBack()
    {
        Debug.Log("NoloVR_OtherPlayform DisConnectedCallBack");
    }

    public override bool InitDevice()
    {
        Debug.Log("NoloVR_OtherPlayform InitDevice");
        return true;
    }

    public override void ReconnectDeviceCallBack()
    {
        Debug.Log("NoloVR_OtherPlayform ReconnectDeviceCallBack");
    }

    public override void SetHmdTrackingCenter(int type)
    {
        Debug.Log("NoloVR_OtherPlayform SetHmdTrackingCenter");
    }

    public override void TriggerHapticPulse(int deviceIndex, int intensity)
    {
        Debug.Log("NoloVR_OtherPlayform TriggerHapticPulse");
    }

    public override void ReportError(string msg)
    {

    }
}
