using UnityEngine;

public class BoomResize : MonoBehaviour
{
    public float statrtSize = 0;
    public float finalSize = 1;
    public float duration = 0.3f;
    private float currentTime = 0;

    void Start()
    {
        transform.localScale = Vector3.zero;
    }
    void Update()
    {
        if (currentTime > duration)
        {
            Destroy(gameObject);
            return;
        }
        currentTime += Time.deltaTime;
        var currentScale = currentTime / duration * finalSize;
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

}
