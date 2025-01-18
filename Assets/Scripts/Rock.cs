using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private GameObject RockDisplay;
    [SerializeField] private GameObject pickUpText;
    [SerializeField] private GameObject cantPickUpText;
    [SerializeField] private float throwForce = 5f;
    private Rigidbody2D rockRigidbody;
    private GameObject player;
    private bool pickUpAllowed = false;
    private bool isPickedUp = false;
    private bool beenPicked = false;
    // Start is called before the first frame update
    void Start()
    {
        pickUpText.gameObject.SetActive(false);
        cantPickUpText.gameObject.SetActive(false);
        RockDisplay.gameObject.SetActive(false);
        rockRigidbody = GetComponent<Rigidbody2D>();
        rockRigidbody.isKinematic = true;
        player = GameObject.Find("PlayerObject");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isPickedUp){
            if(Input.GetKeyDown("c") && isPickedUp)
            {
                Debug.Log("gameObject exists: "+(gameObject != null));
                StartCoroutine(ThrowItem());
            }
            else
            {
                // Makes the rock follow the player position
                transform.position = player.transform.position;
            }
        }
        else{
            if(pickUpAllowed){
                pickUpAllowed = false;
                beenPicked = true;
                isPickedUp = true;

                rockRigidbody.isKinematic = true;
                rockRigidbody.gravityScale = 0; 
                rockRigidbody.velocity = Vector2.zero;

                RockDisplay.gameObject.SetActive(true);
                pickUpText.gameObject.SetActive(false);
                
            }
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && beenPicked == false)
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }

        if(collision.tag == "Player" && beenPicked == true){
            if(!isPickedUp){
                cantPickUpText.gameObject.SetActive(true);
            }
        }        
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cantPickUpText.gameObject.SetActive(false);
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void ThrowRock()
    {
        isPickedUp = false;
        RockDisplay.gameObject.SetActive(false);
        rockRigidbody.isKinematic = false;
        rockRigidbody.AddForce( player.transform.right * throwForce, ForceMode2D.Impulse);
        rockRigidbody.gravityScale = 1; // Ensure gravity affects the rock when thrown
        rockRigidbody.AddForce(player.transform.right * throwForce + player.transform.up * (throwForce * 0.5f), ForceMode2D.Impulse);
    }

    private IEnumerator ThrowItem()
    {
        Debug.Log("we're throwing");
        isPickedUp = false;
        RockDisplay.gameObject.SetActive(false);
        Vector3 startPoint = transform.position;
        Vector3 throwDirection = new Vector3(player.transform.localScale.x, 0.5f,1).normalized;
        

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            throwDirection = Vector2.left;  // Player facing left
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            throwDirection = Vector2.right;  // Player facing right
        }

        Vector3 endPoint = player.transform.position + throwDirection * 2;

        for (int i = 0; i < 25; i++)
        {
            transform.position = Vector3.Lerp(startPoint, endPoint, i * .04f);
            yield return null;
        }
        
        rockRigidbody.simulated = true;
    }
}
