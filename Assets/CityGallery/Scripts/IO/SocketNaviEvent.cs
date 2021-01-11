using RoboRyanTron.Unite2017.Events;
using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketNaviEvent : MonoBehaviour
{
    [Header("Pre-set settings")]
    [SerializeField]
    private SocketIOComponent IoComponent;

    [Header("socket events")]
    public string naviEnterEvnt = "userEnter";
    public string naviLeaveEvnt = "userLeave";
    public string naviIdxEvnt = "navigationIndex";
    public string selectIdxEvnt = "navigationSelect";
    public string langIdxEvnt = "selectLang";
    [Header("deprecated socket events")]
    public string naviLeftEvnt = "navigationLeft";
    public string naviRightEvnt = "navigationRight";
    
    [Header("server socket events handlers")]
    [SerializeField]
    private GameIntEvent naviIdxSOEventServer;
    [SerializeField]
    private GameIntEvent selectIdxSOEventServer;

    private void Start()
    {
        IoComponent.On(naviIdxEvnt, OnNavigationIndex);
        IoComponent.On(selectIdxEvnt, OnSelectIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EmitEnter();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            EmitLeave();
        }
    }

    private void OnNavigationIndex(SocketIOEvent obj)
    {
        int idx = -1;
        obj.data.GetField(ref idx, "index");
        Debug.Log("[SocketIO]Received NavigationIndex:[" + idx + "]");

        naviIdxSOEventServer.Raise(idx);
    }

    private void OnSelectIndex(SocketIOEvent obj)
    {
        int idx = -1;
        obj.data.GetField(ref idx, "index");
        Debug.Log("[SocketIO]Received SelectIndex:[" + idx + "]");

        selectIdxSOEventServer.Raise(idx);
    }

    private JSONObject GetJsonArg(int sendInt)
    {
        return new JSONObject(sendInt);
    }

    public void EmitNaviLeft()
    {
        IoComponent.Emit(naviLeftEvnt);
    }

    public void EmitNaviRight()
    {
        IoComponent.Emit(naviRightEvnt);
    }

    public void EmitEnter()
    {
        IoComponent.Emit(naviEnterEvnt);
        Debug.Log("Enter Socket Test");
    }

    public void EmitLeave()
    {
        IoComponent.Emit(naviLeaveEvnt);
        Debug.Log("Leave Socket Test");
    }

    public void EmitNavigationIndex(int idx)
    {
        IoComponent.Emit(naviIdxEvnt, GetJsonArg(idx));
    }

    public void EmitSelectLang(int idx)
    {
        IoComponent.Emit(langIdxEvnt, GetJsonArg(idx));
    }
}
