using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadRotation : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        XRDevice.DisableAutoXRCameraTracking(Camera.main, true);
    }
}
