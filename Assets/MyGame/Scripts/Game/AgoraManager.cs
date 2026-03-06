using Agora.Rtc;
using UnityEngine;

public class AgoraManager : MonoBehaviour
{
    public static AgoraManager Instance { get; private set; }

    private const string APP_ID = "6022e0e1f8034b729273203ca1b80bd8";

    private IRtcEngine rtcEngine;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rtcEngine = RtcEngine.CreateAgoraRtcEngine();

        RtcEngineContext context = new RtcEngineContext();
        context.appId = APP_ID;
        context.channelProfile = CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING;
        context.audioScenario = AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT;

        rtcEngine.Initialize(context);
        rtcEngine.InitEventHandler(new UserEventHandler());

        rtcEngine.EnableAudio();
        rtcEngine.EnableAudioVolumeIndication(200, 3, true);

        JoinChannel("test_channel");
    }

    void OnDestroy()
    {
        if (rtcEngine != null)
        {
            rtcEngine.LeaveChannel();
            rtcEngine.Dispose();
        }
    }

    public void JoinChannel(string channel)
    {
        string token = ""; // ko dùng token

        rtcEngine.JoinChannel(token, channel, "", 0);
        Debug.Log(rtcEngine.GetConnectionState());
    }

    public void LeaveChannel()
    {
        rtcEngine.LeaveChannel();
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
