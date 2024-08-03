using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossLaserScript : MonoBehaviour
{
   private PlayerScript pl;
   private GameObject bossLaserPosition;

   private void Awake()
   {
      pl = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
      bossLaserPosition = GameObject.Find("BossLaserPosition");
   }

   private void Update()
   {
      transform.position = bossLaserPosition.transform.position;
   }

   private void OnTriggerStay2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         pl.heart--;
      }
   }
}
