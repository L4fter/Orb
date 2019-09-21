using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnableIfButtonSelected : MonoBehaviour
{
   public Button button;
   private TMP_Text text;

   private void Awake()
   {
      text = GetComponent<TMP_Text>();
   }

   private void Update()
   {
      if (button.gameObject == EventSystem.current.currentSelectedGameObject &&
          !this.text.enabled)
      {
         this.text.enabled = true;
      }
      
      if (button.gameObject != EventSystem.current.currentSelectedGameObject &&
          this.text.enabled)
      {
         this.text.enabled = false;
      }
   }
}
