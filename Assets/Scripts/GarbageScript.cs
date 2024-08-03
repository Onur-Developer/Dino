using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageScript : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Enemy"))
    {
      Destroy(col.gameObject);
    }

    if (col.gameObject.CompareTag("Healer"))
    {
      Destroy(col.gameObject);
    }

    if (col.gameObject.CompareTag("Mushroom"))
    {
      Destroy(col.gameObject);
    }
  }
}
