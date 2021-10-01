using UnityEngine;
using ViewModel;

namespace View
{
    public class SnakeControl : ISnakeControl
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        public Vector2Int DirectionToMove
        {
            get
            {
                float horizontal = Input.GetAxis(HorizontalAxisName);
                float vertical = Input.GetAxis(VerticalAxisName);

                if (!Mathf.Approximately(horizontal, 0))
                {
                    return horizontal > 0 ? Vector2Int.right : Vector2Int.left;
                }

                if (!Mathf.Approximately(vertical, 0))
                {
                    return vertical > 0 ? Vector2Int.up : Vector2Int.down;
                }
                
                return Vector2Int.zero;
            }
        }
    }
}