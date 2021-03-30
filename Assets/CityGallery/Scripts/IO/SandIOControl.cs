using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.IO.Ports;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using SOVariables;
using RoboRyanTron.Unite2017.Events;

public class SandIOControl : MonoBehaviour
{
    public static SandIOControl Instance => instance;
    private static SandIOControl instance;
    public enum SandTransition
    {
        IDLE,
        //on select language enter
        ACTIVATE,
        //on select language
        L0,
        //on left hand raised
        L1,
        //on right hand raised
        R1,
        //on confirm
        C1,
        //on skip
        SKIP,
        //on restart
        RESTART,
        //on select year
        SELECT_YEAR,
        //on year entered
        YEAR_ENTER
    }

    [SerializeField]
    private string settingsFileName = "SandDropSettings.json";
    [SerializeField]
    private SandDropConfig sandDropConfig;
    [SerializeField]
    private FloatVariable userEnterDelay;
    [SerializeField]
    private FloatVariable selectYearDelay;
    [SerializeField]
    private FloatVariable yearEnteredDelay;
    [SerializeField]
    private GameEvent onPlayerEnterEvnt;

    [SerializeField]
    private string message;

    private UdpClient udpClient;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        LoadSettingsFile();

        udpClient = new UdpClient(sandDropConfig.address.ip, sandDropConfig.address.port);
        Debug.Log("Sand UDP Connected:[" + sandDropConfig.address.ip + ":" + sandDropConfig.address.port + "]");
    }

    private void OnDisable()
    {
        if (udpClient != null)
        {
            udpClient.Close();
            udpClient.Dispose();
        }
    }
    private void LoadSettingsFile()
    {
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, settingsFileName);
        string jsonString = File.ReadAllText(jsonFilePath);
        sandDropConfig = JsonUtility.FromJson<SandDropConfig>(jsonString);

        userEnterDelay.InitializeValue(sandDropConfig.userEnterDelay);
        selectYearDelay.InitializeValue(sandDropConfig.selectYearDelay);
        yearEnteredDelay.InitializeValue(sandDropConfig.yearEnteredDelay);

        if (!string.IsNullOrEmpty(sandDropConfig.sandCmd))
        {
            message = sandDropConfig.sandCmd;
        }
    }

    private void SendCommandToUdp(string cmd)
    {
        if (udpClient != null)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(cmd);
            udpClient.Send(sendBuffer, sendBuffer.Length);
            Debug.Log("SandDrop Command Sent: \"" + cmd + "\"");
        }
    }

    private IEnumerator DelayedPlayerEnter(float delay)
    {
        yield return new WaitForSeconds(delay);
        onPlayerEnterEvnt.Raise();
    }

    private IEnumerator DropSandDelayed(float delay1, float delay2 = 0)
    {
        yield return new WaitForSeconds(delay1);
        DropSandIO(SandTransition.SELECT_YEAR);
        yield return new WaitForSeconds(delay2);
        DropSandIO(SandTransition.YEAR_ENTER);
    }

    private void DropSandIO(SandTransition transition)
    {
        switch (transition)
        {
            case SandTransition.IDLE:
            default:
                SendCommandToUdp(sandDropConfig.idleCmd);
                break;
            case SandTransition.ACTIVATE:
                SendCommandToUdp(sandDropConfig.activeCmd);
                break;
            case SandTransition.L0:
                SendCommandToUdp(sandDropConfig.leftReadyCmd);
                break;
            case SandTransition.L1:
                SendCommandToUdp(sandDropConfig.leftDoneCmd);
                break;
            case SandTransition.R1:
                SendCommandToUdp(sandDropConfig.rightDoneCmd);
                break;
            case SandTransition.C1:
                SendCommandToUdp(sandDropConfig.confirmDoneCmd);
                break;
            case SandTransition.SKIP:
                SendCommandToUdp(sandDropConfig.skipCmd);
                break;
            case SandTransition.RESTART:
                SendCommandToUdp(sandDropConfig.restartCmd);
                break;
            case SandTransition.SELECT_YEAR:
                SendCommandToUdp(sandDropConfig.sandCmd);
                break;
            case SandTransition.YEAR_ENTER:
                SendCommandToUdp(sandDropConfig.slideCmd);
                break;
        }
    }

    public static void SendSandIO(SandTransition transition)
    {
        if (instance != null)
        {
            instance.DropSandIO(transition);
        }
    }

    public void OnTutorialConfirmed()
    {
        StartCoroutine(DelayedPlayerEnter(userEnterDelay));
    }

    public void OnSelectYearConfirmed()
    {
        StartCoroutine(DropSandDelayed(selectYearDelay, yearEnteredDelay));
    }

}

[Serializable]
public struct SandDropConfig
{
    public IPAddressConfig address;
    public float userEnterDelay;
    public float selectYearDelay;
    public float yearEnteredDelay;

    public string idleCmd;
    public string activeCmd;
    public string leftReadyCmd;
    public string leftDoneCmd;
    public string rightDoneCmd;
    public string confirmDoneCmd;
    public string skipCmd;
    public string restartCmd;
    public string sandCmd;
    public string slideCmd;
}
