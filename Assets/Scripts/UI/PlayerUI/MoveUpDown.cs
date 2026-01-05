using System;
using System.Collections;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public RectTransform target;
    public float startPosition;

    public float finalPosition = -200;
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
            var newpos = target.position;
            newpos.y = startPosition;
            target.position = newpos;
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
            var newpos = target.position;
            newpos.y = finalPosition;
            target.position = newpos;
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
            Vector2 newPos = target.position;
            if (isUpMove)
            {
                newPos.y = Mathf.Lerp(startPosition, finalPosition, 1 - currentTime / duration);
            }
            else
            {
                newPos.y = Mathf.Lerp(startPosition, finalPosition, currentTime / duration);
            }
            target.position = newPos;
            currentTime += Time.deltaTime;
            yield return null;
        }
        var newpos = target.position;
        newpos.y = isUpMove ? startPosition : finalPosition;
        target.position = newpos;
    }
}
