using UnityEngine;

public static class AppSystemManager
{
    public static void Quit()
    {
        // ゲームを終了させます。
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}