using System;
using UnityEngine;

namespace GridSystem
{
    public class UIGrid<T>
    {
        private readonly int _width;
        private readonly int _height;
        private readonly T[,] _gridArray;

        public event Action<int, int, T> OnGridValueChanged;

        public UIGrid(int width, int height, Func<UIGrid<T>, int, int, T> createGridObject)
        {
            _width = width;
            _height = height;
            _gridArray = new T[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _gridArray[x, y] = createGridObject(this, x, y);
                }
            }
        }

        public void SetValue(int x, int y, T value)
        {
            if (IsInBounds(x, y))
            {
                _gridArray[x, y] = value;
                OnGridValueChanged?.Invoke(x, y, value);
            }
        }

        public T GetValue(int x, int y)
        {
            return IsInBounds(x, y) ? _gridArray[x, y] : default;
        }

        bool IsInBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _width && y < _height;
        }
    }
}
