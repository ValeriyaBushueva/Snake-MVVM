using Model;
using UnityEngine;

namespace ViewModel
{
    public class GameLogic
    {
        private ISnakeControl snakeControl;
        private SnakeLogic snakeLogic;
        private FoodLogic foodLogic;

        private Vector2Int ControlledDirection => snakeControl.DirectionToMove;

        public GameLogic(ISnakeControl snakeControl, SnakeLogic snakeLogic, FoodLogic foodLogic)
        {
            this.snakeControl = snakeControl;
            this.snakeLogic = snakeLogic;
            this.foodLogic = foodLogic;
        }

        public void Initialize() => snakeLogic.FailCollision += RestartGame;

        public void Execute()
        {
            ProcessMove();

            ProcessFeeding();
        }

        private void ProcessMove()
        {
            if (SnakeIsControlled())
            {
                snakeLogic.Move(ControlledDirection);
            }
            else
            {
                snakeLogic.Move();
            }
        }

        private void ProcessFeeding()
        {
            if (foodLogic.IsFoodOnPosition(snakeLogic.HeadPosition))
            {
                snakeLogic.Feed();
                foodLogic.SetFoodIsEaten();
            }
        }

        private void RestartGame() => snakeLogic.ResetSnake();

        private bool SnakeIsControlled() => ControlledDirection.sqrMagnitude > 0;
    }
}