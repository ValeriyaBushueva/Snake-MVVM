using System;
using System.Collections;
using UnityEngine;
using ViewModel;

namespace View
{
    public class GameExecuter : MonoBehaviour
    {
        [Range(1,10)]
        [SerializeField] private int snakeMovesPerSecond;

        private GameLogic gameLogic;

        public void Construct(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;

            StartCoroutine(Execution());
        }

        private IEnumerator Execution()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(1f / snakeMovesPerSecond);
                
                gameLogic.Execute();
            }
        }
    }
}