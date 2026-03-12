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

    private void Awake()
    {
        leaveButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.LeaveLobby();
            AgoraManager.Instance.LeaveChannel();
            SceneManager.LoadScene("MainMenu");
        });
    }

    void Start()
    {
        roomNameTxt.text = LobbyManager.Instance.GetLobbyName();

        SpawnPlayers();
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
