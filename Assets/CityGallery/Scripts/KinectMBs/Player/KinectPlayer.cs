using UnityEngine;
using Kinect = Windows.Kinect;

public class KinectPlayer : MonoBehaviour
{
    
    #region KinectBodyInfo
    protected Kinect.Body bodyRaw;
    public Kinect.Body BodyRaw => bodyRaw;

    protected ulong bodyID => bodyRaw.TrackingId;
    #endregion

    public void SetPlayerBody(Kinect.Body body)
    {
        bodyRaw = body;
    }

    protected static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
