using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game303FlipDotEventHandler : MonoBehaviour
{
    private Game303Mediator mediator => Game303Mediator.instance;
    private Game303Manager manager => Game303Manager.instance;

    //internal event
    [SerializeField]
    private GameIntEvent navigationIndexEvntIO;

    [Header("Debug")]
    [SerializeField]
    private int currentNaviIdx = 0;

    public void OnEnterRangeInternal()
    {
        if (manager.CheckStatus(Status.Idle))
        {
            mediator.ChangeStatus(Status.SelectLanguage);
            currentNaviIdx = 0;
        }
    }

    public void OnUserLeave()
    {
        mediator.ChangeStatus(Status.Idle);     
    }

    public void OnServerHome()
    {
        mediator.ChangeStatus(Status.SelectLanguage);
        currentNaviIdx = 0;
    }

    public void OnNavigationIndex(int idx)
    {
        if (!manager.CheckStatus(Status.Idle) && !manager.CheckStatus(Status.SelectLanguage))
        {
            mediator.ChangeStatus(Status.SelectYear);
            mediator.SelectYear(idx);
        }
    }

    public void OnSelectIndex(int idx) 
    {
        if (manager.CheckStatus(Status.SelectYear) || manager.CheckStatus(Status.Confirm))
        {
            mediator.SelectYear(idx);
            mediator.ChangeStatus(Status.Confirm);
        }
    }

    public void OnHandCursorLeft()
    {
        if (manager.CheckStatus(Status.Tutorial) || manager.CheckStatus(Status.Ready))
        {
            mediator.SelectLeft();
        }
        else if (manager.CheckStatus(Status.SelectYear))
        {
            currentNaviIdx = mediator.SelectYear(currentNaviIdx - 1);
            navigationIndexEvntIO.Raise(currentNaviIdx);
        }
    }

    public void OnHandCursorRight()
    {
        if (manager.CheckStatus(Status.Tutorial) || manager.CheckStatus(Status.Ready))
        {
            mediator.SelectRight();
        }
        else if (manager.CheckStatus(Status.SelectYear))
        {
            currentNaviIdx = mediator.SelectYear(currentNaviIdx + 1);
            navigationIndexEvntIO.Raise(currentNaviIdx);
        }
    }
}
