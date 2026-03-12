using Agora.Rtc;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform playerContainer;

    [SerializeField] private TextMeshProUGUI roomNameTxt;
    [SerializeField] private TextMeshProUGUI gameStateTxt;
    [SerializeField] private Button micButton;
    [SerializeField] private Button leaveButton;
    [SerializeField] private TMP_Dropdown voiceEffectDropdown;

    private Dictionary<string, AUDIO_EFFECT_PRESET> voiceEffects;

    private void Awake()
    {
        leaveButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.LeaveLobby();
            AgoraManager.Instance.LeaveChannel();
            SceneManager.LoadScene("MainMenu");
        });

        voiceEffects = new Dictionary<string, AUDIO_EFFECT_PRESET>
        {
            { "Normal", AUDIO_EFFECT_PRESET.AUDIO_EFFECT_OFF },
            { "Old man", AUDIO_EFFECT_PRESET.VOICE_CHANGER_EFFECT_OLDMAN },
            { "Boy", AUDIO_EFFECT_PRESET.VOICE_CHANGER_EFFECT_BOY },
            { "Sister", AUDIO_EFFECT_PRESET.VOICE_CHANGER_EFFECT_SISTER },
            { "Girl", AUDIO_EFFECT_PRESET.VOICE_CHANGER_EFFECT_GIRL },
            { "Uncle", AUDIO_EFFECT_PRESET.VOICE_CHANGER_EFFECT_UNCLE },
            { "Pigking", AUDIO_EFFECT_PRESET.VOICE_CHANGER_EFFECT_PIGKING },
            { "Hulk", AUDIO_EFFECT_PRESET.VOICE_CHANGER_EFFECT_HULK }
        };

        voiceEffectDropdown.ClearOptions();
        voiceEffectDropdown.AddOptions(new List<string>(voiceEffects.Keys));
        voiceEffectDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void Start()
    {
        roomNameTxt.text = LobbyManager.Instance.GetLobbyName();

        SpawnPlayers();
    }

    void OnDropdownChanged(int index)
    {
        string key = voiceEffectDropdown.options[index].text;
        var effect = voiceEffects[key];

        AgoraManager.Instance.SetVoiceEffect(effect);
    }

    private void SpawnPlayers()
    {
        Debug.Log(GameNetworkManager.Instance.GetPlayers());
        foreach (var player in GameNetworkManager.Instance.GetPlayers())
        {
            var obj = Instantiate(playerPref, playerContainer);

            var ui = obj.GetComponent<PlayerInGameSingleUI>();

            ui.SetPlayer(
                player.PlayerName.ToString(),
                player.AgoraUid
            );

            ui.Show();

            AgoraManager.Instance.JoinChannel(LobbyManager.Instance.GetLobbyName(), player.AgoraUid);
        }
    }
}
