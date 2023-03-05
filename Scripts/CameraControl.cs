using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 摄像机的控制
/// </summary>
public class CameraControl
{
    private Transform _trans;
    private Vector3 _oldPos;
    public CameraControl(Transform trans)
    {
        _trans = trans;
        _oldPos = trans.position;
        GameManager.Instance.LateUpdater.Add(LateUpdate);
    }

    private void LateUpdate()
    {
        var cubePos = GameManager.Instance.CubeMove.Trans.position;
        _trans.position = new Vector3(0,_oldPos.y, cubePos.z - 4);
    }
}
