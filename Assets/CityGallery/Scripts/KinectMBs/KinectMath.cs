using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public static class KinectMath
{
    public static float PointDistFromPlane(Kinect.CameraSpacePoint point, Kinect.Vector4 plane)
    {
        //Hessian normal form
        // D = normal (dot) point + 

        Vector3 normalV3 = new Vector3(plane.X, plane.Y, plane.Z);
        Vector3 pointV3 = new Vector3(point.X, point.Y, point.Z);
        return (Vector3.Dot(normalV3, pointV3) + plane.W) / (normalV3.magnitude);
    }

    public static Kinect.CameraSpacePoint ProjectedPointOntoPlane(Kinect.CameraSpacePoint point, Kinect.Vector4 plane)
    {
        Vector3 normalV3 = new Vector3(plane.X, plane.Y, plane.Z);
        Vector3 pointV3 = new Vector3(point.X, point.Y, point.Z);
        Vector3 projectedPointV3 = pointV3 - (Vector3.Dot(normalV3, pointV3) + plane.W) * normalV3;

        return new Kinect.CameraSpacePoint()
        {
            X = projectedPointV3.x,
            Y = projectedPointV3.y,
            Z = projectedPointV3.z
        };
    }

    public static float CameraSpacePointDistance(Kinect.CameraSpacePoint ptA, Kinect.CameraSpacePoint ptB)
    {
        return Vector3.Distance(new Vector3(ptA.X, ptA.Y, ptA.Z), new Vector3(ptB.X, ptB.Y, ptB.Z));
    }

    #region Camera Space Point Extensions
    public static float CameraSpacePointMagnitudeSqrd(this Kinect.CameraSpacePoint point)
    {
        return new Vector3(point.X, point.Y, point.Z).sqrMagnitude;
    }

    public static float DistanceFromOrigin(this Kinect.CameraSpacePoint point)
    {
        return Mathf.Sqrt(CameraSpacePointMagnitudeSqrd(point));
    }
    #endregion
}
