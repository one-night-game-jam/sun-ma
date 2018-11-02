using UnityEngine;

namespace Sanmas
{
    public class SanmaDebugGUI : MonoBehaviour
    {
        [SerializeField] SanmaCore _core;
        GUIStyle _guiStyle;

        void Awake()
        {
            _guiStyle = new GUIStyle
            {
                fontSize = 48,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            };
        }

        void OnGUI()
        {
            var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Label(new Rect (screenPoint.x - 50, Screen.height - screenPoint.y - 150, 200, 100), $"{_core.BroiledValue.Value*200:F2}%", _guiStyle);

        }
    }
}
