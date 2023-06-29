using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Grid<TGridObject> where TGridObject : BlankTilePosition
    {
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

        public class OnGridObjectChangedEventArgs : EventArgs
        {
            public int x; 
            public int y;
        }

        private int width;
        private int height;
        private float cellSize;
        private Vector3 gridOrigin;
        private TGridObject[,] gridObjectsArray;

        public Grid(int width, int height, float cellSize, Vector3 gridOrigin, Func<Grid<TGridObject>, int, int, TGridObject> createdGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.gridOrigin = gridOrigin;

            gridObjectsArray = new TGridObject[width, height];

            for (int x = 0; x < gridObjectsArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridObjectsArray.GetLength(1); y++)
                {
                    gridObjectsArray[x, y] = createdGridObject(this, x, y);
                }
            }

            ShowGridMapDebug(true);
        }

        private void ShowGridMapDebug(bool show)
        {
            if (show)
            {
                TextMesh[,] debugTextArray = new TextMesh[width, height];

                for (int x = 0; x < gridObjectsArray.GetLength(0); x++)
                {
                    for (int y = 0; y < gridObjectsArray.GetLength(1); y++)
                    {
                        debugTextArray[x, y] = CreateWorldText(gridObjectsArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.black, TextAnchor.MiddleCenter);
                        debugTextArray[x, y].characterSize = .1f;
                        debugTextArray[x, y].fontSize = 30;
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

                OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridObjectsArray[eventArgs.x, eventArgs.y]?.GetTileType().ToString();
                };
            }
        }

        private TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, 
            Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
        {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }

        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPos, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPos;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.color = color;
            textMesh.fontSize = fontSize;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + gridOrigin;
        }

        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridObjectsArray[x, y];
            }
            else
                return default;
        }

        public void TriggerGridObjectChanged(int x, int y)
        {
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }
}
