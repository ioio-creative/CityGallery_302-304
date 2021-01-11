using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303FlipDotEventHandler : MonoBehaviour
{
    private Game303Mediator mediator => Game303Mediator.instance;

    public void OnUserEnter()
    {
        mediator.ChangeStatus(Status.Tutorial);
    }

    public void OnUserLeave()
    {
        mediator.ChangeStatus(Status.Idle);
    }

    public void OnNavigationIndex(int idx)
    {
        mediator.ChangeStatus(Status.SelectYear);
        mediator.SelectYear(idx);
    }

    public void OnSelectIndex(int idx) 
    {
        mediator.SelectYear(idx);
        mediator.ChangeStatus(Status.Confirm);
    }

    public void OnTutorialLeft()
    {
        mediator.SelectLeft();
    }

    public void OnTutorialRight()
    {
        mediator.SelectRight();
    }
}
