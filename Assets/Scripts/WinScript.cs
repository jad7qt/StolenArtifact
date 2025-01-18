using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] public GameObject WinScreen;
    [SerializeField] public PlayerMovement player;
    public bool happen = false;

    [SerializeField] public GameObject Timer;
    [SerializeField] public GameObject Objective;
    [SerializeField] public CountdownScript timerScript;
    [SerializeField] public TMPro.TextMeshProUGUI message;

    // Start is called before the first frame update
    void Start()
    {
        WinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && player.hasArtifact){
            //Debug.Log("Level completed");
            message.text = "You stole the artifact in " + (timerScript.fullTime - timerScript.timeRemaining).ToString("0.00") + " seconds"; 
            WinScreen.SetActive(true);
            Timer.SetActive(false);
            Objective.SetActive(false);
            Time.timeScale = 0;
            happen = true;
            if(MainManager.Instance != null){
                MainManager.Instance.levelOneComplete = true;
            }
        }
    }
}
