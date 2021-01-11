using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
    //public Material BoneMaterial;

    [SerializeField]
    private GameObject _JointObject;
    [SerializeField]
    private BodySourceManager _BodyManager;
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    
    //private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    //{
    //    { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
    //    { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
    //    { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
    //    { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
    //    { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
    //    { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
    //    { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
    //    { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
    //    { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
    //    { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
    //    { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
    //    { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
    //    { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
    //    { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
    //    { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
    //    { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
    //    { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
    //    { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
    //    { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
    //    { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
    //    { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
    //    { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
    //    { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
    //    { Kinect.JointType.Neck, Kinect.JointType.Head },
    //};

    private static List<Kinect.JointType> _JointTypesToVisualize = new List<Kinect.JointType>() 
    { 
        Kinect.JointType.HandLeft,
        Kinect.JointType.HandRight
    };

    void Update () 
    {
        if (_BodyManager == null)
        {
            return;
        }

        #region Get Kinect Data
        Kinect.Body[] data = _BodyManager.BodiesRaw;
        if (data == null)
        {
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }
        #endregion

        #region Delete Lost Tracked Kinect Bodies
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        // First delete untracked bodies
        foreach (ulong knownId in knownIds)
        {
            if (!trackedIds.Contains(knownId))
            {
                Destroy(_Bodies[knownId]);
                _Bodies.Remove(knownId);
            }
        }
        #endregion

        #region Create Kinect Bodies
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                //Create Body for new IDs
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                //Update Body positions
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
        #endregion
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);

        //for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        //{
        //    GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //    LineRenderer lr = jointObj.AddComponent<LineRenderer>();
        //    //lr.SetVertexCount(2);//[deprecated]
        //    lr.positionCount = 2;
        //    lr.material = BoneMaterial;
        //    //lr.SetWidth(0.05f, 0.05f);//[deprecated]
        //    lr.startWidth = 0.05f;
        //    lr.endWidth = 0.005f;

        //    jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //    jointObj.name = jt.ToString();
        //    jointObj.transform.parent = body.transform;
        //}

        foreach (Kinect.JointType joint in _JointTypesToVisualize)
        {           
            if (_JointObject != null)
            {
                GameObject newJoint = Instantiate(_JointObject);
                newJoint.name = joint.ToString();
                newJoint.transform.parent = body.transform;              
            }
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        //for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        //{
        //    Kinect.Joint sourceJoint = body.Joints[jt];
        //    Kinect.Joint? targetJoint = null;

        //    if(_BoneMap.ContainsKey(jt))
        //    {
        //        targetJoint = body.Joints[_BoneMap[jt]];
        //    }

        //    Transform jointObj = bodyObject.transform.Find(jt.ToString());
        //    jointObj.localPosition = GetVector3FromJoint(sourceJoint);

        //    LineRenderer lr = jointObj.GetComponent<LineRenderer>();
        //    if(targetJoint.HasValue)
        //    {
        //        lr.SetPosition(0, jointObj.localPosition);
        //        lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
        //        //lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));//[deprecated]
        //        lr.startColor = GetColorForState(sourceJoint.TrackingState);
        //        lr.endColor = GetColorForState(targetJoint.Value.TrackingState);
        //    }
        //    else
        //    {
        //        lr.enabled = false;
        //    }
        //}

        Kinect.HandState LHS = body.HandLeftState;
        Kinect.HandState RHS = body.HandRightState;

        foreach (Kinect.JointType joint in _JointTypesToVisualize)
        {
            Kinect.Joint srcJoint = body.Joints[joint];
            Vector3 targetPosition = GetVector3FromJoint(srcJoint);
            targetPosition.z = 0;

            Transform jointObject = bodyObject.transform.Find(joint.ToString());
            jointObject.position = targetPosition;

            //if (joint == Kinect.JointType.HandLeft)
            //{
            //    jointObject.GetComponent<HandJointManager>().SetCursorHandState(LHS);
            //}
            //else if (joint == Kinect.JointType.HandRight)
            //{
            //    jointObject.GetComponent<HandJointManager>().SetCursorHandState(RHS);
            //}
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
}
