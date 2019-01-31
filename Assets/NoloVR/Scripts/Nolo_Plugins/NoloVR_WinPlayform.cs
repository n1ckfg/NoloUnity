/*************************************************************
 * 
 *  Copyright(c) 2017 Lyrobotix.Co.Ltd.All rights reserved.
 *  NoloVR_WinPlayform.cs
 *   
*************************************************************/

using UnityEngine;
using System;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
public class NoloVR_WinPlayform : NoloVR_Playform
{
    NOLO.DisConnectedCallBack disconn;
    NOLO.ConnectedCallBack conn;
    public override bool InitDevice()
    {
        if (playformError == NoloError.None) return true;
        try
        {
            disconn = new NOLO.DisConnectedCallBack(DisConnectedCallBack);
            conn = new NOLO.ConnectedCallBack(ReconnectDeviceCallBack);
            NOLO.NOLOClient_V2_API.disConnenct_FunCallBack(disconn);
            NOLO.NOLOClient_V2_API.connectSuccess_FunCallBack(conn);
            NOLO.NOLOClient_V2_API.open_Nolo_ZeroMQ();
            playformError = NoloError.NoConnect;
        }
        catch (Exception ex)
        {
            Debug.Log("NoloVR_WinPlayform InitDevice:"+ex.Message);
            playformError = NoloError.ConnectFail;
            return false;
        }
        return true;
    }

    public override void DisconnectDevice()
    {
        Debug.Log("NoloVR_WinPlayform DisconnectDevice");
        playformError = NoloError.DisConnect;
        NOLO.NOLOClient_V2_API.close_Nolo_ZeroMQ();
    }

    public override void DisConnectedCallBack()
    {
        Debug.Log("disconnect nolo device");
        try
        {
            playformError = NoloError.NoConnect;
        }
        catch (Exception e)

        {
            Debug.Log("DisConnectedCallBack:"+e.Message);
            throw;
        }
    } 

    public override void ReconnectDeviceCallBack()
    {
        Debug.Log("reconnect nolo device success");
        try
        {
            playformError = NoloError.None;
        }
        catch (Exception e)
        {
            Debug.Log("ReconnectDevice:" + e.Message);
            throw;
        }

    }

    public override void TriggerHapticPulse(int deviceIndex, int intensity)
    {
        NOLO.NOLOClient_V2_API.Nolovr_TriggerHapticPulse (deviceIndex, intensity);
    }

    public override void SetHmdTrackingCenter(int type)
    {
        //NoloVR_Plugins.API.SetHmdType(type);
    }

    public override void Authentication(string appKey)
    {
        isAuthentication = true;
    }

    public override void ReportError(string msg)
    {
        throw new NotImplementedException(msg);
    }
}
#endif