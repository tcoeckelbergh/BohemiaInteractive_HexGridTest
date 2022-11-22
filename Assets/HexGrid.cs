using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexGrid : MonoBehaviour
{
    private List<Vector2> coordinates = new List<Vector2>();

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

    public Dictionary<string, HexTile> GetHexTileNeighbours(float size, Vector2 posOriginalTile)
    {
        // Returns a dictionary of neighbours in the following format
        // 0 = top neighbour, 1 = top right neighbour, 2 = bottom right neighbour
        // 3 = bottom neighbour, 4 = bottom left neighbour, 5 = top left neighbour

        Dictionary<string, HexTile> neighbours = new Dictionary<string, HexTile>();

        // Top neighbour
        neighbours.Add("Top-Neighbour", GetHexTileAtPosition(new Vector2(posOriginalTile.x, posOriginalTile.y + (size * Mathf.Sqrt(3)))));

        // Top-right neighbour
        neighbours.Add("Top-right-Neighbour", GetHexTileAtPosition(new Vector2(posOriginalTile.x + (size * 3 / 2), posOriginalTile.y + (size * Mathf.Sqrt(3) / 2))));

        // Bottom-right neighbour
        neighbours.Add("Bottom-right-Neighbour", GetHexTileAtPosition(new Vector2(posOriginalTile.x + (size * 3 / 2), posOriginalTile.y - (size * Mathf.Sqrt(3) / 2))));

        // Bottom neighbour
        neighbours.Add("Bottom-Neighbour", GetHexTileAtPosition(new Vector2(posOriginalTile.x, posOriginalTile.y - (size * Mathf.Sqrt(3)))));

        // Bottom-left neighbour
        neighbours.Add("Bottom-left-Neighbour", GetHexTileAtPosition(new Vector2(posOriginalTile.x - (size * 3 / 2), posOriginalTile.y - (size * Mathf.Sqrt(3) / 2))));

        // Top-left neighbour
        neighbours.Add("Top-left-Neighbour", GetHexTileAtPosition(new Vector2(posOriginalTile.x - (size * 3 / 2), posOriginalTile.y + (size * Mathf.Sqrt(3) / 2))));

        return neighbours;
    }

    public HexTile GetHexTileAtPosition(Vector2 position)
    {
        LayerMask mask = LayerMask.NameToLayer("Grid");
        var hits = Physics2D.OverlapCircleAll(position, 0.1f, 1 << 6);

        if (hits.Length == 0)
        {
            return null;
        }
        if (hits.Length == 1)
        {
            return hits[0].gameObject.GetComponent<HexTile>();
        }

        Debug.LogWarning("There are overlapping neighbours for (" + position.x + ", " + position.y + ") -> Please take a look.");
        return null;
    }
}
