using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class RaycastDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DebugRaycast();
        }
    }

    void DebugRaycast()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        Debug.Log($"=== Raycast Results ({results.Count} objects) ===");

        foreach (RaycastResult result in results)
        {
            GameObject obj = result.gameObject;
            RectTransform rect = obj.GetComponent<RectTransform>();

            Debug.Log($"Object: {obj.name}");
            Debug.Log($"  - Layer: {LayerMask.LayerToName(obj.layer)}");
            Debug.Log($"  - Position: {obj.transform.position}");
            Debug.Log($"  - Rect: {rect.rect}");
            Debug.Log($"  - Block Raycasts: {obj.GetComponent<CanvasRenderer>()?.cullTransparentMesh}");
            Debug.Log($"  - Graphics Raycaster: {obj.GetComponentInParent<GraphicRaycaster>()?.name}");

            // Проверяем компоненты
            CheckComponents(obj);
        }
    }

    void CheckComponents(GameObject obj)
    {
        // Проверяем Image
        UnityEngine.UI.Image image = obj.GetComponent<UnityEngine.UI.Image>();
        if (image != null)
        {
            Debug.Log($"  - Image: raycastTarget={image.raycastTarget}, color={image.color}");
        }

        // Проверяем RawImage
        UnityEngine.UI.RawImage rawImage = obj.GetComponent<UnityEngine.UI.RawImage>();
        if (rawImage != null)
        {
            Debug.Log($"  - RawImage: raycastTarget={rawImage.raycastTarget}");
        }

        // Проверяем Text
        UnityEngine.UI.Text text = obj.GetComponent<UnityEngine.UI.Text>();
        if (text != null)
        {
            Debug.Log($"  - Text: raycastTarget={text.raycastTarget}");
        }

        // Проверяем все CanvasGroup
        CanvasGroup[] canvasGroups = obj.GetComponentsInParent<CanvasGroup>();
        foreach (var group in canvasGroups)
        {
            Debug.Log($"  - CanvasGroup '{group.gameObject.name}': blocksRaycasts={group.blocksRaycasts}, ignoreParentGroups={group.ignoreParentGroups}");
        }
    }
}