using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Mirror;

public class ButtonManager : MonoBehaviour
{
    public Button jumpBtn;
    public Button fireBtn;
    public Button quitBtn;

    public delegate void TapAction();
    public static event TapAction OnJump;
    public delegate void Test();

    public static event Test OnFire;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        fireBtn = root.Q<Button>("FireBtn");
        fireBtn.clicked += FireBtnClicked;

        jumpBtn = root.Q<Button>("JumpBtn");
        jumpBtn.clicked += JumpBtnClicked;

        quitBtn = root.Q<Button>("QuitBtn");
        quitBtn.clicked += StopClient;
    }

    void StopClient()
    {
        NetworkManager.singleton.StopClient();
        SceneManager.LoadScene("Mk - Menu");
    }
    void JumpBtnClicked()
    {
        OnJump();
    }
    void FireBtnClicked()
    {
        OnFire();
    }
}
