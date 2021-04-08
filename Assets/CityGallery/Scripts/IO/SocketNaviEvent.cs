using RoboRyanTron.Unite2017.Events;
using SocketIO;
using SocketIO.Custom;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketNaviEvent : MonoBehaviour
{
    [Header("Pre-set settings")]
    [SerializeField]
    private SocketIOControl IoComponent;
    [SerializeField]
    private bool loadSocketUrlFromConfig;

    [Header("socket events")]
    public string naviEnterEvnt = "userEnter";
    public string naviLeaveEvnt = "userLeave";
    public string naviIdxEvnt = "navigationIndex";
    public string selectIdxEvnt = "navigationSelect";
    public string langIdxEvnt = "selectLang";
    public string homeEvnt = "backToHome";

    public string touchEvnt = "onTouchStart";

    [Header("deprecated socket events")]
    public string naviLeftEvnt = "navigationLeft";
    public string naviRightEvnt = "navigationRight";
    
    [Header("server socket events handlers")]
    [SerializeField]
    private GameIntEvent naviIdxSOEventServer;
    [SerializeField]
    private GameIntEvent selectIdxSOEventServer;
    [SerializeField]
    private GameEvent onUserLeaveSOEventServer;

    [SerializeField]
    private GameEvent onTouchDownSOEventServer;

    private IEnumerator Start()
    {
        if (IoComponent != null)
        {
            if (IoComponent.IsConnected)
            {
                IoComponent.Close();
                yield return new WaitUntil(() => !IoComponent.IsConnected);
            }

            if (loadSocketUrlFromConfig)
            {
                
                IoComponent.url = SystemConfig.Instance.Config.socketUrl;
                
            }

            IoComponent.On("connect", OnConnected);
            IoComponent.On("disconnect", OnDisconnected);
            IoComponent.On(naviIdxEvnt, OnNavigationIndex);
            IoComponent.On(selectIdxEvnt, OnSelectIndex);
            IoComponent.On(homeEvnt, OnBackToHome);
            IoComponent.On(touchEvnt, OnTouchDown);

            IoComponent.Connect();
        }
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EmitEnter();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EmitLeave();
        }
    }

    private void OnConnected(SocketIOEvent obj)
    {
        Debug.Log(name + ": OnConnceted");
    }

    private void OnDisconnected(SocketIOEvent obj)
    {
        Debug.Log(name + ": OnDisconnected");
    }

    private void OnNavigationIndex(SocketIOEvent obj)
    {
        int idx = 0;
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

    private void OnBackToHome(SocketIOEvent obj)
    {
        Debug.Log("[SocketIO]Received BackToHome");
        onUserLeaveSOEventServer.Raise();
    }

    private void OnTouchDown(SocketIOEvent obj)
    {
        Debug.Log("[SocketIO]Received Touch");
        onTouchDownSOEventServer.Raise();
    }

    private JSONObject GetJsonArg(string fieldName, int sendInt)
    {
        var sendDic = new Dictionary<string, JSONObject>();
        sendDic.Add(fieldName, new JSONObject(sendInt));
        return new JSONObject(sendDic);
    }

    //public void EmitNaviLeft()
    //{
    //    IoComponent.Emit(naviLeftEvnt);
    //}

    //public void EmitNaviRight()
    //{
    //    IoComponent.Emit(naviRightEvnt);
    //}

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
        var packet = GetJsonArg("index", idx);
        IoComponent.Emit(naviIdxEvnt, packet);
        Debug.Log("[SocketIO]Sent Nav Idx Packet: " + packet.ToString());
    }

    public void EmitSelectLang(int idx)
    {
        var packet = GetJsonArg("index", idx);
        IoComponent.Emit(langIdxEvnt, packet);
        Debug.Log("[SocketIO]Sent Lang Idx Packet: " + packet.ToString());
    }
}    
