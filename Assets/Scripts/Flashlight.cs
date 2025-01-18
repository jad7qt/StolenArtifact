using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] public GameObject LossScreen;
    [SerializeField] public PlayerMovement player;
    public bool happen = false;

    [SerializeField] public GameObject Timer;
    [SerializeField] public GameObject Objective;

    void OnTriggerEnter2D(Collider2D c){
        if(c.gameObject.tag == "Player" && !player.cloaked){
            LossScreen.SetActive(true);
            Timer.SetActive(false);
            Objective.SetActive(false);
            Time.timeScale = 0;
            happen = true;
        }
    }
}
