using System;
using UnityEngine;
using ViewModel;

namespace View
{
    public class FoodView : MonoBehaviour
    {
        private FoodLogic foodLogic;

        public void Construct(FoodLogic foodLogic)
        {
            this.foodLogic = foodLogic;

            RegisterFoodPositionChange();
        }

        private void OnDestroy()
        {
            UnregisterFoodPositionChange();
        }

        private void RegisterFoodPositionChange() => foodLogic.NewFoodPosition += OnNewFoodPosition;
        private void UnregisterFoodPositionChange() => foodLogic.NewFoodPosition -= OnNewFoodPosition;

        private void OnNewFoodPosition(Vector2Int newPosition) 
            => transform.position = new Vector3(newPosition.x, newPosition.y, -2);
    }
}