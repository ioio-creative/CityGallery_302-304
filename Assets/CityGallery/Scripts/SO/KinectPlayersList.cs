using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Kinect = Windows.Kinect;

[CreateAssetMenu(menuName = "SO Kinect Data/KinectPlayersList")]
public class KinectPlayersList : ScriptableObject
{
    [SerializeField]
    private List<KinectPlayer> players;
    public List<KinectPlayer> TrackedPlayers => players;
    
    [SerializeField]
    private KinectPlayer selectedPlayer;
    public KinectPlayer SelectedPlayer => selectedPlayer;

    public void SetPlayersFromDictionary(Dictionary
        <ulong, KinectPlayer>.ValueCollection dictValues)
    {
        players = new List<KinectPlayer>(dictValues.ToArray().Cast<KinectPlayer>());
    }

    public KinectPlayer SelectPlayer(Kinect.CameraSpacePoint refPoint, Kinect.JointType refJointType, Kinect.Vector4 refFloor)
    {
        if (players.Count <=0 )
        {
            return null;
        }
        
        KinectPlayer selection = null;
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
