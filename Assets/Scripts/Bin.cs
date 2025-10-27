using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bin : MonoBehaviour
{
    [SerializeField] private TextMeshPro _txt;
    int _totalCount = 0;
    int _count = 0;

    private void Start()
    {
        float minScale = Mathf.Min(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        _txt.transform.localScale = new Vector3(_txt.transform.localScale.x/transform.lossyScale.x, _txt.transform.localScale.y/transform.lossyScale.y, _txt.transform.localScale.z/transform.lossyScale.z) * minScale;
    }

    public void SetTotal(int count)
    {
        _totalCount = count;
        _txt.text = _count.ToString() + "/" + _totalCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            _count++;
            _txt.text = _count.ToString() + "/" + _totalCount.ToString();
            if (_count == _totalCount)
            {
                GameplayManager.Instance.Win();
            }
        }
    }
}
