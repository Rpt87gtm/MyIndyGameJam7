using System;
using System.Collections.Generic;
using bullets;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reload : MonoBehaviour
{
    public Inventory inventory;
    private PlayerUI playerUI;

    public Player player;
    private InputSystem_Actions inputAction;
    private bool isReloading = false;

    private List<BulletType> _bullets = new();
    private AudioSource audioSource;
    public RandomSoundList reloadSounds;
    public AudioClip openReloadSound;
    public AudioClip closeReloadSound;

    public List<BulletType> GetBullets()
    {
        return _bullets;
    }

    public int Count()
    {
        return _bullets.Count;
    }

    public void Clear()
    {
        _bullets.Clear();
    }

    public void AddBullets(List<BulletType> bullets)
    {
        _bullets.AddRange(bullets);
    }

    public void RemoveAt(int pos)
    {
        _bullets.RemoveAt(pos);
    }
    public bool IsReloading
    {
        get { return isReloading; }
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        inputAction = new();
    }
    void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
        playerUI.Init(this);
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

    public void ReloadCallback(BulletType bullet)
    {
        _bullets.Add(bullet);
        inventory.UseBullet(bullet);
        reloadSounds.PlayRandomSound(audioSource);
        if (_bullets.Count < 6) return;
        StopReload();
    }
    private void StopReload()
    {
        audioSource.PlayOneShot(closeReloadSound);
        playerUI.StopReload();
        Debug.Log("stop reload");
        isReloading = false;
    }
    private void OnReloadPressed(InputAction.CallbackContext context)
    {
        if (_bullets.Count < 6)
        {
            SwithReload();
        }
        else
        {
            Debug.Log("No need reload");
        }
    }

    private void SwithReload()
    {
        if (isReloading)
        {
            StopReload();
        }
        else
        {
            Debug.Log("start reload");
            audioSource.PlayOneShot(openReloadSound);
            playerUI.StartReload(inventory.GetBulletsCount());
            isReloading = true;
        }
    }
}
