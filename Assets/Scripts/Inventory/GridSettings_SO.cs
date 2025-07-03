using System;
using UnityEngine;
using GridSystem;
using Inventory;

public class GridSettings_SO : ScriptableObject
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public Vector2 cellSize = new Vector2(1, 1);
    public Vector2 originPosition = Vector2.zero;
}
