using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoloVR_AppInfo : MonoBehaviour {
    public string appKey;
    public NoloAndroidVRPlayform VRPlayform;
    void Start() {
        NoloVR_Playform.GetInstance().Authentication(appKey);
        NoloVR_Playform.GetInstance().SetHmdTrackingCenter((int)VRPlayform);
    }
}
