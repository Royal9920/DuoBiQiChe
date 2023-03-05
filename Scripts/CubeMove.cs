using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class CubeMove
{
    private Transform _trans;
    private float _zMoveSpeed;
    private float _xMoveSpeed;
    public Transform Trans => _trans;
    private Vector3 _startPos;
    private float _startTime;
    private bool _isGameRunning;
    private AudioSource _audiSource;
    public CubeMove(Transform transform)
    {
        var carGo = Resources.Load<GameObject>("Prefabs/PlayerCars/Car_1A");
        _trans = GameObject.Instantiate(carGo).transform;
        _trans.localScale = Vector3.one * 0.6f;
        _trans.SetParent(transform, false);
        _audiSource = _trans.GetComponent<AudioSource>();
        GameManager.Instance.Updater.Add(Update);
        _zMoveSpeed = 8f;
        _xMoveSpeed = 5f;
        _startPos = _trans.position;
        StartGame();
        InitEvent();
    }

    private void StartGame()
    {
        _isGameRunning = true;
        _trans.position = _startPos;
        _startTime = Time.realtimeSinceStartup;
        _audiSource.enabled = true;
    }

    private void InitEvent()
    {
        EventCenter.Instance.OnCollisionEnter.AddListener(HitCallback);
        EventCenter.Instance.GameReStart.AddListener(GameReStartCallback);
    }

    private void HitCallback(GameObject go,Collision collision)
    {
        //Debug.Log($"Hit-------{go.name}_{collision.gameObject.name}");
        _isGameRunning = false;
        GameManager.Instance.GameOverUI.SetActive(true);
        var now = Time.realtimeSinceStartup;
        var lifeTime = now - _startTime;
        string lifeTimeText = $"存活时间:{(int)lifeTime}s";
        GameManager.Instance.GameOverUI.RefreshLifeTimeText(lifeTimeText);
        _audiSource.enabled = false;

    }

    private void GameReStartCallback()
    {
        GameManager.Instance.GameOverUI.SetActive(false);
        GameManager.Instance.OtherCubeManager.ReStartGame();
        GameManager.Instance.GroundManager.ReStartGame();
        StartGame();
    }

    private void Update()
    {
        if (_isGameRunning)
        {
            MoveX();
            MoveZ();
            UpdateMainUI();
        }
        
    }


    private void MoveZ()
    {
        var oldPos = _trans.position;
        oldPos.z += _zMoveSpeed * Time.deltaTime;
        _trans.position = oldPos;
    }

    private void MoveX()
    {
        var dir = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) {
            dir = new Vector3(-1, 0,0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            dir = new Vector3(1, 0,0);
        }

        var deltaPos = dir * _xMoveSpeed * Time.deltaTime;
        var oldPos = _trans.position;
        oldPos += deltaPos;
        _trans.position = oldPos;
    }

    private void UpdateMainUI()
    {
        string speedText = $"当前速度:{_zMoveSpeed}m/s";
        GameManager.Instance.MainUI.RefreshSpeedText(speedText);
        var nowPos = _trans.position;
        var distance = Vector3.Distance(nowPos, _startPos);
        string distanceText = $"通过距离:{(int)distance}m";
        GameManager.Instance.MainUI.RefreshDistanceText(distanceText);
        var nowTime = Time.realtimeSinceStartup;
        var lifeTime = nowTime - _startTime;
        string lifeTimeText = $"存活时间:{(int)lifeTime}s";
        GameManager.Instance.MainUI.RefreshLifeTimeText(lifeTimeText);

    }
}
