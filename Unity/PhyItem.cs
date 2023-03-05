using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class PhyItem:MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EventCenter.Instance.OnCollisionEnter.Broadcast(transform.gameObject, collision);
    }
}
