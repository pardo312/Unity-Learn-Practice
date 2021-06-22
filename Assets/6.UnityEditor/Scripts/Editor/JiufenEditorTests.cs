using UnityEditor;
using UnityEngine;
public class JiufenEditorTests  : EditorWindow
{
    [MenuItem("JiufenEditorTests/WindowTest")]
    public static void ShowWindow()
    {
        GetWindow<JiufenEditorTests>(false, "WindowTest", true);
    }
}