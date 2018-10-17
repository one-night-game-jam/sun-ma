using System;
using UnityEditor;
using UnityEngine;

public class ScreenshotTaker
{
    [MenuItem("Tools/Capture Screenshot %t")]
    static void CaptureScreenShot()
    {
        var fileName = $"Screenshot_{Screen.width}x{Screen.height}_{DateTime.Now:yyyyMMddHHmmss}.png";
        ScreenCapture.CaptureScreenshot(fileName);
        Debug.Log($"Captured a screenshot {Application.persistentDataPath}/{fileName}");
    }
}
