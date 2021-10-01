using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class Snake
    {
        public Vector2Int MoveDirection = Vector2Int.up;
        public List<SnakeElement> SnakeElements;

        public bool IsFeeded = true;

        public SnakeElement Head => SnakeElements.First();

        public SnakeElement Tale => SnakeElements.First();

        public List<SnakeElement> DefaultSnakeElements
        {
            get
            {
                Vector2Int fieldCenter = fieldSize / 2;
            
                return new List<SnakeElement>
                {
                    new SnakeElement(fieldCenter)
                    , new SnakeElement(new Vector2Int(fieldCenter.x, fieldCenter.y - -1))
                };
            }
        }

        private Vector2Int fieldSize;
        public Snake(Vector2Int fieldSize)
        {
            this.fieldSize = fieldSize;
            SnakeElements = DefaultSnakeElements;
        }
    }
}
