using System;
using System.Collections.Generic;
using bullets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reload : MonoBehaviour
{
    public Inventory inventory;
    private PlayerUI playerUI;

    public Player player;
    private InputSystem_Actions inputAction;
    private bool isReloading = false;

    void Awake()
    {
        inputAction = new();
    }
    void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
    }
    void OnEnable()
    {
        inputAction.Enable();
        inputAction.Player.Interact.performed += SwithReload;
    }

    void OnDisable()
    {
        inputAction.Player.Interact.performed -= SwithReload;
        inputAction.Disable();
    }

    void ReloadCallback(List<BulletType> bullets)
    {
        player.SetBullets(bullets);
    }

    private void SwithReload(InputAction.CallbackContext context)
    {
        if (isReloading)
        {
            Debug.Log("stop reload");
            playerUI.StopReload();
            isReloading = false;
        }
        else
        {
            Debug.Log("start reload");
            playerUI.StartReload(ReloadCallback);
            isReloading = true;
        }
    }
}
