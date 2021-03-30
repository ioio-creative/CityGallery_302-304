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


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (playerDetectList.TrackedPlayers.Count == 0 && Game303Manager.instance.CheckStatus(Status.Idle))
        {
            maskAnim.SetBool("pulse", true);            
        }
        else
        {
            maskAnim.SetBool("pulse", false);
        }
    }

    public void SetBuildingSprite(Sprite sprite)
    {
        building.sprite = sprite;
    }

    public void ShowIdelBuilding()
    {
        building.color = Color.white;
    }

    public void HideIdleBuilding()
    {
        building.color = Color.clear;
    }
}
