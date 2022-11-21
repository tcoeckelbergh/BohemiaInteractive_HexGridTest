using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridSnappingWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    HexTile.HexTileType type;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/GridSnappingWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GridSnappingWindow window = (GridSnappingWindow)EditorWindow.GetWindow(typeof(GridSnappingWindow));
        window.Show();
    }

    void OnGUI()
    {
        // Selected Tile 
        if (Selection.activeGameObject != null)
        {
            if (Selection.activeGameObject.tag == "Tile")
            {
                GUILayout.Label("Selected Tile", EditorStyles.boldLabel);
                EditorGUILayout.ColorField(Selection.activeGameObject.GetComponentInChildren<SpriteRenderer>().color);
                type = (HexTile.HexTileType)EditorGUILayout.EnumPopup("HexTileType: ", type);

                if (GUILayout.Button("UPDATE"))
                {
                    Selection.activeGameObject.GetComponent<HexTile>().UpdateType(type);
                }
            }
            else
            {
                GUILayout.Label("No Tile selected", EditorStyles.boldLabel);
                
            }
        }

        GUILayout.Space(20);

        // Snapping all tiles
        GUILayout.Label("Press button to snap all hexTiles to closest grid position", EditorStyles.boldLabel);
        if (GUILayout.Button("SNAP"))
        {
            RunGridSnapping();
        }
    }

    void RunGridSnapping()
    {
        var tileList = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tileList)
        {
            Debug.Log(tile.name);
        }

        // For each tile, calculate closest grid position

        // If that position is taken, move the tile to the FRONT-most sorting layer and color it red + throw a warning dialog
    }
}
