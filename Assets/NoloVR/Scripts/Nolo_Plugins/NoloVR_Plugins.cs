/*************************************************************
 * 
 *  Copyright(c) 2017 Lyrobotix.Co.Ltd.All rights reserved.
 *  NoloVR_Plugins.cs
 *   
*************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public enum TurnAroundButtonType
{
    Null = -1,
    Touchpad = 0,
    Menu = 2,
    Grip = 4
}

public enum NoloAndroidVRPlayform
{
    GearVR,
    CardBoard,
    DayDream,
    Other
}

public enum NoloDeviceType
{
    Hmd = 0,
    LeftController,
    RightController,
    BaseStation,
}
public enum NoloButtonID
{
    TouchPad = 0,
    Trigger,
    Menu,
    System,
    Grip,
    TouchPadUp = 8,
    TouchPadDown,
    TouchPadLeft,
    TouchPadRight,
}
public enum NoloTouchID
{
    TouchPad,
    Trigger
}
public enum NoloError
{
    None = 0,             //没有错误
    ConnectFail,          //连接失败
    NoConnect,            //未连接
    DisConnect,           //断开连接
    UnKnow,               //未知错误
}
public enum NoloTrackingStatus
{
    NotConnect = 0,
    Normal,
    OutofRange
}

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate void DisConnectedCallBack();
[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate void ConnectedCallBack();

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate void ConnectedStatusCallBackFunc(int status);


public class NoloVR_Plugins
{
    //total number
    public const int trackedDeviceNumber = 4;

    #region method
    public static Nolo_Transform GetPose(int deviceIndex)
    {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        return NOLO.NOLOClient_V2_API.GetPoseByDeviceType(deviceIndex);
#elif UNITY_ANDROID
       return new Nolo_Transform(API.GetPoseByDeviceType(deviceIndex));
#endif

    }
    public static Nolo_ControllerStates GetControllerStates(int deviceIndex)
    {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        return NOLO.NOLOClient_V2_API.GetControllerStatesByDeviceType(deviceIndex);
#elif UNITY_ANDROID
      return API.GetControllerStatesByDeviceType(deviceIndex);
#endif
    }
    public static int GetElectricity(int deviceIndex)
    {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        return NOLO.NOLOClient_V2_API.GetElectricityByDeviceType(deviceIndex);
#elif UNITY_ANDROID
      return API.GetElectricityByDeviceType(deviceIndex);
#endif

    }
    #endregion

    #region Struct
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Vector2
    {
        public float x;
        public float y;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Vector3
    {
        public float x;
        public float y;
        public float z;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Quaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Pose
    {
        public Nolo_Vector3 pos;
        public Nolo_Quaternion rot;
        public Nolo_Vector3 vecVelocity;
        public Nolo_Vector3 vecAngularVelocity;
        public int status;
        public bool bDeviceIsConnected;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_ControllerStates
    {
        public uint buttons;
        public uint touches;
        public Nolo_Vector2 touchpadAxis;
        public Nolo_Vector2 rAxis1;
        public Nolo_Vector2 rAxis2;
        public Nolo_Vector2 rAxis3;
        public Nolo_Vector2 rAxis4;
    }
    #endregion

    #region API
    public class API
    {
        public const string dllName = "libNoloVR";

        [DllImport(dllName, EntryPoint = "getElectricityByDeviceType")]
        public static extern int GetElectricityByDeviceType(int type);

        [DllImport(dllName, EntryPoint = "getPoseByDeviceType", CallingConvention = CallingConvention.StdCall)]
        public static extern Nolo_Pose GetPoseByDeviceType(int type);

        [DllImport(dllName, EntryPoint = "getControllerStatesByDeviceType")]
        public static extern Nolo_ControllerStates GetControllerStatesByDeviceType(int type);

        [DllImport(dllName, EntryPoint = "triggerHapticPulse")]
        public static extern bool Nolovr_TriggerHapticPulse(int type, int intensity);

        [DllImport(dllName, EntryPoint = "setHmdType")]
        public static extern void SetHmdType(int hmdType);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetConnectedStatus")]
        public static extern int SetConnectedStatus([MarshalAs(UnmanagedType.FunctionPtr)]ConnectedStatusCallBackFunc nfun);

    }
    #endregion
}

public class NoloVR_System
{
    private static NoloVR_System instance;
    public NoloVR_TrackedDevice[] objects;
    public GameObject VRCamera;

    private NoloVR_System()
    {
        NoloVR_Controller.Listen();
    }
    public static NoloVR_System GetInstance()
    {
        if (instance == null)
        {
            instance = new NoloVR_System();
        }
        return instance;
    }
    ~NoloVR_System()
    {
        NoloVR_Controller.Remove();
    }

}