using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class GroundManager
{
    private Transform _trans;
    private GameObject _groundGo;
    private List<GameObject> _unUsedGrounds;
    private List<GameObject> _usedGrounds;
    private Vector3 _lastPos;
    public GroundManager(Transform trans) {
        _trans = trans;
        _groundGo = Resources.Load<GameObject>("Prefabs/HighWayRoads/Highway_RoadGroup");
        _unUsedGrounds = new List<GameObject>();
        _usedGrounds = new List<GameObject>();
        for(int i = 0; i < 10; ++i)
        {
            var groundGo = GameObject.Instantiate(_groundGo);
            groundGo.transform.SetParent(_trans, false);
            //groundGo.SetActive(false);
            groundGo.transform.localPosition = new Vector3(0, 0, 62 * i);
            _usedGrounds.Add(groundGo);
            _lastPos = groundGo.transform.localPosition;
        }
        GameManager.Instance.Updater.Add(Update);
    }

    private void Update()
    {
        if(GameManager.Instance.CubeMove!= null)
        {
            var playerPos = GameManager.Instance.CubeMove.Trans.position;
            for(int i = _usedGrounds.Count - 1; i >= 0; --i)
            {
                var groundGo = _usedGrounds[i];
                var pos = groundGo.transform.position;
                if(playerPos.z-pos.z > 50.0f)
                {
                    _unUsedGrounds.Add(groundGo);
                    groundGo.SetActive(false);
                    _usedGrounds.RemoveAt(i);
                }
            }

            for(int i = _unUsedGrounds.Count - 1; i >= 0; --i)
            {
                var groundGo = _unUsedGrounds[i];
                _lastPos.z += 62;
                groundGo.transform.localPosition = _lastPos;
                _unUsedGrounds.RemoveAt(i);
                _usedGrounds.Add(groundGo);
                groundGo.SetActive(true);
            }
        }
    }

    public void ReStartGame()
    {
        _lastPos = new Vector3(0, 0, -62);
        _unUsedGrounds.AddRange(_usedGrounds);
        _usedGrounds.Clear();
    }
}
