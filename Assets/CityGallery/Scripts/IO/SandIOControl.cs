using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.IO.Ports;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class SandIOControl : MonoBehaviour
{
    [SerializeField]
    private string settingsFileName = "SandDropSettings.json";
    [SerializeField]
    private IPAddressConfig sandDropIp;

    [SerializeField]
    private string message;

    private UdpClient udpClient;

    private void OnEnable()
    {
        LoadSettingsFile();

        udpClient = new UdpClient(sandDropIp.ip, sandDropIp.port);
        Debug.Log("Sand UDP Connected:[" + sandDropIp.ip + ":" + sandDropIp.port + "]");
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
        sandDropIp = JsonUtility.FromJson<IPAddressConfig>(jsonString);
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

    public void DropSandIO()
    {
        SendCommandToUdp(message);
    }
}
