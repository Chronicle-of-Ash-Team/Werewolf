using UnityEngine;

public class ResponsiveCanvas : MonoBehaviour
{
    [SerializeField] private GameObject mobileUI;
    [SerializeField] private GameObject desktopUI;

    private void Start()
    {
        if (Screen.width > Screen.height)
        {
            mobileUI.SetActive(false);
            desktopUI.SetActive(true);
        }
        else
        {
            desktopUI.SetActive(false);
            mobileUI.SetActive(true);
        }
    }

    //void Update()
    //{
    //    if (Screen.width > Screen.height)
    //    {
    //        mobileUI.SetActive(false);
    //        desktopUI.SetActive(true);
    //    }
    //    else
    //    {
    //        desktopUI.SetActive(false);
    //        mobileUI.SetActive(true);
    //    }
    //}
}
