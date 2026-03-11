using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInGameSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameTxt;
    [SerializeField] private Slider volumeSlider;
    public uint uid;

    public void SetPlayer(string name, uint uid)
    {
        playerNameTxt.text = name;
        this.uid = uid;
    }

    public void SetVolume(int volume)
    {
        volumeSlider.value = volume / 255f;
    }
}

