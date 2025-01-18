using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject laserVert;
    [SerializeField] private GameObject laserHor;
    [SerializeField] private GameObject laserDeactive;
    [SerializeField] private GameObject laserLight;
    [SerializeField] private GameObject buttonPrompt;
    [SerializeField] private TMPro.TextMeshProUGUI ObjectiveText;
    [SerializeField] private GameObject arrow; 
    private bool inTrigger = false;
    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        buttonPrompt.SetActive(false);
        arrow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger)
        {
            if(Input.GetKeyDown("space")){
                laserHor.SetActive(false);
                laserVert.SetActive(false);
                laserDeactive.SetActive(true);
                laserLight.SetActive(false);
                isPressed = true;
                buttonPrompt.SetActive(false);
                arrow.SetActive(false);
                ObjectiveText.text = "FIND AND COLLECT THE MONA LISA";
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            if(!isPressed){
                buttonPrompt.SetActive(true);
            }
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            buttonPrompt.SetActive(false);
            inTrigger = false;
        }
    }
}
