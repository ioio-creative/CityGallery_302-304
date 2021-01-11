using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class PlayerJoint : MonoBehaviour
{
    private Kinect.JointType type;
    public Kinect.JointType Type => type;

    public Kinect.Joint jointRaw;

}
