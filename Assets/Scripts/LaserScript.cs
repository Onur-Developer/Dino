using UnityEngine;


public class LaserScript : MonoBehaviour
{
    private PlayerScript pl;

    private void Awake()
    {
        pl = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D col2)
    {
        if (col2.gameObject.CompareTag("Enemy"))
        {
            pl.score++;
            Destroy(col2.gameObject);
        }

        if (col2.gameObject.CompareTag("Healer"))
        {
            if(pl.heart<100 && pl.heart>=90)
            {
                pl.heart = 100;
            }
            else
            {
                pl.heart += 10;
            }
            Destroy(col2.gameObject);
        }

        if (col2.gameObject.CompareTag("Mushroom"))
        {
            pl.score +=5;
            Destroy(col2.gameObject);
        }
    }
}