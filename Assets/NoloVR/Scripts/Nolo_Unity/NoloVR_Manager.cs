/*************************************************************
 * 
 *  Copyright(c) 2017 Lyrobotix.Co.Ltd.All rights reserved.
 *  NoloVR_Manager.cs
 *   
*************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class NoloVR_Manager : MonoBehaviour
{
   
    [Tooltip("Camera's rotation should be changed when the app running")]
    public GameObject VRCamera;
    [Tooltip("Double click turnaround button")]
    public TurnAroundButtonType turnAroundButtonType;
    [HideInInspector]
    public NoloVR_TrackedDevice[] objects;

    AndroidJavaClass unityPlayer;
    AndroidJavaObject currentActivity;
    AndroidJavaObject context;
    AndroidJavaObject jc, jo;

    void Awake()
    {
        NoloVR_System.GetInstance().objects = GameObject.FindObjectsOfType<NoloVR_TrackedDevice>();
        NoloVR_System.GetInstance().VRCamera = this.VRCamera;
    }

    void Update ()
    {
        if (turnAroundButtonType!= TurnAroundButtonType.Null)
        {
            TurnAroundEventsMonitor();
        }
        Recenter();
    }

    //turn around about
    private int leftcontrollerTurn_PreFrame = -1;
    private int rightcontrollerTurn_PreFrame = -1;
    private int turnAroundSpacingFrame = 20;
    void TurnAroundEventsMonitor()
    {
        //leftcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp((uint)1 << (int)turnAroundButtonType))
        {
            if (Time.frameCount - leftcontrollerTurn_PreFrame <= turnAroundSpacingFrame)
            {
                NOLO_Events.Send(NOLO_Events.EventsType.TurnAround);
                leftcontrollerTurn_PreFrame = -1;
            }
            else
            {
                leftcontrollerTurn_PreFrame = Time.frameCount;
            }
        }
        //rightcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp((uint)1 << (int)turnAroundButtonType))
        {
            if (Time.frameCount - rightcontrollerTurn_PreFrame <= turnAroundSpacingFrame)
            {
                NOLO_Events.Send(NOLO_Events.EventsType.TurnAround);
                rightcontrollerTurn_PreFrame = -1;
            }
            else
            {
                rightcontrollerTurn_PreFrame = Time.frameCount;
            }
        }
    }
    //recenter about
    private int leftcontrollerRecenter_PreFrame = -1;
    private int rightcontrollerRecenter_PreFrame = -1;
    private int recenterSpacingFrame = 20;

    void Recenter()
    {
        //leftcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.System))
        {
            if (Time.frameCount - leftcontrollerRecenter_PreFrame <= recenterSpacingFrame)
            {
                UnityEngine.XR.InputTracking.Recenter();
                leftcontrollerRecenter_PreFrame = -1;
            }
            else
            {
                leftcontrollerRecenter_PreFrame = Time.frameCount;
            } 
        }
        //rightcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.System))
        {
            if (Time.frameCount - rightcontrollerRecenter_PreFrame <= recenterSpacingFrame)
            {
                UnityEngine.XR.InputTracking.Recenter();
                rightcontrollerRecenter_PreFrame = -1;
            }
            else
            {
                rightcontrollerRecenter_PreFrame = Time.frameCount;
            }
        }
    }

    void OnApplicationQuit()
    {
        //close connect from device
        Debug.Log("Nolo debug:Application quit");
        NoloVR_Playform.GetInstance().DisconnectDevice();
    }

}
