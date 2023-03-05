using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class MainUI
{
    private Text _speedText;
    private Text _distanceText;
    private Text _lifeTimeText;
    private Transform _trans;
    public MainUI(Transform trans)
    {
        _trans = trans;
        _speedText = _trans.Find("speed").GetComponent<Text>();
        _distanceText = _trans.Find("distance").GetComponent<Text>();
        _lifeTimeText = _trans.Find("life_time").GetComponent<Text>();

    }

    public void RefreshSpeedText(string speedText)
    {
        _speedText.text = speedText;
    }

    public void RefreshDistanceText(string distanceText)
    {
        _distanceText.text = distanceText;
    }

    public void RefreshLifeTimeText(string lifeTimeText)
    {
        _lifeTimeText.text = lifeTimeText;
    }
}
