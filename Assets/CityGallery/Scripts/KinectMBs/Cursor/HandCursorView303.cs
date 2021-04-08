using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class HandCursorView303 : HandCursorBase
{
    public static new float CursorDepth = 0;

    public bool CursorOn;

    private KinectPlayer303 selectedPlayer => base.players.SelectedPlayer as KinectPlayer303;

    private void Update()
    {
        if (selectedPlayer == null)
        {
            CursorOn = false;
        }
        else
        {
            CursorOn = true;
            PlayerJoint303 hand = LeftHand ? selectedPlayer.GetLeftHand() : selectedPlayer.GetRightHand();
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
