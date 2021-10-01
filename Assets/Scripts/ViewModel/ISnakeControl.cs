using UnityEngine;

namespace ViewModel
{
    public interface ISnakeControl
    {
        Vector2Int DirectionToMove { get; }
    }
}