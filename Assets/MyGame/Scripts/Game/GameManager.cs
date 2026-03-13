using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private string gameRoomName;

    private void Awake()
    {
        Instance = this;

        gameRoomName = LobbyManager.Instance.GetLobbyName();
    }


    public string GetGameRoomName()
    {
        return gameRoomName;
    }
}
