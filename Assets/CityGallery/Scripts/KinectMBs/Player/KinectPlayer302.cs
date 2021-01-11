using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class KinectPlayer302 : KinectPlayer
{
    private static List<Kinect.JointType> _JointTypesToSpawn = new List<Kinect.JointType>()
    {
        Kinect.JointType.HandLeft,
        Kinect.JointType.HandRight,
        Kinect.JointType.SpineMid
    };

    [SerializeField]
    private PlayerJoint302 _JointPrefab;

    private Dictionary<Kinect.JointType, PlayerJoint302> _JointObjects = new Dictionary<Kinect.JointType, PlayerJoint302>();

    private void OnEnable()
    {
        InstantiateJointObjects(_JointTypesToSpawn.ToArray());
    }

    private void OnDisable()
    {
        DestroyJointObjects();
    }

    private void Update()
    {
        UpdateJointObjects();
    }

    private void InstantiateJointObjects(Kinect.JointType[] jointTypes)
    {
        if (_JointPrefab != null)
        {
            foreach (var jointType in jointTypes)
            {
                GameObject newJoint = Instantiate(_JointPrefab.gameObject, transform);
                newJoint.name = jointType.ToString();
                _JointObjects.Add(jointType, newJoint.GetComponent<PlayerJoint302>());
            }
        }
    }

    private void DestroyJointObjects()
    {
        if (_JointPrefab != null)
        {
            foreach (var joint in _JointObjects)
            {
                Destroy(joint.Value);
            }
            _JointObjects.Clear();
        }
    }

    private void UpdateJointObjects()
    {
        if (bodyRaw != null)
        {
            foreach (var joint in _JointObjects)
            {
                var updatedPos = GetVector3FromJoint(bodyRaw.Joints[joint.Key]);
                joint.Value.transform.position = updatedPos;
            }
        }
    }

    public PlayerJoint302 GetLeftHand()
    {
        PlayerJoint302 hand;
        if (_JointObjects.TryGetValue(Kinect.JointType.HandLeft, out hand))
        {
            return hand;
        }
        return null;
    }

    public PlayerJoint302 GetRightHand()
    {
        PlayerJoint302 hand;
        if (_JointObjects.TryGetValue(Kinect.JointType.HandRight, out hand))
        {
            return hand;
        }
        return null;
    }
}
