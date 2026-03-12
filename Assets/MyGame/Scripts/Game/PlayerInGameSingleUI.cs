using TMPro;
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
        Debug.Log("Set player: " + name + " - " + uid);
    }

    public void SetVolume(int volume)
    {
        volumeSlider.value = volume / 255f;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}

