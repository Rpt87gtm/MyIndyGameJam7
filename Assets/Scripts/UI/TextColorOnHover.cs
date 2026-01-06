using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextColorOnHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Text Reference")]
    [SerializeField] private TMP_Text buttonText;

    [Header("Color Settings")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private Color pressedColor = new Color(1f, 0.5f, 0f); // Оранжевый

    [Header("Animation Settings")]
    [SerializeField] private float colorChangeDuration = 0.1f;
    [SerializeField] private bool useSmoothTransition = true;

    private Button buttonComponent;
    private bool isPointerOver = false;
    private bool isPressed = false;
    private Coroutine colorTransitionCoroutine;

    void Start()
    {
        // Находим текстовый компонент
        if (buttonText == null)
            buttonText = GetComponentInChildren<TMP_Text>();

        // Проверяем наличие Button компонента (опционально)
        buttonComponent = GetComponent<Button>();

        // Устанавливаем начальный цвет
        buttonText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;

        // Если кнопка не нажата в данный момент
        if (!isPressed)
        {
            ChangeColor(hoverColor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;

        // Если кнопка не нажата, возвращаем нормальный цвет
        if (!isPressed)
        {
            ChangeColor(normalColor);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Проверяем, что кнопка интерактивна (если есть Button компонент)
        if (buttonComponent != null && !buttonComponent.interactable)
            return;

        isPressed = true;
        ChangeColor(pressedColor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Проверяем, что кнопка интерактивна
        if (buttonComponent != null && !buttonComponent.interactable)
            return;

        isPressed = false;

        // После отпускания возвращаемся к hover или normal цвету
        if (isPointerOver)
        {
            ChangeColor(hoverColor);
        }
        else
        {
            ChangeColor(normalColor);
        }
    }

    // Дополнительный метод для обработки состояния disabled
    void OnDisable()
    {
        // При отключении объекта сбрасываем состояния
        isPointerOver = false;
        isPressed = false;

        if (buttonText != null)
        {
            buttonText.color = normalColor;
        }
    }

    private void ChangeColor(Color targetColor)
    {
        if (buttonText == null) return;

        // Останавливаем предыдущую корутину если есть
        if (colorTransitionCoroutine != null)
        {
            StopCoroutine(colorTransitionCoroutine);
        }

        if (useSmoothTransition && colorChangeDuration > 0)
        {
            colorTransitionCoroutine = StartCoroutine(SmoothColorChange(buttonText.color, targetColor));
        }
        else
        {
            buttonText.color = targetColor;
        }
    }

    private System.Collections.IEnumerator SmoothColorChange(Color from, Color to)
    {
        float elapsed = 0f;

        while (elapsed < colorChangeDuration)
        {
            buttonText.color = Color.Lerp(from, to, elapsed / colorChangeDuration);
            elapsed += Time.unscaledDeltaTime; // Используем unscaled для работы при паузе
            yield return null;
        }

        buttonText.color = to;
    }

    // Публичные методы для изменения цветов динамически
    public void SetNormalColor(Color newColor)
    {
        normalColor = newColor;
        if (!isPointerOver && !isPressed)
        {
            ChangeColor(normalColor);
        }
    }

    public void SetHoverColor(Color newColor)
    {
        hoverColor = newColor;
        if (isPointerOver && !isPressed)
        {
            ChangeColor(hoverColor);
        }
    }

    public void SetPressedColor(Color newColor)
    {
        pressedColor = newColor;
        if (isPressed)
        {
            ChangeColor(pressedColor);
        }
    }
}