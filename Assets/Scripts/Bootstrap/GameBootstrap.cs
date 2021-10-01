using System;
using Model;
using UnityEngine;
using View;
using ViewModel;

namespace Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Properties")] 
        [SerializeField] private Vector2Int gameFieldSize;
        
        [Header("View components")]
        [SerializeField] private SnakeView snakeView;
        [SerializeField] private FoodView foodView;
        [SerializeField] private GameExecuter gameExecuter;
        [SerializeField] private GameFieldView gameFieldView;
        
        private Snake snakeModel;
        private Food foodModel = new Food();
        private GameField gameFieldModel = new GameField();

        private SnakeLogic snakeLogic;
        private GameFieldLogic gameFieldLogic;
        private GameLogic gameLogic;
        private FoodLogic foodLogic;

        private ISnakeControl snakeControl = new SnakeControl();

        private void Start()
        {
            Boot();
        }

        private void Boot()
        {
            InitializeSnakeModel();
            
            InitializeGameFieldSize();

            InitializeViewModels();
            
            InitializeFoodStartPosition();
            
            InitializeViews();
            
            ViewModelPostInitialize();
        }

        private void InitializeSnakeModel() => snakeModel = new Snake(gameFieldSize);

        private void InitializeGameFieldSize() => gameFieldModel.FieldSize = gameFieldSize;

        private void InitializeViewModels()
        {
            gameFieldLogic = new GameFieldLogic(gameFieldModel);

            snakeLogic = new SnakeLogic(snakeModel, gameFieldLogic);

            foodLogic = new FoodLogic(foodModel, gameFieldLogic);

            gameLogic = new GameLogic(snakeControl, snakeLogic, foodLogic);
        }

        private void InitializeFoodStartPosition() => foodModel.Position = gameFieldLogic.GetRandomPosition();

        private void InitializeViews()
        {
            gameFieldView.Construct(gameFieldModel.FieldSize);
            
            snakeView.Construct(snakeLogic);
            foodView.Construct(foodLogic);
            gameExecuter.Construct(gameLogic);
        }

        private void ViewModelPostInitialize()
        {
            snakeLogic.Initialize();
            foodLogic.Initialize();
            gameLogic.Initialize();
        }
    }
}