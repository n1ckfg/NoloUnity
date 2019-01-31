using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Test : MonoBehaviour {
    private Text UIText;
	void Start () {
        UIText = GetComponent<Text>();
    }

    void Update()
    {
        //Debug.Log(NoloVR_Plugins.GetElectricity(3));
        try
        {
            UIText.text =
              "HMD POS       :" + NoloVR_Controller.GetDevice(NoloDeviceType.Hmd).GetPose().pos + "\n"
             //+ "HMD ROT       :" + NoloVR_Controller.GetDevice(NoloDeviceType.Hmd).GetPose().rot + "\n"
             //+ "HMD ROT       :" + NoloVR_Plugins.API.GetPoseByDeviceType(0).bDeviceIsConnected + NoloVR_Plugins.API.GetPoseByDeviceType(1).bDeviceIsConnected + NoloVR_Plugins.API.GetPoseByDeviceType(2).bDeviceIsConnected +NoloVR_Plugins.API.GetPoseByDeviceType(3).bDeviceIsConnected+"\n"
            + "HMD VEC     :" + NoloVR_Controller.GetDevice(NoloDeviceType.Hmd).GetPose().vecVelocity + "\n"
            + "HMD ANGULAR     :" + NoloVR_Controller.GetDevice(NoloDeviceType.Hmd).GetPose().vecAngularVelocity + "\n"
            + "LEFT AXIS     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetAxis(NoloTouchID.Trigger) + "RIGHT AXIS    :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis(NoloTouchID.Trigger) + "\n"

            + "LEFT POS      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().pos.x + "RIGHT POS     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().pos.x + "\n"
            + "LEFT ROT      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().rot+ "RIGHT ROT     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().rot + "\n"
            + "LEFT VEC      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().vecVelocity + "RIGHT VEC      :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().vecVelocity + "\n"
             + "LEFT ANGULAR      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().vecAngularVelocity + "RIGHT ANGULAR      :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().vecAngularVelocity + "\n"

            + "LEFT TRIGGER  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Trigger) + "RIGHT TRIGGER :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Trigger) + "\n"
            + "LEFT MENU     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Menu) + "RIGHT MENU    :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Menu) + "\n"
            + "LEFT TOUCHPAD :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPad) + "RIGHT TOUCHPAD:" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad) + "\n"
            + "LEFT SYSTEM   :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.System) + "RIGHT SYSTEM  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.System) + "\n"
            + "LEFT GRIP     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Grip) + "RIGHT GRIP    :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Grip) + "\n"
            + "LEFT up     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadUp) + "RIGHT up     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadUp) + "\n"
            + "LEFT down     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadDown) + "RIGHT down     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadDown) + "\n"
            + "LEFT left     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadLeft) + "RIGHT left     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadLeft) + "\n"
            + "LEFT right     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadRight) + "RIGHT right     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadRight) + "\n"
            + "LEFT ELE      :" + NoloVR_Plugins.GetElectricity(3) + "RIGHT ELE     :" + NoloVR_Plugins.GetElectricity(0) + "\n";
        }
        catch (System.Exception e)
        {
            Debug.Log("Catch"+e.Message);
            throw;
        }
        

    }
}
