using System.Linq;
using DG.Tweening;
using Game.Level;
using Game.Racket;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Pool;

namespace Game.Managers
{
    public class GameManager : MBSingleton<GameManager>
    {
        public RectTransform LayerBack => layerBack;
        public RectTransform LayerFront => layerFront;
        
        public LevelInspector LvlInspector => lvlInspector;

        public RacketController Racket => racket;
        public BallLogic Ball => ball;

        public int MaxBlocksInRow => maxBlocksInRow;
        public int MinBlocksInLevel => minBlocksInLevel;
        public int MaxBlocksInLevel => maxBlocksInLevel;
        public int DistanceBetweenBlocks => distanceBetweenBlocks;
        public int MaxBlockHp => maxBlockHp;
        public Gradient HpGradient => hpGradient;

        [SerializeField] private RectTransform layerBack;
        [SerializeField] private RectTransform layerFront;
        
        [SerializeField] private RectTransform gameZone;
        [SerializeField] private Score labelScore;
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Animator animatorButtonPlay;
        [SerializeField] private LevelInspector lvlInspector;

        [Header("Game objects")]
        [SerializeField] private RacketController racket;
        [SerializeField] private BallLogic ball;

        [Header("Gameplay Settings")] 
        [SerializeField] private int maxBlocksInRow = 5;
        [SerializeField] private int minBlocksInLevel = 10;
        [SerializeField] private int maxBlocksInLevel = 40;
        [SerializeField] private int distanceBetweenBlocks = 10;
        [SerializeField] private int maxBlockHp = 5;
        [SerializeField] private Gradient hpGradient;

        private Vector3 _ballStartPos;
        private int _scoreCount;
        
        private void Start()
        {
            buttonPlay.onClick.AddListener(StartGame);
            var localScale = gameZone.localScale/gameZone.sizeDelta;
            var screenScale = new Vector3(Screen.width, Screen.height, 1f);
            localScale = Vector3.Scale(localScale, screenScale);
            gameZone.localScale = localScale;
        }

        private void StartGame()
        {
            _scoreCount = 0;
            labelScore.Set(_scoreCount);
            
            lvlInspector.OpenLevel(LevelProvider.CreateRandomLevel());

            var o = ball.gameObject;
            var originalScale = o.transform.localScale;
            _ballStartPos = o.transform.localPosition;
            o.transform.localScale = Vector3.zero;
            o.SetActive(true);
            o.transform.DOScale(originalScale, 3f)
                .OnComplete(ball.EnableLogic);
        }

        public void IncreaseScore()
        {
            labelScore.Set(++_scoreCount);
        }
        
        public void CheckWin(IDeactivable obj)
        {
            if(PoolManager.Instance.BlockPool.Objects.Any(o => o.isActiveAndEnabled)) return;
            
            lvlInspector.OpenLevel(LevelProvider.CreateRandomLevel());
        }

        public void Restart()
        {
            lvlInspector.CloseLevel();
            animatorButtonPlay.Rebind();
            ball.DisableLogic();
            
            var o = ball.gameObject;
            o.SetActive(false);
            o.transform.localPosition = _ballStartPos;
        }
    }
}