
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Button yesbutton, nobutton,continuebutton,mainmenubutton,exitbutton,pausebutton;
    private BossScript bs;
    private Text scoretx;
    private ParticleSystem laserparticle;
    private ParticleSystem.MainModule ma;
    public Sprite flydino, dino;
    private Slider heartBar, fireBar;
    private GameObject laser, spawner, spawner2, deathpanel,pausepanel;
    private Image backgroundımage;
    private SpriteRenderer dinosprite;
    public int heart, score, control;
    Rigidbody2D rb;
    public float jumpforce, speed, fire, isdinocooldown, xPosition, yPosition;
    public bool isjump, isdino, contolb;
    private Vector3 velocity;
    public Transform laserDino, laserFly;

    private void Awake()
    {
        pausebutton = GameObject.Find("PauseButton").GetComponent<Button>();
        pausepanel = GameObject.Find("PausePanel");
        continuebutton = GameObject.Find("ContinueButton").GetComponent<Button>();
        mainmenubutton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        exitbutton = GameObject.Find("ExitButton").GetComponent<Button>();
        yesbutton = GameObject.Find("YesButton").GetComponent<Button>();
        nobutton = GameObject.Find("NoButton").GetComponent<Button>();
        deathpanel = GameObject.Find("DeathPanel");
        backgroundımage = GameObject.Find("BackgroundPanel").GetComponent<Image>();
        bs = GameObject.FindWithTag("Boss").GetComponent<BossScript>();
        spawner = GameObject.Find("Spawner");
        spawner2 = GameObject.Find("Spawner (1)");
        scoretx = GameObject.Find("ScoreText").GetComponent<Text>();
        fireBar = GameObject.Find("FireBar").GetComponent<Slider>();
        heartBar = GameObject.Find("HeartBar").GetComponent<Slider>();
        speed = 3;
        laser = GameObject.Find("LaserParticle");
        laserparticle = laser.GetComponent<ParticleSystem>();
        ma = laserparticle.main;
        dinosprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Time.timeScale = 1;
        deathpanel.SetActive(false);
        contolb = true;
        continuebutton.onClick.AddListener(devamet);
        mainmenubutton.onClick.AddListener(anamenu);
        exitbutton.onClick.AddListener(cıkıs);
        pausebutton.onClick.AddListener(durdur);
        yesbutton.onClick.AddListener(yesbuttonfonc);
        nobutton.onClick.AddListener(nobuttonfonc);
        Invoke("Bosscoming", 60);
        laser.SetActive(false);
        heart = 100;
        fire = 100;
        fireBar.value = fire;
        isjump = false;
        isdino = true;
        isdinocooldown = 10;
        pausepanel.SetActive(false);
    }

    void Bosscoming()
    {
        spawner.SetActive(false);
        spawner2.SetActive(false);
        bs.isboss = true;
    }

    void panelanimation()
    {
        deathpanel.SetActive(true);
        backgroundımage.color = new Color32(0, 0, 0, 100);
        if (score >= 30)
        {
            yesbutton.interactable = true;
        }
        else
        {
            yesbutton.interactable = false;
        }

        Time.timeScale = 0;
    }

    void yesbuttonfonc()
    {
        heart = 100;
        backgroundımage.color = new Color32(0, 0, 0, 0);
        contolb = true;
        score -= 30;
        Time.timeScale = 1;
        deathpanel.SetActive(false);
    }

    void nobuttonfonc()
    {
        PlayerPrefs.SetInt("Skor",score);
        SceneManager.LoadScene(2);
    }

    void durdur()
    {
        pausebutton.interactable = false;
        pausepanel.SetActive(true);
        Time.timeScale = 0;
    }

    void devamet()
    {
        pausebutton.interactable = true;
        pausepanel.SetActive(false);
        Time.timeScale = 1;
    }

    void anamenu()
    {
        SceneManager.LoadScene(0);
    }

    void cıkıs()
    {
        Application.Quit();
    }

    void Update()
    {
        heart = Mathf.Clamp(heart, 0, 100);
        scoretx.text = score.ToString();
        xPosition = Mathf.Clamp(transform.position.x, -8.14f, 8.22f);
        yPosition = Mathf.Clamp(transform.position.y, -2.7f, 4.5f);
        transform.position = new Vector2(xPosition, yPosition);
        heartBar.value = heart;
        fireBar.value = fire;
        if (Input.GetKey(KeyCode.Space))
        {
            if (fire > 0)
            {
                fire -= 20f*Time.deltaTime;
                fireBar.transform.GetChild(1).gameObject.SetActive(true);
                laser.SetActive(true);

                if (isdino)
                    laser.transform.position = laserDino.position;
                else
                    laser.transform.position = laserFly.position;
            }
            else
            {
                laser.SetActive(false);
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            laser.SetActive(false);
        }
        if (fire < 100 && !Input.GetKey(KeyCode.Space))
        {
            fire += 8 * Time.deltaTime;
        }

        if (heart == 0 && contolb)
        {
            panelanimation();
            contolb = false;
        }

        /* if (!isdino)
         {
             isdinocooldown -= Time.deltaTime;
             dinosprite.sprite = flydino;
             transform.GetChild(0).transform.position = transform.GetChild(2).transform.position;
             if (isdinocooldown <= 0)
             {
                 isdino = true;
                 isdinocooldown = 10;
                 rb.gravityScale = 1;
                 dinosprite.sprite = dino;
                 transform.GetChild(0).transform.position = transform.GetChild(1).transform.position;
             }
         } */
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isdino)
            {
                rb.velocity = Vector2.zero;
                isjump = false;
                dinosprite.sprite = flydino;
                transform.GetChild(0).transform.position = transform.GetChild(2).transform.position;
                ma.startColor = Color.cyan;
                rb.gravityScale = 0;
                isdino = false;
                dinosprite.flipX = !dinosprite.flipX;

                if (dinosprite.flipX)
                {
                    laser.transform.rotation = Quaternion.Euler(0, 0, -90);
                    laserFly.localPosition = new Vector2(1.43f,-0.7f);
                }
                else
                {
                    laser.transform.rotation = Quaternion.Euler(0, 0, 90);
                    laserFly.localPosition = new Vector2(-1.43f,-0.7f);
                }
            }
            else
            {
                rb.gravityScale = 1;
                dinosprite.sprite = dino;
                transform.GetChild(0).transform.position = transform.GetChild(1).transform.position;
                ma.startColor = Color.yellow;
                isdino = true;
                dinosprite.flipX = !dinosprite.flipX;
                if (!dinosprite.flipX)
                {
                    laser.transform.rotation = Quaternion.Euler(0, 0, -90);
                    laserDino.localPosition = new Vector2(1.05f, 1.05f);
                }
                else
                {
                    laser.transform.rotation = Quaternion.Euler(0, 0, 90);
                    laserDino.localPosition = new Vector2(-1.05f, 1.05f);
                }
            }
        }

        if (!isdino)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        }

        transform.position += velocity * speed * Time.deltaTime;
        

        if (Input.GetKeyDown(KeyCode.W) && isjump)
        {
            rb.velocity = Vector2.up * jumpforce;
            isjump = false;
        }

        if (velocity.x!=0 && isdino)
        {
            if (!dinosprite.flipX && velocity.x < 0)
            {
                laser.transform.rotation = Quaternion.Euler(0, 0, 90);
                dinosprite.flipX = true;
                laserDino.localPosition = new Vector2(-1.05f, 1.05f);
            }
            else if (dinosprite.flipX && velocity.x > 0)
            {
                laser.transform.rotation = Quaternion.Euler(0, 0, -90);
                dinosprite.flipX = false;
                laserDino.localPosition = new Vector2(1.05f, 1.05f);
            }
        }
        else
        {
            if (dinosprite.flipX && velocity.x < 0)
            {
                laser.transform.rotation = Quaternion.Euler(0, 0, 90);
                dinosprite.flipX = false;
                laserFly.localPosition = new Vector2(-1.43f,-0.7f);
            }
            else if (!dinosprite.flipX && velocity.x > 0)
            {
                laser.transform.rotation = Quaternion.Euler(0, 0, -90);
                dinosprite.flipX = true;
                laserFly.localPosition = new Vector2(1.43f,-0.7f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            if (!isdino)
                isjump = false;
            else
            {
                isjump = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (heart > 0)
            {
                heart -= 10;
                heartBar.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                heartBar.transform.GetChild(1).gameObject.SetActive(false);
            }

            StartCoroutine(SpriteColorChange());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            heart--;
            StartCoroutine(SpriteColorChange());
        }
    }

    IEnumerator SpriteColorChange()
    {
        dinosprite.color = Color.red;
        yield return new WaitForSecondsRealtime(1);
        dinosprite.color = Color.white;
    }
}