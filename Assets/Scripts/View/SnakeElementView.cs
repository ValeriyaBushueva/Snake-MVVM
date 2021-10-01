using UnityEngine;

namespace View
{
    public class SnakeElementView : MonoBehaviour
    {
        public Vector2Int Position
        {
            set => transform.position = new Vector3(value.x, value.y, 0);
        }
    }
}