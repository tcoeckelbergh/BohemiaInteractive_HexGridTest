using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridSnappingWindow : EditorWindow
{
    HexTile.HexTileType type;
    bool checkNeighbours = false;
    HexGrid hexGrid;
    HexTileColors hexColors;

    [MenuItem("Window/GridSnappingWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GridSnappingWindow window = (GridSnappingWindow)EditorWindow.GetWindow(typeof(GridSnappingWindow));
        window.Show();
    }

    void OnGUI()
    {
        #region Initialization
        if (!hexGrid)
        {
            hexGrid = GameObject.FindObjectOfType<HexGrid>();
            hexGrid.CalculateCoordinatePositions(1.152f, 20, 20);
        }
        if (!hexColors)
        {
            hexColors = GameObject.FindObjectOfType<GameResources>().HexTileColors;
        }
        #endregion

        #region Changing HexTileType of one or multiple tiles
        if (Selection.gameObjects != null)
        {
            // Counters to display the number of selected objects per HexTileType (1-5)
            int counter1 = 0;
            int counter2 = 0;
            int counter3 = 0;
            int counter4 = 0;
            int counter5 = 0;

            GUILayout.Label("Currently selected tiles", EditorStyles.boldLabel);

            // Loop over each object and increment the appropriate counter
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
                }
            }

            // Display all counters with their associated HexTileType
            if (counter1 > 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(counter1 + "x", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EnumPopup(HexTile.HexTileType.one);
                GUILayout.FlexibleSpace();
                EditorGUILayout.ColorField(hexColors.oneLivesColor);
                EditorGUILayout.EndHorizontal();
            }
            
            if (counter2 > 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(counter2 + "x", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EnumPopup(HexTile.HexTileType.two);
                GUILayout.FlexibleSpace();
                EditorGUILayout.ColorField(hexColors.twoLivesColor);
                EditorGUILayout.EndHorizontal();
            }
            
            if (counter3 > 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(counter3 + "x", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EnumPopup(HexTile.HexTileType.three);
                GUILayout.FlexibleSpace();
                EditorGUILayout.ColorField(hexColors.threeLivesColor);
                EditorGUILayout.EndHorizontal();
            }

            if (counter4 > 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(counter4 + "x", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EnumPopup(HexTile.HexTileType.four);
                GUILayout.FlexibleSpace();
                EditorGUILayout.ColorField(hexColors.fourLivesColor);
                EditorGUILayout.EndHorizontal();
            }
          
            if (counter5 > 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(counter5 + "x", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EnumPopup(HexTile.HexTileType.five);
                GUILayout.FlexibleSpace();
                EditorGUILayout.ColorField(hexColors.fiveLivesColor);
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            // Provide a choice menu to change the HexTileType of all Tiles selected to the new HexTileType chosen
            type = (HexTile.HexTileType)EditorGUILayout.EnumPopup("Change selection into: ", type);
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
        #endregion

        GUILayout.Space(20);

        #region Snap All Tiles to closest grid position

        GUILayout.Label("Press button to snap all hexTiles to closest grid position", EditorStyles.boldLabel);
        if (GUILayout.Button("SNAP"))
        {
            var tileList = GameObject.FindGameObjectsWithTag("Tile");
            foreach (GameObject tile in tileList)
            {
                Vector2 closestGridPos = hexGrid.CalculateClosestGridPosition(tile.transform.position);
                tile.transform.position = new Vector3(closestGridPos.x, closestGridPos.y, 0);
                EditorUtility.SetDirty(tile.transform);
            }
        }
        #endregion

        GUILayout.Space(20);

        checkNeighbours = GUILayout.Toggle(checkNeighbours, "Display Neighbours of the last selected tile");

        if (Selection.gameObjects.Length > 0)
        {
            if (Selection.activeGameObject.tag == "Tile")
            {
                if (checkNeighbours)
                {
                    var neighbours = hexGrid.GetHexTileNeighbours(1.152f, Selection.activeGameObject.transform.position);
                    foreach (var n in neighbours)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label(n.Key);
                        GUILayout.FlexibleSpace();
                        if (n.Value != null)
                            EditorGUILayout.EnumPopup(n.Value.HexType);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }  
    }
}
