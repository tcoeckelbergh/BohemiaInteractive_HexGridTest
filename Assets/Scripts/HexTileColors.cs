using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HexTileColors")]
public class HexTileColors : ScriptableObject
{
    public Color fiveLivesColor;
    public Color fourLivesColor;
    public Color threeLivesColor;
    public Color twoLivesColor;
    public Color oneLivesColor;

    public Color GetColor(HexTile.HexTileType type)
    {
        switch (type)
        {
            case HexTile.HexTileType.five:
                return fiveLivesColor;
            case HexTile.HexTileType.four:
                return fourLivesColor;
            case HexTile.HexTileType.three:
                return threeLivesColor;
            case HexTile.HexTileType.two:
                return twoLivesColor;
            case HexTile.HexTileType.one:
                return oneLivesColor;
            default:
                Debug.LogWarning("Unbound HexFileType provided to GetColor(type): " + type);
                return Color.clear;
        }
    }
}
