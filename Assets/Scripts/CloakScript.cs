using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloakScript : MonoBehaviour
{
    [SerializeField] public float cloakTimeRemaining = 10.0f;
    [SerializeField] private TMPro.TextMeshProUGUI m_text;
    [SerializeField] public GameObject Timer;
    [SerializeField] public PlayerMovement player;
    [SerializeField] private GameObject cloak;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] public GameObject cloackDisplay;
    [SerializeField] private GameObject pickUpText;
    // Start is called before the first frame update
    void Start()
    {
        m_text.text = "X";
        Timer.SetActive(false);
        cloak.SetActive(true);
        cloackDisplay.SetActive(false);
        pickUpText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.cloaked){
            if(cloakTimeRemaining < 1) {
                player.cloaked = false;
                Timer.SetActive(false);
                cloackDisplay.SetActive(false);
            }
            if(cloakTimeRemaining < 4) {
                m_text.color = Color.red;
            }
            if(cloakTimeRemaining > 0) {
                cloakTimeRemaining -= Time.deltaTime;
                m_text.text = cloakTimeRemaining.ToString("0");            
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {

            player.hasCloak = true;
            cloak.GetComponent<Collider2D>().enabled = false;
            sr.enabled = false;
            cloackDisplay.SetActive(true);
            Timer.SetActive(true);
            StartCoroutine(ActivateForSeconds(pickUpText, 3));
        }
    }

    IEnumerator ActivateForSeconds(GameObject obj, float duration)
        {
            obj.SetActive(true);  // Activate the GameObject
            yield return new WaitForSeconds(duration);  // Wait for 'duration' seconds
            obj.SetActive(false);  // Deactivate the GameObject
        }
    
}
