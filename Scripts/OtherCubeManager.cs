using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class OtherCubeManager
{
    private Transform _trans;
    private GameObject _otherCubeGo;
    private List<GameObject> _unUsedOtherCubes;
    private List<GameObject> _usedOtherCubes;
    private Vector3 _lastPos;
    private float _yPos;
    public OtherCubeManager(Transform trans)
    {
        _trans = trans;
        _otherCubeGo = Resources.Load<GameObject>("Prefabs/TrafficCars/Car_2A");
        _unUsedOtherCubes = new List<GameObject>();
        _usedOtherCubes = new List<GameObject>();
        _yPos = -0.33f;
        for(int i = 0; i < 10; ++i)
        {
            var cubeGo = GameObject.Instantiate(_otherCubeGo);
            cubeGo.transform.SetParent(_trans, false);
            var oldPos = cubeGo.transform.localPosition;
            oldPos.y = _yPos;
            cubeGo.transform.localPosition = oldPos;
            cubeGo.transform.localScale = Vector3.one * 0.8f;
            cubeGo.SetActive(false);
            _unUsedOtherCubes.Add(cubeGo);
        }

        GameManager.Instance.Updater.Add(Update);
    }


    private void Update()
    {
        if(GameManager.Instance.CubeMove != null)
        {
            //回收已经在屏幕外的红色方块
            var playerPos = GameManager.Instance.CubeMove.Trans.position;
            for(int i = _usedOtherCubes.Count - 1; i >= 0; --i)
            {
                var cubeGo = _usedOtherCubes[i];
                var pos = cubeGo.transform.position;
                if(playerPos.z -pos.z > 5.0f)
                {
                    _unUsedOtherCubes.Add(cubeGo);
                    cubeGo.gameObject.SetActive(false);
                    _usedOtherCubes.RemoveAt(i);
                }
            }

            //将现在空置的红色方块重新设置位置
            for(int i = _unUsedOtherCubes.Count - 1; i >= 0; --i)
            {
                var cubeGo = _unUsedOtherCubes[i];
                _lastPos.x = UnityEngine.Random.Range(-5.0f, 5.0f);
                _lastPos.y = _yPos;
                _lastPos.z += 8f;
                cubeGo.transform.position = _lastPos;
                cubeGo.gameObject.SetActive(true);
                _unUsedOtherCubes.RemoveAt(i);
                _usedOtherCubes.Add(cubeGo);
            }
           
        }
        
    }

    public void ReStartGame()
    {
        _lastPos = Vector3.zero;
        _unUsedOtherCubes.AddRange(_usedOtherCubes);
        _usedOtherCubes.Clear();
    }


}
