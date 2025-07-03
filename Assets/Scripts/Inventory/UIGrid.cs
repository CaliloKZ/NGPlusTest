using System;
using UnityEngine;

namespace GridSystem
{
    public class UIGrid<T>
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Vector2 _cellSize;
        private readonly Vector2 _originPosition;

        private readonly T[,] _gridArray;

        public event Action<int, int, T> OnGridValueChanged;

        public UIGrid(int width, int height, Vector2 cellSize, Vector2 originPosition, Func<UIGrid<T>, int, int, T> createGridObject)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;

            _gridArray = new T[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _gridArray[x, y] = createGridObject(this, x, y);
                }
            }
        }
        
        public Vector2 GetWorldPosition(int x, int y)
        {
            float isoX = (x - y) * (_cellSize.x * 0.5f);
            float isoY = (x + y) * (_cellSize.y * 0.5f);
            return new Vector2(isoX, isoY) + _originPosition;
        }
        
        public void GetXY(Vector2 worldPosition, out int x, out int y)
        {
            Vector2 localPos = worldPosition - _originPosition;

            float halfCellWidth = _cellSize.x * 0.5f;
            float halfCellHeight = _cellSize.y * 0.5f;

            x = Mathf.FloorToInt((localPos.x / halfCellWidth + localPos.y / halfCellHeight) * 0.5f);
            y = Mathf.FloorToInt((localPos.y / halfCellHeight - localPos.x / halfCellWidth) * 0.5f);
        }

        public void SetValue(int x, int y, T value)
        {
            if (IsInBounds(x, y))
            {
                _gridArray[x, y] = value;
                OnGridValueChanged?.Invoke(x, y, value);
            }
        }

        public void SetValue(Vector2 worldPosition, T value)
        {
            GetXY(worldPosition, out int x, out int y);
            SetValue(x, y, value);
        }

        public T GetValue(int x, int y)
        {
            return IsInBounds(x, y) ? _gridArray[x, y] : default;
        }

        public T GetValue(Vector2 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return GetValue(x, y);
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _width && y < _height;
        }
    }
}
