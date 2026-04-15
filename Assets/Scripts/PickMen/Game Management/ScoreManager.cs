using Shears;
using UnityEngine;

namespace PickMen.GameManagement
{
    public class ScoreManager : PersistentProtectedSingleton<ScoreManager>
    {
        [SerializeField]
        private int score;

        public static int Score => Instance.score;

        public static void AddScore(int scoreToAdd) {
            Instance.InstAddScore(scoreToAdd);
        }

        private void InstAddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            print(score);
        }
    }
}
