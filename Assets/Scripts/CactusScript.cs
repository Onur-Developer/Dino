using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusScript : MonoBehaviour
{
     float speed;

    void Start()
    {
        speed = 5;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation=Quaternion.Euler(0,180,0);
    }
}