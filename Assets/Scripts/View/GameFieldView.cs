using UnityEngine;

namespace View
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private Transform backgroundCellContainer;
        [SerializeField] private GameObject backgroundCellPrefab;
        private Vector2Int fieldSize;

        public void Construct(Vector2Int fieldSize)
        {
            this.fieldSize = fieldSize;

            for (int i = 0; i <= fieldSize.x; i++)
            {
                for (int j = 0; j <= fieldSize.y; j++)
                {
                    Instantiate(backgroundCellPrefab, new Vector3(i, j), Quaternion.identity, backgroundCellContainer);
                }
            }
        }
    }
}