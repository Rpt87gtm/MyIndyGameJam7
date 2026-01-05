using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePausePanel : MonoBehaviour
{
    public Button exitButton;
    public GameObject panelGO;
    public GameObject settingsMenu;
    private InputSystem_Actions _playerInput;
    private Pause pause;
    private int pauseId = 0;
    private bool isOpened = false;


    public void Open()
    {
        pause.RequestPause(pauseId);
        Debug.Log("open pause panel");
        panelGO.SetActive(true);
    }
    public void Close()
    {
        pause.Unpause(pauseId);
        Debug.Log("close pause panel");
        settingsMenu.SetActive(false);
        panelGO.SetActive(false);
    }
    void Awake()
    {
        _playerInput = new();
        pause = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<Pause>();
        panelGO.SetActive(false);
    }
    void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.UI.Menu.performed += OnEscPressed;
        exitButton.onClick.AddListener(SwitchState);
    }

    void OnDisable()
    {
        exitButton.onClick.RemoveListener(SwitchState);
        _playerInput.UI.Menu.performed -= OnEscPressed;
        _playerInput.Disable();
    }

    private void OnEscPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        SwitchState();
    }

    public void SwitchState()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("no pause menu in main menu");
            return;
        }
        if (isOpened)
        {
            Close();
        }
        else
        {
            Open();
        }
        isOpened = !isOpened;
    }
}


