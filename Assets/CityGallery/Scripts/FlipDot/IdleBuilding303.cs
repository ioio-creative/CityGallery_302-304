using SOVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBuilding303 : MonoBehaviour
{
    public static IdleBuilding303 instance;
    
    [SerializeField]
    private SpriteRenderer building;
    [SerializeField]
    private SpriteMask mask;
    [SerializeField]
    private Animator maskAnim;

    [SerializeField]
    private KinectPlayersList playerDetectList;
    [SerializeField]
    private FloatVariable closestBodyPosition;
    [SerializeField]
    private FloatVariable playerInRangeMax;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Game303Manager.instance.CheckStatus(Status.Idle))
        {
            maskAnim.SetBool("pulse", ShouldActivatePulsePredicate());   
        }
        else
        {
            maskAnim.SetBool("pulse", false);
        }
    }

    private bool ShouldActivatePulsePredicate()
    {
        if (!Game303Manager.instance.CheckStatus(Status.Idle)) return false;
    
        if (playerDetectList.TrackedPlayers == null || playerDetectList.TrackedPlayers.Count == 0) return true;

        return closestBodyPosition > playerInRangeMax;     
    }

    public void SetBuildingSprite(Sprite sprite)
    {
        building.sprite = sprite;
    }

    public void ShowIdleBuilding()
    {
        building.color = Color.white;
    }

    public void HideIdleBuilding()
    {
        building.color = Color.clear;
    }


}
