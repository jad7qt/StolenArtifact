using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] public GameObject LossScreen;
    [SerializeField] private TMPro.TextMeshProUGUI m_MyText;
    [SerializeField] public float timeRemaining = 60.0f;
    public float fullTime;
    [SerializeField] public GameObject Timer;
    [SerializeField] public GameObject Objective;

    // Start is called before the first frame update
    void Start()
    {
        fullTime = timeRemaining;
        m_MyText.text = timeRemaining.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {   
        if(timeRemaining < 1){
            LossScreen.SetActive(true);
            Timer.SetActive(false);
            Objective.SetActive(false);
            Time.timeScale = 0;
        }

        if(timeRemaining < 10){
            m_MyText.color = Color.red;
        }
        if (timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            m_MyText.text = timeRemaining.ToString("0");
        }
    }
}
