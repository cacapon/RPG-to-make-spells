using UnityEngine;

public static class AppSystemManager
{
    public static void Quit()
    {
        // ÉQÅ[ÉÄÇèIóπÇ≥ÇπÇ‹Ç∑ÅB
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}