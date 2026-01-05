using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePausePanel : MonoBehaviour
{
    public Button exitButton;
    public GameObject panelGO;
    private InputSystem_Actions _playerInput;
    private Pause pause;
    private int pauseId = 0;
    private bool isOpened = false;

    public void Open()
    {
        Debug.Log("open pause panel");
        pause.RequestPause(pauseId);
        panelGO.SetActive(true);
    }
    public void Close()
    {
        Debug.Log("close pause panel");
        pause.Unpause(pauseId);
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


