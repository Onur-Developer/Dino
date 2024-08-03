using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;



public class BossScript : MonoBehaviour
{
    private PlayerScript pl;
    public int a;
    public float bossHeart, ismove, xPosition, xRotate;
    private GameObject bosslaser, bossHeartBarobject;
    public bool isboss, isattack, isrotate;
    public float speed = 100;
    private Slider bossHeartBar;

    private void Awake()
    {
        pl = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        bossHeartBarobject = GameObject.Find("BossHeartBar");
        bossHeartBar = GameObject.Find("BossHeartBar").GetComponent<Slider>();
        bosslaser = GameObject.Find("BossLaserParticle");
    }

    private void Start()
    {
        xRotate = -1;
        bossHeart = 1500;
        bossHeartBar.value = bossHeart;
        bosslaser.SetActive(false);
        bossHeartBarobject.SetActive(false);
    }

    private void Update()
    {
        bossHeartBar.value = bossHeart;

        if (bossHeart <= 0)
        {
            pl.score += 200;
            PlayerPrefs.SetInt("Skor", pl.score);
            SceneManager.LoadScene(2);
        }

        if (isboss && transform.position.x > 6.1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            isattack = true;
            bossHeartBarobject.SetActive(true);
        }

        if (transform.position.x < 6.1)
        {
            if (isattack)
            {
                StartCoroutine("bossattack");
            }
        }

        if (ismove == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            xPosition = Mathf.Clamp(transform.position.x, -6.1f, 6);
            transform.position = new Vector2(xPosition, transform.position.y);
        }
        else if (ismove == 2)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            xPosition = Mathf.Clamp(transform.position.x, -6.2f, 6);
            transform.position = new Vector2(xPosition, transform.position.y);
        }

        if (isrotate)
        {
            if (xRotate == -1)
            {
                xRotate *= -1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                bosslaser.transform.rotation = Quaternion.Euler(0, 0, 270);
                isrotate = false;
            }
            else if (xRotate == 1)
            {
                xRotate *= -1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                bosslaser.transform.rotation = Quaternion.Euler(0, 0, 90);
                isrotate = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLaser"))
        {
            if (!pl.isdino)
            {
                bossHeart--;
            }
            else
            {
                bossHeart = bossHeart - 2;
            }
        }
    }

    IEnumerator bossattack()
    {
        isboss = false;
        isattack = false;
        a = Random.Range(0, 3);
        yield return new WaitForSeconds(3);
        switch (a)
        {
            case 0:
                bosslaser.SetActive(true);
                break;
            case 1:
                ismove = 1;
                break;
            case 2:
                ismove = 2;
                isrotate = true;
                break;
        }

        yield return new WaitForSeconds(3);
        switch (a)
        {
            case 0:
                bosslaser.SetActive(false);
                break;
            case 1:
                ismove = 2;
                break;
            case 2:
                break;
        }

        StartCoroutine("bossattack");
    }
}