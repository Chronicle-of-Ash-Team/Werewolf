using Agora.Rtc;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class AgoraManager : MonoBehaviour
{
    public static AgoraManager Instance { get; private set; }

    private const string APP_ID = "6022e0e1f8034b729273203ca1b80bd8";

    private IRtcEngine rtcEngine;

    private void Awake()
    {
        Instance = this;
        InitRtcEngine();
    }

    void Start()
    {
        //JoinChannel(LobbyManager.Instance.GetLobbyName() + "_open");
    }

    void OnDestroy()
    {
        if (rtcEngine != null)
        {
            rtcEngine.LeaveChannel();
            rtcEngine.Dispose();
        }
    }

    public void InitRtcEngine()
    {
        rtcEngine = RtcEngine.CreateAgoraRtcEngine();

        RtcEngineContext context = new RtcEngineContext();
        context.appId = APP_ID;
        context.channelProfile = CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION;
        context.audioScenario = AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT;
        context.areaCode = AREA_CODE.AREA_CODE_CN;

        rtcEngine.Initialize(context);
        rtcEngine.InitEventHandler(new UserEventHandler());

        rtcEngine.EnableAudio();
        rtcEngine.EnableAudioVolumeIndication(200, 3, true);
    }

    public void JoinChannel(string channel, uint uuid)
    {
        string token = ""; // ko dùng token

        rtcEngine.JoinChannel(token, channel, "", 0);
        Debug.Log("Join channel: " + channel);
    }

    public void LeaveChannel()
    {
        rtcEngine.LeaveChannel();
    }

    public void SetMic(bool enable)
    {
        rtcEngine.MuteLocalAudioStream(!enable);

        Debug.Log("Mic: " + (enable ? "ON" : "OFF"));
    }
}

class UserEventHandler : IRtcEngineEventHandler
{
    public override void OnAudioVolumeIndication(
        RtcConnection connection,
        AudioVolumeInfo[] speakers,
        uint speakerNumber,
        int totalVolume)
    {
        foreach (var speaker in speakers)
        {
            if (speaker.uid == 0)
            {
                Debug.Log("Mic volume: " + speaker.volume);
            }
        }
    }
}
