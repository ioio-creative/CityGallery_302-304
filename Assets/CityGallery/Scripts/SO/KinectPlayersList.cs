using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Kinect = Windows.Kinect;

[CreateAssetMenu(menuName = "SO Kinect Data/KinectPlayersList")]
public class KinectPlayersList : ScriptableObject
{
    [SerializeField]
    private List<KinectPlayer302> players;
    public List<KinectPlayer302> TrackedPlayers => players;
    
    [SerializeField]
    private KinectPlayer302 selectedPlayer;
    public KinectPlayer302 SelectedPlayer => selectedPlayer;

    public void SetPlayersFromDictionary(Dictionary
        <ulong, KinectPlayer>.ValueCollection dictValues)
    {
        players = new List<KinectPlayer302>(dictValues.ToArray().Cast<KinectPlayer302>());
    }

    public KinectPlayer302 SelectPlayer(Kinect.CameraSpacePoint refPoint, Kinect.JointType refJointType, Kinect.Vector4 refFloor)
    {
        if (players.Count <=0 )
        {
            return null;
        }
        
        KinectPlayer302 selection = null;
        float distComparer = 10000f;
        foreach (var player in TrackedPlayers)
        {
            //var projJoint = ProjectedPointOntoPlane(player.BodyRaw.Joints[refJointType].Position,refFloor);
            //var projRef = ProjectedPointOntoPlane(refPoint, refFloor);
            //float distFromRefCSP = CameraSpacePointDistance(projJoint, projRef);
            if (player.BodyRaw != null)
            {
                float distFromRefCSP = KinectMath.CameraSpacePointDistance(player.BodyRaw.Joints[refJointType].Position, refPoint);
                if (distFromRefCSP < distComparer)
                {
                    distComparer = distFromRefCSP;
                    selection = player;
                } 
            }
        }
        selectedPlayer = selection;
        return selectedPlayer;
    }

}
