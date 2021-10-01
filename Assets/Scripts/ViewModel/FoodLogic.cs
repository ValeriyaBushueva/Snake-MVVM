using System;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ViewModel
{
    public class FoodLogic
    {
        private Food foodModel;
        private GameFieldLogic gameFieldLogic;

        public event Action<Vector2Int> NewFoodPosition;

        public FoodLogic(Food foodModel, GameFieldLogic gameFieldLogic)
        {
            this.foodModel = foodModel;
            this.gameFieldLogic = gameFieldLogic;
        }

        public void Initialize()
        {
            SetNewFoodPosition();
        }

        public bool IsFoodOnPosition(Vector2Int position) => foodModel.Position == position;
        
        public void SetFoodIsEaten()
        {
            SetNewFoodPosition();
        }

        private void SetNewFoodPosition()
        {
            Vector2Int newPosition = gameFieldLogic.GetRandomPosition();
            foodModel.Position = newPosition;
            RaiseNewFoodPosition(newPosition);
        }

        private void RaiseNewFoodPosition(Vector2Int newPosition) => NewFoodPosition?.Invoke(newPosition);
    }
}