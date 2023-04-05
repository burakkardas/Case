using UnityEngine;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [HideInInspector] public RectTransform startPanelTransform;
    [HideInInspector] public RectTransform gamePanelTransform;
    [HideInInspector] public RectTransform gameWinPanelTransform;



    #region Animation
    [SerializeField] private Sequence _sequence;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _startPanelEndYValue;
    [SerializeField] private float _duration;
    #endregion


    private void Start()
    {
        _sequence = DOTween.Sequence();
    }



    public void SetGameStartPanelMoves()
    {

        _sequence.Append(startPanelTransform.DOLocalMoveY(_startPanelEndYValue, _duration).SetEase(_ease));
        _sequence.Append(gamePanelTransform.DOLocalMoveY(0, _duration).SetEase(_ease));
    }



    public void SetGameEndPanelMoves()
    {
        gameWinPanelTransform.DOLocalMoveY(0, _duration);
    }
}
