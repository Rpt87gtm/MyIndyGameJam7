using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageColorOnHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Image Reference")]
    [SerializeField] private Image targetImage;

    [Header("Color Settings")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(0.9f, 0.9f, 0.9f, 1f); // Светло-серый
    [SerializeField] private Color pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Серый

    [Header("Animation Settings")]
    [SerializeField] private float colorChangeDuration = 0.1f;
    [SerializeField] private bool useSmoothTransition = true;

    private Button buttonComponent;
    private bool isPointerOver = false;
    private bool isPressed = false;
    private Coroutine colorTransitionCoroutine;

    void Start()
    {
        // Находим Image компонент
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        if (targetImage == null)
        {
            Debug.LogError("No Image component found!");
            return;
        }

        // Проверяем наличие Button компонента (опционально)
        buttonComponent = GetComponent<Button>();

        // Устанавливаем начальный цвет
        targetImage.color = normalColor;
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

    // При отключении объекта сбрасываем состояния
    void OnDisable()
    {
        isPointerOver = false;
        isPressed = false;

        if (targetImage != null)
        {
            targetImage.color = normalColor;
        }
    }

    private void ChangeColor(Color targetColor)
    {
        if (targetImage == null) return;

        // Останавливаем предыдущую корутину если есть
        if (colorTransitionCoroutine != null)
        {
            StopCoroutine(colorTransitionCoroutine);
        }

        if (useSmoothTransition && colorChangeDuration > 0)
        {
            colorTransitionCoroutine = StartCoroutine(SmoothColorChange(targetImage.color, targetColor));
        }
        else
        {
            targetImage.color = targetColor;
        }
    }

    private System.Collections.IEnumerator SmoothColorChange(Color from, Color to)
    {
        float elapsed = 0f;

        while (elapsed < colorChangeDuration)
        {
            targetImage.color = Color.Lerp(from, to, elapsed / colorChangeDuration);
            elapsed += Time.unscaledDeltaTime; // Используем unscaled для работы при паузе
            yield return null;
        }

        targetImage.color = to;
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

    // Метод для сброса к исходным настройкам
    public void ResetToDefaultColors()
    {
        normalColor = Color.white;
        hoverColor = new Color(0.9f, 0.9f, 0.9f, 1f);
        pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);

        if (!isPointerOver && !isPressed)
        {
            ChangeColor(normalColor);
        }
    }
}