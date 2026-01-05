using UnityEngine;
using UnityEngine.UI;

public class ExitOnClick : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
