using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Zoom : MonoBehaviour
{
    public Volume zoomed;
    public Volume normal;
    public GunSettings g;
    public Transform gun;
    public MouseLook mouseLook;

    private Camera cam;
    private Vector3 gunDefaultPosition;
    private float defaultSensitivity;
    public float zoomSensitivity;
    float lerpAmount = 0;
    void Start()
    {
        //cam = GetComponent<Camera>();
        gunDefaultPosition = gun.localPosition;
        defaultSensitivity = mouseLook.mouseSensitivity;
        zoomSensitivity = defaultSensitivity * g.zoomMouseSensitivity;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            lerpAmount = Mathf.Min(lerpAmount + g.zoomSpeed * Time.deltaTime, 1f);
            gun.localPosition = g.localZoomDisplacement;
            mouseLook.mouseSensitivity = zoomSensitivity;
        }
        else
        {
            lerpAmount = Mathf.Max(lerpAmount - g.zoomSpeed * Time.deltaTime, 0f);
            gun.localPosition = gunDefaultPosition;
            mouseLook.mouseSensitivity = defaultSensitivity;
        }
        normal.weight = Mathf.Clamp(1 - lerpAmount, g.minNormalAmount, 1f);
        zoomed.weight = Mathf.Clamp(lerpAmount, 0f, g.maxZoomAmount);
        //cam.fieldOfView = Mathf.Lerp(g.normalFOV, g.zoomedFOV, lerpAmount);




        //gun.localPosition = gun.localPosition - g.localZoomDisplacement;

    }
}

//Vector3(0, 1.45, -0.238) Vector3(-0.004, -0.15, -0.1)
