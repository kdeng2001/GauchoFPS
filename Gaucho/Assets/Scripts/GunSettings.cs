using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "GunAttribute/GunSettings")]
public class GunSettings : ScriptableObject
{
    public float zoomSpeed = 5;
    public float normalFOV = 60;
    public float zoomedFOV = 45;
    public float minNormalAmount = 0;
    public float maxZoomAmount = 1;
    public float zoomMouseSensitivity = .75f;
    public Vector3 localZoomDisplacement = new Vector3((float)-0.004,(float) -1.6,(float) .138);

    public float damage = 20;
    public int ammoCapacity = 200;
}
