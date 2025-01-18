using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LaserScript : MonoBehaviour
{
    [SerializeField] private GameObject laserVert;
    [SerializeField] private GameObject laserHor;
    [SerializeField] private GameObject laserDeactive;
    [SerializeField] public GameObject LossScreen;
    [SerializeField] public GameObject Timer;
    [SerializeField] public GameObject Objective;
    public bool happen = false;

    // Start is called before the first frame update
    void Start()
    {
        laserVert.SetActive(true);
        laserHor.SetActive(true);
        laserDeactive.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){
            
            Debug.Log("Touched laser");
            LossScreen.SetActive(true);
            Timer.SetActive(false);
            Objective.SetActive(false);
            Time.timeScale = 0;
            happen = true;
        }
    }
}
