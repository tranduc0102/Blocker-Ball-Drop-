using UnityEngine;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    private LevelEditorManager editorManager;
    private string fileName = "Level_1";

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("🧱 LEVEL EDITOR TOOL", EditorStyles.boldLabel);
        GUILayout.Space(5);

        editorManager = (LevelEditorManager)EditorGUILayout.ObjectField("Editor Manager", editorManager, typeof(LevelEditorManager), true);

        if (editorManager == null)
        {
            EditorGUILayout.HelpBox("Kéo LevelEditorManager trong scene vào đây!", MessageType.Info);
            return;
        }

        // --- Mode Selection ---
        GUILayout.Space(10);
        GUILayout.Label("✏️ Edit Mode", EditorStyles.boldLabel);
        editorManager.mode = (EditorMode)EditorGUILayout.EnumPopup("Mode", editorManager.mode);

        // --- Block Settings ---
        if (editorManager.mode == EditorMode.Block)
        {
            GUILayout.Space(10);
            GUILayout.Label("🔷 Block Settings", EditorStyles.boldLabel);
            editorManager.currentShape = (BlockShape)EditorGUILayout.EnumPopup("Shape", editorManager.currentShape);
            editorManager.currentColor = (ColorBlock)EditorGUILayout.EnumPopup("Color", editorManager.currentColor);
            editorManager.currentRotation = (Rotation)EditorGUILayout.EnumPopup("Rotation", editorManager.currentRotation);

            EditorGUILayout.HelpBox("Phím tắt: R = xoay, C = đổi màu, 1–4 = đổi shape", MessageType.None);
        }

        // --- Save/Load ---
        GUILayout.Space(15);
        GUILayout.Label("💾 Save & Load", EditorStyles.boldLabel);
        fileName = EditorGUILayout.TextField("File Name", fileName);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Level"))
        {
            editorManager.SaveLevel(fileName);
        }

        if (GUILayout.Button("Load Level"))
        {
            editorManager.LoadLevel(fileName);
        }
        GUILayout.EndHorizontal();

        // --- Utilities ---
        GUILayout.Space(10);
        if (GUILayout.Button("🧹 Clear All"))
        {
            if (EditorUtility.DisplayDialog("Clear All", "Xóa toàn bộ cell và đối tượng trong editor?", "Yes", "No"))
            {
                Undo.RegisterFullObjectHierarchyUndo(editorManager.gameObject, "Clear All");
                editorManager.ClearAll();
            }
        }
        GUILayout.Space(10);
        GUILayout.Label("⚙️ Level Settings", EditorStyles.boldLabel);
        editorManager.levelWidth = EditorGUILayout.IntField("Width", editorManager.levelWidth);
        editorManager.levelHeight = EditorGUILayout.IntField("Height", editorManager.levelHeight);
        editorManager.levelTime = EditorGUILayout.FloatField("Time (sec)", editorManager.levelTime);
/*        editorManager.cameraSize = EditorGUILayout.Slider("Camera Size", editorManager.cameraSize, 5f, 50f);
*/
        if (GUILayout.Button("🔄 Regenerate Grid"))
        {
            editorManager.GenerateGrid();
        }

    }
}
