using DG.Tweening;
using Game.Data;
using Game.Level;
using Game.Racket;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game.Managers
{
    public class GameManager : MBSingleton<GameManager>
    {
        public RectTransform LayerBack => layerBack;
        public RectTransform LayerMid => layerMid;
        public RectTransform LayerFront => layerFront;
        
        public Animator AnimatorButtonPlay => animatorButtonPlay;

        public GameData GameData => gameData;
        public LevelInspector LvlInspector => lvlInspector;

        public RacketController Racket => racket;

        public int MaxBlocksInRow => maxBlocksInRow;
        public int MinBlocksInLevel => minBlocksInLevel;
        public int MaxBlocksInLevel => maxBlocksInLevel;
        public int DistanceBetweenBlocks => distanceBetweenBlocks;
        public int MaxBlockHp => maxBlockHp;
        public Gradient HpGradient => hpGradient;

        [SerializeField] private RectTransform layerBack;
        [SerializeField] private RectTransform layerMid;
        [SerializeField] private RectTransform layerFront;
        
        [SerializeField] private RectTransform gameZone;
        [SerializeField] private Score labelScore;
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Animator animatorButtonPlay;
        
        [SerializeField] private GameData gameData;
        [SerializeField] private LevelInspector lvlInspector;

        [Header("Game objects")]
        [SerializeField] private RacketController racket;
        [SerializeField] private Transform ballStartPos;

        [Header("Gameplay Settings")] 
        [SerializeField] private int maxBlocksInRow = 5;
        [SerializeField] private int minBlocksInLevel = 10;
        [SerializeField] private int maxBlocksInLevel = 40;
        [SerializeField] private int distanceBetweenBlocks = 10;
        [SerializeField] private int maxBlockHp = 5;
        [SerializeField] private Gradient hpGradient;
        
        private int _scoreCount;
        
        private void Start()
        {
            gameData.Initialize();
            GameData.Score.ChangeEvent += labelScore.Show;
            
            buttonPlay.onClick.AddListener(StartGame);
            
            var localScale = gameZone.localScale/gameZone.sizeDelta;
            var screenScale = new Vector3(Screen.width, Screen.height, 1f);
            localScale = Vector3.Scale(localScale, screenScale);
            gameZone.localScale = localScale;
        }

        private void StartGame()
        {
            GameData.Score.Set(0);
            
            lvlInspector.OpenLevel(LevelProvider.CreateRandomLevel());

            var ball = PoolManager.Instance.BallPool.GetObject();
            var o = ball.gameObject;
            var originalScale = o.transform.localScale;
            o.transform.localPosition = ballStartPos.localPosition;
            o.transform.localScale = Vector3.zero;
            o.transform.DOScale(originalScale, 3f)
                .OnComplete(ball.EnableLogic);
        }
    }
}