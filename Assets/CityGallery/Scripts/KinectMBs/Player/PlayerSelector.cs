﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;
using SOVariables;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField]
    private BodyPlayersManager playerManager;
    //ScriptableObject To get KinectPlayer ref
    [SerializeField]
    private KinectPlayersList playersList;
    [SerializeField]
    private FloatReferenceArray bodyDistances;

    [SerializeField]
    private Kinect.JointType referenceJoint;

    [SerializeField]
    private Vector3 referencePoint;
    private Kinect.CameraSpacePoint refCamSpacePoint => new Kinect.CameraSpacePoint()
    {
        X = referencePoint.x,
        Y = referencePoint.y,
        Z = referencePoint.z
    };
        
    

    [SerializeField]
    private float selectionRadius;

    private Kinect.Vector4 floorPlane => BodySourceManager.FloorPlane;

    private void Awake()
    {
        bodyDistances.Clear();
    }

    private void FixedUpdate()
    {
        playersList.SelectPlayer(refCamSpacePoint, referenceJoint, floorPlane);

        if (bodyDistances != null)
        {
            SetBodyDistances();
        }

    }

    private void SetBodyDistances()
    {
        var refJoints = GetJointFromBodiesByType(referenceJoint);
        int trackedPlayers = refJoints.Count;
        float[] _bodyDistances = new float[trackedPlayers];

        for (int i = 0; i < trackedPlayers; i++)
        {
            _bodyDistances[i] = refJoints[i].Position.DistanceFromOrigin();
        }
        bodyDistances.SetArray(_bodyDistances);
    }

    private List<Kinect.Joint> GetJointFromBodiesByType(Kinect.JointType type)
    {
        List<Kinect.Joint> list = new List<Kinect.Joint>();
        foreach (var player in playersList.TrackedPlayers)
        {
            list.Add(player.BodyRaw.Joints[type]);
        }

        return list;
    }
    
}
