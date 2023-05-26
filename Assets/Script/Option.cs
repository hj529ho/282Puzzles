using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{ 
   public Slider BGM;
   public Slider SFX;

   private void Start()
   {
      BGM.onValueChanged.AddListener(
         value =>
         {
            GameManager.instance._soundManager.ChangeVolume(value,Define.Sound.BGM);
         }
      );
      SFX.onValueChanged.AddListener(
         value =>
         {
            GameManager.instance._soundManager.ChangeVolume(value,Define.Sound.SFX);
         }
      );
   }
}
