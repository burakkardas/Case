using UnityEngine;
using System.IO;
using UDOGames;
using UnityEngine.SceneManagement;


public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector] public GameData gameData;

    public bool IsStart => _isStart;
    public bool IsGameEnd
    {
        get => _isGameEnd;
        set { _isGameEnd = value; }
    }
    private bool _firstTouch;
    private bool _isStart;
    private bool _isGameEnd;





    private void Awake()
    {
        Application.targetFrameRate = 120;
        GetGameDataValues();
    }



    private void Update()
    {
        HandleGameInputs();
    }




    private void HandleGameInputs()
    {
        if (CanStart())
        {
            _firstTouch = true;
            _isStart = true;
            _isGameEnd = false;
            UIManager.Instance.SetGameStartPanelMoves();
        }
    }



    public void Restart()
    {
        _firstTouch = false;
        _isStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    private bool CanStart()
    {
        return Input.GetMouseButtonDown(0) && !_firstTouch && !Utility.IsPointerOverUIObject();
    }


    #region Save&Load
    public void Save()
    {
        string _json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", _json);
    }


    public void Load()
    {
        string _json = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
        gameData = JsonUtility.FromJson<GameData>(_json);
    }



    private void GetGameDataValues()
    {
        gameData = new GameData();

        if (File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            Load();
        }
    }
    #endregion
}



[System.Serializable]
public class GameData
{
    public float coin = 0;
}