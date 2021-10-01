using System;
using System.Collections.Generic;
using UnityEngine;
using ViewModel;

namespace View
{
    public class SnakeView : MonoBehaviour
    {
        [SerializeField] private SnakeElementView snakeElementPrefab;
        private SnakeLogic snakeLogic;

        private List<SnakeElementView> currentElements = new List<SnakeElementView>();

        public void Construct(SnakeLogic snakeLogic)
        {
            this.snakeLogic = snakeLogic;

            RegisterSnakeChanges();
        }

        private void OnDestroy()
        {
            UnregisterSnakeChanges();
        }

        private void RegisterSnakeChanges()
        {
            snakeLogic.SnakePositionChanged += OnPositionChange;
            snakeLogic.SnakeNewElement += OnNewElement;
            snakeLogic.Reset += OnReset;
        }

        private void UnregisterSnakeChanges()
        {
            snakeLogic.SnakePositionChanged -= OnPositionChange;
            snakeLogic.SnakeNewElement -= OnNewElement;
            snakeLogic.Reset -= OnReset;
        }

        private void OnPositionChange(List<Vector2Int> elementPositions)
        {
            for (var i = 0; i < elementPositions.Count; i++)
            {
                currentElements[i].Position = elementPositions[i];
            }
        }

        private void OnNewElement(Vector2Int newElementPosition)
        {
            Vector3 position = new Vector3(newElementPosition.x, newElementPosition.y, 0);
            SnakeElementView view = Instantiate(snakeElementPrefab, position, Quaternion.identity, transform);
            
            currentElements.Add(view);
        }

        private void OnReset()
        {
            foreach (SnakeElementView snakeElementView in currentElements)
            {
                Destroy(snakeElementView.gameObject);
            }
            
            currentElements.Clear();
        }
    }
}