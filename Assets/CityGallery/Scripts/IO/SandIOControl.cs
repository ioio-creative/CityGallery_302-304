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
    public enum SandTransition
    {
        USER_ENTER,
        SELECT_YEAR
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
    private GameEvent onPlayerEnterEvnt;

    [SerializeField]
    private string message;

    private UdpClient udpClient;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            DropSandIO();
        }
    }

    private void LoadSettingsFile()
    {
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, settingsFileName);
        string jsonString = File.ReadAllText(jsonFilePath);
        sandDropConfig = JsonUtility.FromJson<SandDropConfig>(jsonString);

        userEnterDelay.InitializeValue(sandDropConfig.userEnterDelay);
        selectYearDelay.InitializeValue(sandDropConfig.selectYearDelay);
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

    private IEnumerator DropSandDelayed(float delay, Action callback = null)
    {
        yield return new WaitForSeconds(delay);

        SendCommandToUdp(message);
        if (callback != null)
        {
            callback.Invoke();
        }
    }

    public void DropSandIO(int transitionIdx)
    {
        SandTransition transition = (SandTransition)transitionIdx;
        switch (transition)
        {
            case SandTransition.USER_ENTER:
                StartCoroutine(DropSandDelayed(userEnterDelay, () =>
                {
                    onPlayerEnterEvnt.Raise();
                }));
                break;
            case SandTransition.SELECT_YEAR:
                StartCoroutine(DropSandDelayed(selectYearDelay));
                break;
            default:
                break;
        }
    }

    public void DropSandIO()
    {
        SendCommandToUdp(message);
    }
}

[Serializable]
public struct SandDropConfig
{
    public IPAddressConfig address;
    public float userEnterDelay;
    public float selectYearDelay;
    public string sandCmd;
}
