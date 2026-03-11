using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class GameNetworkManager : NetworkBehaviour
{
    public static GameNetworkManager Instance {  get; private set; }

    private NetworkList<PlayerData> playerDatas = new NetworkList<PlayerData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        PlayerData player = new PlayerData
        {
            ClientId = clientId,
            PlayerName = LobbyManager.Instance.GetPlayerName(),
            AgoraUid = (uint)UnityEngine.Random.Range(1, 99999)
        };

        playerDatas.Add(player);

        Debug.Log("Player joined: " + player.PlayerName);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.Log("Player left: " + clientId);
        for (int i = 0; i < playerDatas.Count; i++)
        {
            if (playerDatas[i].ClientId == clientId)
            {
                playerDatas.RemoveAt(i);
                break;
            }
        }
    }

    public NetworkList<PlayerData> GetPlayers()
    {
        return playerDatas;
    }
}

public struct PlayerData : INetworkSerializable, IEquatable<PlayerData>
{
    public ulong ClientId;
    public FixedString32Bytes PlayerName;
    public uint AgoraUid;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer)
        where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref PlayerName);
        serializer.SerializeValue(ref AgoraUid);
    }

    public bool Equals(PlayerData other)
    {
        return ClientId == other.ClientId;
    }
}
