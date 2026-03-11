using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomNameTxt.text = LobbyManager.Instance.GetLobbyName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
