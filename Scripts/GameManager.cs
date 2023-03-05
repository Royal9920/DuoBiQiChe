using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
/// <summary>
/// 整个游戏的管理单例类
/// </summary>
public class GameManager
{
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    private static GameManager _instance;
    private Dictionary<string, GameObject> _dict;
    public Dictionary<string, GameObject> Dict => _dict;

    public Updater Updater;
    public Updater LateUpdater;
    private CubeMove _cubeMove;
    private CameraControl _cameraControl;
    public CubeMove CubeMove => _cubeMove;

    private GameOverUI _gameOverUI;
    public GameOverUI GameOverUI => _gameOverUI;
    private MainUI _mainUI;
    public MainUI MainUI => _mainUI;

    private OtherCubeManager _otherCubeManager;

    public OtherCubeManager OtherCubeManager => _otherCubeManager;

    private GroundManager _groundManager;
    public GroundManager GroundManager => _groundManager;
    public GameManager()
    {
        Updater = new Updater();
        LateUpdater = new Updater();
        _dict = new Dictionary<string, GameObject>();
        var activeScene = SceneManager.GetActiveScene();
        var rootGos = activeScene.GetRootGameObjects();
        foreach(var rootGo in rootGos)
        {
            _dict.Add(rootGo.name, rootGo);
            Debug.Log($"rootGo:{rootGo.name}");
        }
    }


    public void Start()
    {
        _cubeMove = new CubeMove(_dict["CubeMove"].transform);
        _cameraControl = new CameraControl(_dict["Main Camera"].transform);
        _otherCubeManager = new OtherCubeManager(_dict["OtherCubes"].transform);
        _groundManager = new GroundManager(_dict["GroundManager"].transform);
        InitUI();
    }

    private void InitUI()
    {
        var uiCanvas = _dict["UICanvas"].transform;
        _gameOverUI = new GameOverUI(uiCanvas.Find("UI_Gameover"));
        _mainUI = new MainUI(uiCanvas.Find("UI_Main"));
    }
}
