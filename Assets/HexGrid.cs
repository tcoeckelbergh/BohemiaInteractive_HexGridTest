using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private List<HexTile> hexTiles;

    private List<Vector2> coordinates = new List<Vector2>();

    private void Awake()
    {
        //CalculateCoordinatePositions(1.152f, 10, 10);

        //foreach (Vector2 v in coordinates)
        //{
        //    Debug.Log("(" + v.x + ", " + v.y + ")");
        //}
    }

    public Vector2 CalculateClosestGridPosition(Vector2 pos)
    {
        //  Distance formula -> d=sqrt((x2 – x1)² + (y2 – y1)²)
        // Quick and dirty comparison to find closest point (not actual distance, not relevant here)
        Vector2 closestPointInGrid = new Vector2(Mathf.Infinity, Mathf.Infinity);
        float smallestRelativeDistance = Mathf.Infinity;

        foreach (Vector2 v in coordinates)
        {
            float relativeDistance = Mathf.Pow((pos.x - v.x), 2) + Mathf.Pow((pos.y - v.y), 2);
            if (relativeDistance < smallestRelativeDistance)
            {
                smallestRelativeDistance = relativeDistance;
                closestPointInGrid = v;
            }
        }

        return closestPointInGrid;
    }

    public void CalculateCoordinatePositions(float size, int rows, int columns)
    {
        // In flat-top hex orientation
        // The x-coordinate increases with 3/2 * size of the hexagon, size being the distance of the center to the left- or right-most point
        // the y-coordinate increases with Sqrt(3) * size

        float yOffset = 0;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (j % 2 == 1) // if j = odd
                {
                    yOffset = size * Mathf.Sqrt(3) / 2;
                }
                else
                {
                    yOffset = 0;
                }

                Vector2 pos = new Vector2((j * size * 3/2), (i * size * Mathf.Sqrt(3)) + yOffset);
                coordinates.Add(pos);
            }
        }
    }
}
