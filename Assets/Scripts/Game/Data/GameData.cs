using UnityEngine;

namespace Game.Data
{
    public class GameData : MonoBehaviour
    {
        public Data<int> Score;
        
        public void Initialize()
        {
            Score = new Data<int>(0);
        }
    }
}