using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _colid;
    [SerializeField] private bool _isBlock;
    public void ShowColid(bool enable)
    {
        if (_isBlock)
        {
            _colid.SetActive(true);
            return;
        }
        _colid.SetActive(enable);
    }
}
