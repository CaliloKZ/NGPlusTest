using System;

namespace Grid
{
    public class UIGrid<T>
    {
        private readonly int _width;
        private readonly int _height;
        private readonly T[,] _gridArray;

        public UIGrid(int width, int height, Func<UIGrid<T>, int, int, T> createGridObject)
        {
            _width = width;
            _height = height;
            _gridArray = new T[_width, _height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _gridArray[x, y] = createGridObject(this, x, y);
                }
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
