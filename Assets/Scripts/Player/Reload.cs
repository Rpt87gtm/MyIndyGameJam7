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

    public bool IsReloading
    {
        get { return isReloading; }
    }

    void Awake()
    {
        inputAction = new();
    }
    void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
        playerUI.Init(ReloadCallback);
    }
    void OnEnable()
    {
        inputAction.Enable();
        inputAction.Player.Interact.performed += OnReloadPressed;
    }

    void OnDisable()
    {
        inputAction.Player.Interact.performed -= OnReloadPressed;
        inputAction.Disable();
    }

    void ReloadCallback(List<BulletType> bullets)
    {
        player.SetBullets(bullets);
        foreach (var bulletType in bullets)
        {
            inventory.UseBullet(bulletType);
        }
        SwithReload();
    }
    private void OnReloadPressed(InputAction.CallbackContext context)
    {
        SwithReload();
    }

    private void SwithReload()
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
            playerUI.StartReload(inventory.GetBulletsCount());
            isReloading = true;
        }
    }
}
