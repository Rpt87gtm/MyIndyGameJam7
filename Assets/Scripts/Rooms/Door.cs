using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite _closeDoor;
    [SerializeField] Sprite _openDoor;
    [SerializeField] private bool _isOpen;
    public bool IsOpen => _isOpen;

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_isOpen)
            OpenDoor();
        else 
            CloseDoor();
    }

    public void OpenDoor()
    {
        if (!_isOpen)
        {
            _boxCollider.enabled = false;
            _spriteRenderer.sprite = _openDoor;
            _isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (_isOpen)
        {
            _boxCollider.enabled = true;
            _spriteRenderer.sprite = _closeDoor;
            _isOpen = false;
        }
    }


}
