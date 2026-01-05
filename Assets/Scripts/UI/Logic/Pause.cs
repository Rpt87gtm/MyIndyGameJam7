using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private List<int> pauseRequestsIds = new();
    public bool IsPaused()
    {
        return pauseRequestsIds.Count > 0;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void RequestPause(int requestId)
    {
        if (pauseRequestsIds.Contains(requestId))
        {
            Debug.LogError($"Pause with id {requestId} already requested");
            return;
        }
        pauseRequestsIds.Add(requestId);
        SwitchPause();
    }

    public void Unpause(int requestId)
    {
        var existingId = pauseRequestsIds.Find(id => id == requestId);
        if (pauseRequestsIds.Contains(requestId))
        {
            pauseRequestsIds.Remove(existingId);
            SwitchPause();
            return;
        }

        Debug.LogError($"No pause with id {requestId}");
        SwitchPause();
    }

    public void UnPauseAll()
    {
        if (pauseRequestsIds.Count <= 0)
        {
            Debug.Log("Unpause requested but is not paused");
        }

        string pauseIds = "";
        foreach (var id in pauseRequestsIds)
        {
            pauseIds += $", {id}";
        }
        Debug.LogWarning($"You skip pauses with ids: {pauseIds}");
        pauseRequestsIds.Clear();
        SwitchPause();
    }

    private void SwitchPause()
    {
        Debug.Log("switch");
        if (pauseRequestsIds.Count > 0)
        {
            Time.timeScale = 0;
            Debug.Log("Game paused");
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("Game resumed");
        }
    }
}
