using System;
using UnityEditor;
using UnityEngine;

public enum TABS
{
    LOGIN_WITH_EMAIL = 0,
    SIGNUP = 1
}
        
public class JiufenEditorTests : EditorWindow
{
    public TABS op;

    [MenuItem("JiufenEditorTests/WindowTest")]
    public static void ShowWindow()
    {
        GetWindow<JiufenEditorTests>(false, "WindowTest", true);
    }
    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal( GUILayout.ExpandHeight(true));

        //Vertical
        GUILayout.BeginVertical(GUILayout.Width(10));
        EditorGUILayout.LabelField("Model to test:");
        op = (TABS)EditorGUILayout.EnumPopup(op);

        //Slider
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider,GUILayout.ExpandWidth(true) );    

        //TextArea
        var areaStyle = new GUIStyle(GUI.skin.label);
        areaStyle.wordWrap = true;
        areaStyle.alignment = TextAnchor.UpperCenter;
        EditorGUILayout.LabelField("Readme: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        ,areaStyle,GUILayout.Width(200),GUILayout.ExpandHeight(true));
        
        GUILayout.EndVertical();
        //EndVertical

        EditorGUILayout.LabelField("", GUI.skin.verticalSlider,GUILayout.Width(5), GUILayout.ExpandHeight(true));    
        ShowCurrentOption();

        EditorGUILayout.EndHorizontal();
    }

    private void ShowCurrentOption()
    {
        switch(op){
            case TABS.LOGIN_WITH_EMAIL:
                ShowSettings();
                break;
            case TABS.SIGNUP:
                ShowInfo();
                break;
        }
    }

    void ShowInfo(){
        EditorGUILayout.TextField("Creator: Daniel Pardo");
    }

    void ShowSettings(){
        EditorGUILayout.BeginVertical();
        WindowTestData.MuteAllSounds =
            EditorGUILayout.Toggle("Mute All Sounds", WindowTestData.MuteAllSounds);
        WindowTestData.PlayerLifes =
            EditorGUILayout.IntField("Player Lifes", WindowTestData.PlayerLifes);
        WindowTestData.PlayerTwoName =
            EditorGUILayout.TextField("Player Two Name", WindowTestData.PlayerTwoName);

        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(30)))
        {
            WindowTestData.MuteAllSounds = false;
            WindowTestData.PlayerLifes = 4;
            WindowTestData.PlayerTwoName = "John";
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

}