using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class HandCursorView302 : HandCursorBase
{
    public bool CursorOn;

    private KinectPlayer302 selectedPlayer => base.players.SelectedPlayer as KinectPlayer302;

    private void Update()
    {

        if (selectedPlayer == null)
        {
            CursorOn = false;
        }
        else
        {
            CursorOn = true;
            PlayerJoint302 hand = LeftHand ? selectedPlayer.GetLeftHand() : selectedPlayer.GetRightHand();
            if (hand != null)
            {
                base.JointTransform = hand.transform;
                base.UpdateCursorPosition(CursorDepth); 
            }
        }       
    }

    private void LateUpdate()
    {
        if (selectedPlayer != null)
        {
            handState = LeftHand ? selectedPlayer.BodyRaw.HandLeftState : selectedPlayer.BodyRaw.HandRightState;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            if (!CursorOn) sr.color = Color.clear;
            else
            {
                switch (handState)
                {
                    case Kinect.HandState.Open:
                        sr.color = Color.green;
                        break;
                    case Kinect.HandState.Closed:
                        sr.color = Color.red;
                        break;
                    case Kinect.HandState.Lasso:
                        sr.color = Color.blue;
                        break;
                    default:
                        break;
                } 
            }
        }
    }
}
