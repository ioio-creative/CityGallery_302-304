using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodyPlayersManager : MonoBehaviour
{
    [SerializeField]
    private BodySourceManager _BodyManager;
    [SerializeField]
    private Camera _KinectDebugCam;

    [SerializeField]
    private KinectPlayer _BodyPrefab;

    private Dictionary<ulong, KinectPlayer> _Bodies = new Dictionary<ulong, KinectPlayer>();

    //ScriptableObject To store KinectPlayer ref
    [SerializeField]
    private KinectPlayersList playersList;

    private void Start()
    {
        _KinectDebugCam.enabled = SystemConfig.Instance.Config.debug;
    }

    private void Update()
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
        #endregion

        
        #region Create New or Update Kinect Bodies
        //filetered IDs will be gradually removed preexisting tracked bodies, left with untracked old bodies
        HashSet<ulong> filteredIds = new HashSet<ulong>(_Bodies.Keys);
        foreach (var updateBody in data)
        {
            if (updateBody == null)
            {
                continue;
            }
            
            if (updateBody.IsTracked)
            {
                //Check preexisting body
                KinectPlayer bodyPlayer;
                if (_Bodies.TryGetValue(updateBody.TrackingId, out bodyPlayer))
                {
                    //update body data
                    bodyPlayer.SetPlayerBody(updateBody);
                    //filter out trackedId
                    filteredIds.Remove(updateBody.TrackingId);
                }
                else
                {
                    //spawn and add new KinectPlayer to Dictionary
                    
                    bodyPlayer = SpawnBodyObject(updateBody);
                    _Bodies.Add(updateBody.TrackingId, bodyPlayer);
                }
            }
                
        }
        #endregion

        #region Delete Lost Tracked Kinect Bodies
        foreach (ulong id in filteredIds)
        {
            KinectPlayer lostBody;
            if (_Bodies.TryGetValue(id, out lostBody))
            {
                _Bodies.Remove(id);

                if (lostBody != null)
                {
                    Destroy(lostBody.gameObject);
                }
            }
        }
        #endregion

        if (playersList != null)
        {
            playersList.SetPlayersFromDictionary(_Bodies.Values); 
        }
    }

    private KinectPlayer SpawnBodyObject(Kinect.Body body)
    {
        GameObject newPlayerObject = Instantiate(_BodyPrefab.gameObject);
        var kp = newPlayerObject.GetComponent<KinectPlayer>();
        kp.SetPlayerBody(body);
        return kp;
    }

}
