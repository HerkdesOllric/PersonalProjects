using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Base.Game.Signal;
namespace Main
{
    public class UIManager : Singleton<UIManager>
    {

        //Again!! i was just testing something! 

        public TextMeshProUGUI TextScore;

        public float MainScore
        {
            get { return _mainScore; }
            set { _mainScore = Mathf.Clamp(value, 0, Mathf.Infinity); }
        }
        float _mainScore;

        int Multiplier = 1;

        public float IncreaseScore
        {
            get { return 0; }
            set { MainScore += value * Multiplier; }
        }

        public float DecreaseScore
        {
            get { return 0; }
            set { MainScore += value; }
        }

        private void OnDisable()
        {
            SignalBus<SGScoreChange, float, float>.Instance.UnRegister(ChangeScore);
            SignalBus<SGScoreMultiChange, int>.Instance.UnRegister(MultiChange);
        }

        void Start()
        {
            SignalBus<SGScoreChange, float, float>.Instance.Register(ChangeScore);
            SignalBus<SGScoreMultiChange, int>.Instance.Register(MultiChange);
            Initilize();
        }

        void Initilize()
        {
            TextScore.text = Mathf.RoundToInt(MainScore).ToString();
        }
        void ChangeScore(float PositivePoint, float NegativePoint)
        {
            IncreaseScore += PositivePoint;
            DecreaseScore -= NegativePoint;
            TextScore.text = Mathf.RoundToInt(MainScore).ToString();
        }

        void MultiChange(int Increase)
        {
            Multiplier += Increase;
        }
    }

}
