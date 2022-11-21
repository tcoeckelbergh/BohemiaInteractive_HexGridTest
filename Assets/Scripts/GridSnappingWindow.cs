using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridSnappingWindow : EditorWindow
{
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
        // Selected Tiles
        if (Selection.gameObjects != null)
        {
            int counter1 = 0;
            int counter2 = 0;
            int counter3 = 0;
            int counter4 = 0;
            int counter5 = 0;

            GUILayout.Label("Selected Tiles", EditorStyles.boldLabel);
            foreach (var obj in Selection.gameObjects)
            {
                if (obj.tag == "Tile")
                {
                    switch(obj.GetComponent<HexTile>().HexType)
                    {
                        case HexTile.HexTileType.one:
                            counter1++;
                            break;
                        case HexTile.HexTileType.two:
                            counter2++;
                            break;
                        case HexTile.HexTileType.three:
                            counter3++;
                            break;
                        case HexTile.HexTileType.four:
                            counter4++;
                            break;
                        case HexTile.HexTileType.five:
                            counter5++;
                            break;
                    }
                    //EditorGUILayout.EnumPopup(obj.GetComponent<HexTile>().hexType);
                }
            }

            if (counter1 > 0)
            {
                GUILayout.Label(counter1 + " times", EditorStyles.boldLabel);
                EditorGUILayout.EnumPopup(HexTile.HexTileType.one);
            }
            
            if (counter2 > 0)
            {
                GUILayout.Label(counter2 + " times", EditorStyles.boldLabel);
                EditorGUILayout.EnumPopup(HexTile.HexTileType.two);
            }
            
            if (counter3 > 0)
            {
                GUILayout.Label(counter3 + " times", EditorStyles.boldLabel);
                EditorGUILayout.EnumPopup(HexTile.HexTileType.three);
            }

            if (counter4 > 0)
            {
                GUILayout.Label(counter4 + " times", EditorStyles.boldLabel);
                EditorGUILayout.EnumPopup(HexTile.HexTileType.four);
            }
          
            if (counter5 > 0)
            {
                GUILayout.Label(counter5 + " times", EditorStyles.boldLabel);
                EditorGUILayout.EnumPopup(HexTile.HexTileType.five);
            }

            type = (HexTile.HexTileType)EditorGUILayout.EnumPopup("Select new HexTileType: ", type);
            if (GUILayout.Button("UPDATE"))
            {
                foreach (var obj in Selection.gameObjects)
                {
                    if (obj.tag == "Tile")
                    {
                        Debug.Log("LOG - Setting all selected tiles to " + type + " with # lives being " + (int)type);
                        obj.GetComponent<HexTile>().UpdateType(type);
                        EditorUtility.SetDirty(obj.GetComponent<HexTile>());
                    }
                }  
            }
        }
        else
        {
            GUILayout.Label("No Tile selected", EditorStyles.boldLabel);

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
