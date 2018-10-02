using UnityEngine;

namespace UIs.Common
{
    [RequireComponent(typeof(RectTransform))]
    public class SetCanvasBounds : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        public RectTransform rectTransform;

        Rect lastSafeArea = new Rect(0, 0, 0, 0);

        void ApplySafeArea(Rect area)
        {
            var anchorMin = area.position;
            var anchorMax = area.position + area.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            lastSafeArea = area;
        }

        void Update()
        {
            Rect safeArea = Screen.safeArea;

            if (safeArea != lastSafeArea)
                ApplySafeArea(safeArea);
        }

        void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}
