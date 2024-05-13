public struct Utilities
{
    public static void QuitPlayModeInEditor()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
