using System.Collections.Generic;
using Model;
using UnityEngine;

namespace ViewModel
{
    public class GameFieldLogic
    {
        private GameField gameField;

        private int FieldSizeX => gameField.FieldSize.x;
        private int FieldSizeY => gameField.FieldSize.y;

        public GameFieldLogic(GameField gameField)
        {
            this.gameField = gameField;
        }

        public Vector2Int GetRandomPosition()
        {
            int randomX = Random.Range(0, FieldSizeX + 1);
            int randomY = Random.Range(0, FieldSizeY + 1);

            return new Vector2Int(randomX, randomY);
        }

        public bool IsPositionOverWall(Vector2Int position)
        {
            if (position.x > FieldSizeX
                || position.x < 0
                || position.y > FieldSizeY
                || position.y < 0)
            {
                return true;
            }

            return false;
        }
    }
}