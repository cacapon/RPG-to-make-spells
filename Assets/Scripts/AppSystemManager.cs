using UnityEngine;

public static class AppSystemManager
{
    public static void Quit()
    {
        // �Q�[�����I�������܂��B
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}