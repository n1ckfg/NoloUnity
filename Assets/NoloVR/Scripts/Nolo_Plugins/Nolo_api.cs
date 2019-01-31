using System;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

//Version:V_0_1_RC


namespace NOLO
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Vector3
    {
        public float x;
        public float y;
        public float z;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Vector2
    {
        public float x;
        public float y;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Quaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    public enum EBattery{

        ShutDown = 0,
        Low,
        Middle,
        High
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_Pose
    {
        public Nolo_Vector3 pos;
        public Nolo_Quaternion rot;
        public Nolo_Vector3 vecVelocity;
        public Nolo_Vector3 vecAngularVelocity;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Nolo_ControllerStates
    {
        public uint buttons;
        public uint touches;
        public Nolo_Vector2 touchpadAxis;
    }
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void DisConnectedCallBack();
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void ConnectedCallBack();

    public class NOLOClient_V2_API
    {
        public const string dllName = "noloRuntime";
        
        public static Nolo_Transform GetPoseByDeviceType(int deviceIndex) {
            NoloVR_Plugins.Nolo_Pose result = new NoloVR_Plugins.Nolo_Pose();
            int battery = get_Nolo_Battery(deviceIndex);
            Nolo_Pose pose = get_Nolo_Pose(deviceIndex);
            result.pos.x = pose.pos.x;
            result.pos.y = pose.pos.y;
            result.pos.z = pose.pos.z;
            result.rot.w = pose.rot.w;
            result.rot.x = pose.rot.x;
            result.rot.y = pose.rot.y;
            result.rot.z = pose.rot.z;
            result.vecVelocity.x = pose.vecVelocity.x;
            result.vecVelocity.y = pose.vecVelocity.y;
            result.vecVelocity.z = pose.vecVelocity.z;
            result.vecAngularVelocity.x = pose.vecAngularVelocity.x;
            result.vecAngularVelocity.y = pose.vecAngularVelocity.y;
            result.vecAngularVelocity.z = pose.vecAngularVelocity.z;
            result.bDeviceIsConnected = battery > 0 ? true : false;
            return new Nolo_Transform(result);
        }
        public static NoloVR_Plugins.Nolo_ControllerStates GetControllerStatesByDeviceType(int deviceIndex)
        {
            NoloVR_Plugins.Nolo_ControllerStates result= new NoloVR_Plugins.Nolo_ControllerStates();
            Nolo_ControllerStates state = get_Nolo_ControllerStates(deviceIndex);
            result.buttons = state.buttons;
            result.touches = state.touches;
            result.touchpadAxis.x = state.touchpadAxis.x;
            result.touchpadAxis.y = state.touchpadAxis.y;
            return result;
        }
        public static int GetElectricityByDeviceType(int deviceIndex)
        {
            int battery = get_Nolo_Battery(deviceIndex);
            if (battery > 0 && battery < 33)
            {
                return 1;
            }
            else if (battery >= 33 && battery < 66)
            {
                return 2;
            }
            else if (battery >= 66 && battery < 254)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }

        [DllImport(dllName)]
        public static extern void open_Nolo_ZeroMQ();

        [DllImport(dllName)]
        public static extern void close_Nolo_ZeroMQ();

        [DllImport(dllName, EntryPoint = "get_Nolo_Battery")]
        public static extern int get_Nolo_Battery(int type);

        [DllImport(dllName, EntryPoint = "get_Nolo_Pose")]
        public static extern Nolo_Pose get_Nolo_Pose(int type);

        [DllImport(dllName, EntryPoint = "get_Nolo_ControllerStates")]
        public static extern Nolo_ControllerStates get_Nolo_ControllerStates(int type);

        [DllImport(dllName, EntryPoint = "set_Nolo_TriggerHapticPulse")]
        public static extern bool Nolovr_TriggerHapticPulse(int type, int intensity);

        [DllImport(dllName, EntryPoint = "registerDisConnectCallBack")]
        public static extern bool disConnenct_FunCallBack(DisConnectedCallBack callback);

        [DllImport(dllName, EntryPoint = "registerConnectSuccessCallBack")]
        public static extern bool connectSuccess_FunCallBack(ConnectedCallBack callback);

    }

}
