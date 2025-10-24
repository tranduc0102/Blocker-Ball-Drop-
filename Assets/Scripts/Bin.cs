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

   public void SetTotal(int count)
   {
      _totalCount = count;
      _txt.text = _count.ToString() + "/"  + _totalCount.ToString();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Ball")
      {
         _count++;
         _txt.text = _count.ToString() + "/"  + _totalCount.ToString();
         if (_count == _totalCount)
         {
            GameplayManager.Instance.Win();
            other.gameObject.SetActive(false);
         }
      }
   }
}
