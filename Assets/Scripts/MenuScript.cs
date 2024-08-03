using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    RectTransform rc;
    GameObject panel;
    private int i, control;
    private Button forwardbutton, backbutton;

    private void Awake()
    {
        i = 0;
        panel = GameObject.Find("HowToPlayPanel");
        backbutton = GameObject.Find("BackButton").GetComponent<Button>();
        forwardbutton = GameObject.Find("ForwardButton").GetComponent<Button>();
    }

    private void Start()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    void panelanimation()
    {
        panel.transform.GetChild(i).transform.localScale += new Vector3(0.1f, 0.1f);
        control++;
        if (control >= 8)
        {
            CancelInvoke("panelanimation");
            control = 0;
            backbutton.interactable = true;
            forwardbutton.interactable = true;
        }
    }

    public void forward()
    {
        forwardbutton.interactable = false;
        panel.transform.GetChild(i).transform.localScale = new Vector3(0, 0, 1);
        i++;
        rc = panel.transform.GetChild(i).GetComponent<RectTransform>();
        rc.pivot = new Vector2(1f, 0f);
        rc.offsetMin = new Vector2(-106.6667f, 60);
        rc.offsetMax = new Vector2(-106.6667f, 60);
        if (i == 6)
        {
            forwardbutton.transform.localScale = new Vector2(0, 0);
        }
        else
        {
            forwardbutton.transform.localScale = new Vector2(1, 1);
            backbutton.transform.localScale = new Vector2(1, 1);
        }

        InvokeRepeating("panelanimation", 0.05f, 0.05f);
    }

    public void back()
    {
        backbutton.interactable = false;
        panel.transform.GetChild(i).transform.localScale = new Vector3(0, 0, 1);
        i--;
        rc = panel.transform.GetChild(i).GetComponent<RectTransform>();
        rc.pivot = new Vector2(0f, 0f);
        rc.offsetMin = new Vector2(106.6667f, 60);
        rc.offsetMax = new Vector2(106.6667f, 60);

        if (i == 0)
        {
            backbutton.transform.localScale = new Vector2(0, 0);
        }
        else
        {
            backbutton.transform.localScale = new Vector2(1, 1);
            forwardbutton.transform.localScale = new Vector2(1, 1);
        }

        InvokeRepeating("panelanimation", 0.05f, 0.05f);
    }

    public void okfunction()
    {
        panel.transform.GetChild(i).transform.localScale = new Vector3(0, 0);
        panel.SetActive(false);
    }

    public void playfunction()
    {
        SceneManager.LoadScene(1);
    }

    public void howtoplayfunction()
    {
        panel.SetActive(true);
        backbutton.transform.localScale = new Vector2(0, 0);
        forwardbutton.transform.localScale = new Vector2(1, 1);
        i = 0;
        panel.transform.GetChild(0).transform.localScale=new Vector2(0.8f, 0.8f);
    }

    public void exitfunction()
    {
        Application.Quit();
    }
}