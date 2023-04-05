using UnityEngine;
public class UIData : MonoBehaviour
{
    [SerializeField] private RectTransform _startPanelTranform;
    [SerializeField] private RectTransform _gamePanelTransform;
    [SerializeField] private RectTransform _gameWinPanelTranform;




    private void Awake()
    {
        LoadGameUIElement();
    }


    private void LoadGameUIElement()
    {
        UIManager.Instance.startPanelTransform = _startPanelTranform;
        UIManager.Instance.gamePanelTransform = _gamePanelTransform;
        UIManager.Instance.gameWinPanelTransform = _gameWinPanelTranform;
    }



    public void Restart()
    {
        GameManager.Instance.Restart();
    }
}
