using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour
{
    public int numLevel = 1;
    private bool showPrompt = false;

    [SerializeField] TMPro.TextMeshProUGUI textPrompt;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject levelTwoTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        if(!MainManager.Instance.levelOneComplete){
            levelTwoTrigger.SetActive(false);
        } else {
            levelTwoTrigger.SetActive(true);
        }
        showPrompt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(showPrompt && Input.GetKey("space")){
            SceneManager.LoadScene(numLevel+2);
        }
    }

    void OnTriggerEnter2D(Collider2D c) {

        if(numLevel == 1){
            textPrompt.text = "Press space to enter Level 1: The Holy Grail";
        } else if(numLevel == 2){
            textPrompt.text = "Press space to enter Level 2: Mona Lisa";
        }
        panel.SetActive(true);
        showPrompt = true;
    }

    void OnTriggerExit2D(Collider2D c) {
        if(panel != null){
            panel.SetActive(false);
        }
        showPrompt = false;
    }
}
