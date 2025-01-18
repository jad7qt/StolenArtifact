using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    [SerializeField] public GameObject tutorial;
    [SerializeField] public GameObject timer;
    [SerializeField] public GameObject objective;

    public bool isFirstLevel;
    
    void Start(){
        if(isFirstLevel){
            tutorial.SetActive(true);
            timer.SetActive(false);
            objective.SetActive(false);
            Time.timeScale = 0;
        }
        else {
            tutorial.SetActive(false);
            timer.SetActive(true);
            objective.SetActive(true);
            Time.timeScale = 1;
        }
    }

    void Update(){
        if(Input.GetKeyDown("space") && isFirstLevel){
            HideTutorial();
        }
    }

    public void HideTutorial(){
        tutorial.SetActive(false);
        timer.SetActive(true);
        objective.SetActive(true);
        Time.timeScale = 1;
    }
    public void BackToHub(){
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Retry(){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
