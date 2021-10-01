using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

namespace ViewModel
{
    public class SnakeLogic
    {
        private Snake snakeModel;
        private GameFieldLogic gameFieldLogic; 

        public event Action FailCollision;

        public event Action<List<Vector2Int>> SnakePositionChanged;
        public event Action<Vector2Int> SnakeNewElement;
        public event Action Reset;
        public Vector2Int HeadPosition => snakeModel.Head.Position;

        private bool IsFeeded
        {
            get => snakeModel.IsFeeded;
            set => snakeModel.IsFeeded = value;
        }

        public SnakeLogic(Snake snakeModel, GameFieldLogic gameFieldLogic)
        {
            this.snakeModel = snakeModel;
            this.gameFieldLogic = gameFieldLogic;
        }

        public void Initialize()
        {
            RaiseAboutAllNewElements();
        }

        private void RaiseAboutAllNewElements()
        {
            foreach (SnakeElement snakeElement in snakeModel.SnakeElements)
            {
                SnakeNewElement?.Invoke(snakeElement.Position);
            }
        }

        public void Move() => Move(snakeModel.MoveDirection);

        public void Move(Vector2Int direction)
        {
            if (IsDirectionNotOppositeAsSnakeDirection(direction))
            {
                snakeModel.MoveDirection = direction;
            }
            
            Vector2Int talePosition = snakeModel.Tale.Position;

            MoveSnakeElements();

            ProcessFeedOnMove(talePosition);

            RaiseSnakePositionChange();
        }

        private bool IsDirectionNotOppositeAsSnakeDirection(Vector2Int direction) => GetReverseDirection(direction) != snakeModel.MoveDirection;

        private Vector2Int GetReverseDirection(Vector2Int direction) => direction * -1;

        public void Feed()
        {
            IsFeeded = true;
        }

        public void ResetSnake()
        {
            snakeModel.SnakeElements = snakeModel.DefaultSnakeElements.ToList();
            RaiseReset();
            
            RaiseAboutAllNewElements();
            RaiseSnakePositionChange();
        }

        private void RaiseReset()
        {
            Reset?.Invoke();
        }

        private void MoveSnakeElements()
        {
            Vector2Int newPreviousPosition = HeadPosition;
            Vector2Int oldPreviousPosition = Vector2Int.zero;
            
            snakeModel.Head.Position = HeadPosition + snakeModel.MoveDirection;
            
            foreach (var snakeElement in snakeModel.SnakeElements.Skip(1))
            {
                oldPreviousPosition = newPreviousPosition;
                newPreviousPosition = snakeElement.Position;
                
                snakeElement.Position = oldPreviousPosition;
            }

            WallHitCheck();
            SelfCollisionCheck();
        }

        private void ProcessFeedOnMove(Vector2Int taleElementPosition)
        {
            if (IsFeeded)
            {
                IsFeeded = false;
                snakeModel.SnakeElements.Add(new SnakeElement(taleElementPosition));
                SnakeNewElement?.Invoke(taleElementPosition);
            }
        }

        private void WallHitCheck()
        {
            if (gameFieldLogic.IsPositionOverWall(HeadPosition))
            {
                RaiseFailCollision();
            }
        }

        private void SelfCollisionCheck()
        {
            foreach (var snakeElement in snakeModel.SnakeElements.Skip(1))
            {
                if (snakeElement.Position == HeadPosition)
                {
                    RaiseFailCollision();
                }
            }
        }
        

        private void RaiseFailCollision() => FailCollision?.Invoke();

        private void RaiseSnakePositionChange()
        {
            List<Vector2Int> currentSnakePositions = snakeModel.SnakeElements.Select(x => x.Position).ToList();
            SnakePositionChanged?.Invoke(currentSnakePositions);
        }
    }
}