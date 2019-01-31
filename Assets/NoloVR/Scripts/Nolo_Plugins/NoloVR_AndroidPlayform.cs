/*************************************************************
 * 
 *  Copyright(c) 2017 Lyrobotix.Co.Ltd.All rights reserved.
 *  NoloVR_AndroidPlayform.cs
 *   
*************************************************************/
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public class NoloVR_AndroidPlayform : NoloVR_Playform
{
    AndroidJavaClass unityPlayer;
    AndroidJavaObject currentActivity;
    AndroidJavaObject jc, jo;
    ConnectedStatusCallBackFunc func;

    public override bool InitDevice()
    {
        if (playformError == NoloError.None) return true;
        try
        {
            func = new ConnectedStatusCallBackFunc(ConnectedStatusCallBack);
            NoloVR_Plugins.API.SetConnectedStatus(func);

            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            jc = new AndroidJavaClass("com.nolovr.androidsdkclient.NoloVR");
            jo = jc.CallStatic<AndroidJavaObject>("getInstance", currentActivity);
            if (jo.Call<bool>("isStallServer"))
            {
                jo.Call("openServer");
                playformError = NoloError.None;
            }
        }
        catch (Exception e)
        {
            Debug.Log("NoloVR_AndroidPlayform InitDevice:error"+e.Message);
            playformError = NoloError.ConnectFail;
            return false;
        }
        return true;
    }

    public void ConnectedStatusCallBack(int status)
    {
        switch (status)
        {
            case 0:
                DisConnectedCallBack();
                break;
            case 1:
                ReconnectDeviceCallBack();
                break;
            default:
                break;
        }
    }

    public override void DisconnectDevice()
    {
        jo.Call("closeServer");
        unityPlayer = null;
        currentActivity = null;
        jo = null;
        jc = null;
        playformError = NoloError.DisConnect;
    }
     
    public override void ReconnectDeviceCallBack()
    {
        Debug.Log("nolo_android_ReconnectDeviceCallBack");
        playformError = NoloError.None;
    }

    public override void DisConnectedCallBack()
    {
        Debug.Log("nolo_android_DisConnectedCallBack");
        playformError = NoloError.DisConnect;
    }

    public override void TriggerHapticPulse(int deviceIndex, int intensity)
    {
        NoloVR_Plugins.API.Nolovr_TriggerHapticPulse(deviceIndex, intensity);
    }

    public override void SetHmdTrackingCenter(int type)
    {
        NoloVR_Plugins.API.SetHmdType(type);
    }

    public override void Authentication(string appKey)
    {
        try
        {
            jo.Call("setAppKey", appKey);
            isAuthentication = true;
        }
        catch (Exception ex)
        {
            jo.Call("reportError", ex.Message);
        }
    }

    public override void ReportError(string msg)
    {
        jo.Call("reportError", msg);
    }
}
