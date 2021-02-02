using UnityEngine;
using Kinect = Windows.Kinect;

public class HandCursorBase : MonoBehaviour
{
    public static float CursorDepth = 0;

    [SerializeField]
    protected bool LeftHand;

    [SerializeField]
    protected KinectPlayersList players;
    
    [SerializeField]
    protected Transform JointTransform;
    protected Vector2 velocity = Vector2.zero;

    public Kinect.HandState handState;

    protected void UpdateCursorPosition(float depth)
    {
        if (JointTransform != null)
        {
            Vector2 targetPos = JointTransform.position;
            Vector2 smoothedPos = Vector2.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);

            Vector3 assignedPos = smoothedPos;
            assignedPos.z = depth;

            transform.position = assignedPos;
        }
    }

    public virtual bool IsHandCursorRaised()
    {
        if (!players.HaveInRangePlayers) return false;
        var selectedPlayer = players.SelectedPlayer;
        var selectedPlayerJoints = selectedPlayer.BodyRaw.Joints;
        var handJointToCheck = LeftHand ? selectedPlayerJoints[Kinect.JointType.HandLeft] : selectedPlayerJoints[Kinect.JointType.HandRight];
        var handStateToCheck = LeftHand ? selectedPlayer.BodyRaw.HandLeftState : selectedPlayer.BodyRaw.HandRightState;
        if (handStateToCheck == Kinect.HandState.NotTracked || handStateToCheck == Kinect.HandState.Unknown) return false;
        if (handJointToCheck.TrackingState != Kinect.TrackingState.Tracked) return false;
        return handJointToCheck.Position.Y >= selectedPlayerJoints[Kinect.JointType.SpineShoulder].Position.Y;
    }
}
