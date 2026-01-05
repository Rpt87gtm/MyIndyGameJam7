using System;
using System.Collections;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public RectTransform target;
    public Vector2 startPosition;

    public Vector2 finalPosition;
    public float duration = 1f;
    private bool _isUp = true;
    private Coroutine move;

    public void MoveUp(bool forse = false)
    {
        if (_isUp) return;
        Debug.Log("move up");
        _isUp = true;
        if (move != null)
        {
            StopCoroutine(move);
        }
        if (forse)
        {
            target.position = finalPosition;
        }
        else
        {
            move = StartCoroutine(Move(_isUp, duration));
        }
    }

    public void MoveDown(bool forse = false)
    {
        if (!_isUp) return;
        Debug.Log("move down");
        _isUp = false;
        if (move != null)
        {
            StopCoroutine(move);
        }
        if (forse)
        {
            target.position = finalPosition;
        }
        else
        {
            move = StartCoroutine(Move(_isUp, duration));
        }
    }

    private IEnumerator Move(bool isUpMove, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            Vector2 newPos = new();
            if (isUpMove)
            {
                newPos.x = Mathf.Lerp(startPosition.x, finalPosition.x, 1 - currentTime / duration);
                newPos.y = Mathf.Lerp(startPosition.y, finalPosition.y, 1 - currentTime / duration);
            }
            else
            {
                newPos.x = Mathf.Lerp(startPosition.x, finalPosition.x, currentTime / duration);
                newPos.y = Mathf.Lerp(startPosition.y, finalPosition.y, currentTime / duration);
            }
            target.position = newPos;
            currentTime += Time.deltaTime;
            yield return null;
        }
        target.position = isUpMove ? startPosition : finalPosition;
    }
}
