using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;

public class HeartBeatController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Text asset containing haptic timestamps")]
    private TextAsset m_TimestampsFile;

    [Header("Heartbeat Settings")]
    [SerializeField]
    [Tooltip("Duration of the first haptic pulse in seconds")]
    private float m_FirstPulseDuration = 0.1f;

    [SerializeField]
    [Tooltip("Intensity of the first haptic pulse (0-1)")]
    private float m_FirstPulseIntensity = 1.0f;

    [SerializeField]
    [Tooltip("Delay between first and second pulse in seconds")]
    private float m_PulseDelay = 0.1f;

    [SerializeField]
    [Tooltip("Duration of the second haptic pulse in seconds")]
    private float m_SecondPulseDuration = 0.08f;

    [SerializeField]
    [Tooltip("Intensity of the second haptic pulse (0-1)")]
    private float m_SecondPulseIntensity = 0.6f;

    [SerializeField]
    [Tooltip("Left controller node")]
    private XRNode m_LeftControllerNode = XRNode.LeftHand;

    [SerializeField]
    [Tooltip("Right controller node")]
    private XRNode m_RightControllerNode = XRNode.RightHand;

    private List<float> m_HeartbeatTimestamps = new List<float>();
    private int m_CurrentHeartbeatIndex = 0;
    private Coroutine m_HeartbeatCoroutine;
    private bool m_IsPlaying = false;

    void Start()
    {
        if (m_TimestampsFile != null)
        {
            LoadTimestamps();
        }
    }

    public void StartHeartbeat()
    {
        if (m_IsPlaying)
        {
            return;
        }

        if (m_TimestampsFile == null)
        {
            Debug.LogWarning("HeartBeatController: No timestamps file assigned");
            return;
        }

        m_IsPlaying = true;
        m_CurrentHeartbeatIndex = 0;
        m_HeartbeatCoroutine = StartCoroutine(PlayHeartbeatSequence());
    }

    public void StopHeartbeat()
    {
        if (m_HeartbeatCoroutine != null)
        {
            StopCoroutine(m_HeartbeatCoroutine);
            m_HeartbeatCoroutine = null;
        }
        m_IsPlaying = false;
        m_CurrentHeartbeatIndex = 0;
    }

    private IEnumerator PlayHeartbeatSequence()
    {
        while (m_IsPlaying && m_CurrentHeartbeatIndex < m_HeartbeatTimestamps.Count)
        {
            float nextBeatTime = m_HeartbeatTimestamps[m_CurrentHeartbeatIndex];
            float waitTime = nextBeatTime - (m_CurrentHeartbeatIndex == 0 ? 0 : m_HeartbeatTimestamps[m_CurrentHeartbeatIndex - 1]);

            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);
            }

            PlayHeartbeatPulse();
            m_CurrentHeartbeatIndex++;
        }

        m_IsPlaying = false;
    }

    private void PlayHeartbeatPulse()
    {
        SendHapticToNodes(m_FirstPulseIntensity, m_FirstPulseDuration);
        StartCoroutine(PlaySecondPulse());
    }

    private IEnumerator PlaySecondPulse()
    {
        yield return new WaitForSeconds(m_PulseDelay);
        SendHapticToNodes(m_SecondPulseIntensity, m_SecondPulseDuration);
    }

    private void SendHapticToNodes(float intensity, float duration)
    {
        SendHapticToNode(m_LeftControllerNode, intensity, duration);
        SendHapticToNode(m_RightControllerNode, intensity, duration);
    }

    private void SendHapticToNode(XRNode node, float intensity, float duration)
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(node, inputDevices);

        foreach (var device in inputDevices)
        {
            if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
            {
                device.SendHapticImpulse(0, intensity, duration);
            }
        }
    }

    private void LoadTimestamps()
    {
        m_HeartbeatTimestamps.Clear();

        if (m_TimestampsFile == null)
        {
            Debug.LogWarning("HeartBeatController: Timestamps file is null");
            return;
        }

        string[] lines = m_TimestampsFile.text.Split('\n');
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (string.IsNullOrEmpty(trimmedLine))
                continue;

            // Skip header lines (like "h" for heartbeat)
            if (trimmedLine.Length == 1 && char.IsLetter(trimmedLine[0]))
                continue;

            if (float.TryParse(trimmedLine, out float timestamp))
            {
                m_HeartbeatTimestamps.Add(timestamp);
            }
        }

        Debug.Log($"HeartBeatController: Loaded {m_HeartbeatTimestamps.Count} heartbeat timestamps");
    }

    public void SetTimestampsFile(TextAsset newTimestampsFile)
    {
        m_TimestampsFile = newTimestampsFile;
        StopHeartbeat();
        LoadTimestamps();
    }
}
