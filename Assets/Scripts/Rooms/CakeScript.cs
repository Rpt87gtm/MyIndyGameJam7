using TMPro;
using UnityEngine;

public class CakeScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _textMesh.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
