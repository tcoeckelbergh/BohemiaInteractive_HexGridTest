using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private List<HexTile> hexTiles;

    private List<Vector2> coordinates = new List<Vector2>();

    private void Awake()
    {
        CalculateCoordinatePositions(1.152f, 10, 10);

        foreach (Vector2 v in coordinates)
        {
            Debug.Log("(" + v.x + ", " + v.y + ")");
        }
    }

    public Vector2 CalculateClosestGridPosition(Vector2 pos)
    {
        // In flat-top hex orientation
        // The x-coordinate increases with 3/2 * size of the hexagon, size being the distance of the center to the left- or right-most point
        // the y-coordinate increases with Sqrt(3) * size


        return Vector2.zero;
    }

    public void CalculateCoordinatePositions(float size, int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector2 pos = new Vector2(i * size * 3/2, j * size * Mathf.Sqrt(3));
                coordinates.Add(pos);
            }
        }
    }
}