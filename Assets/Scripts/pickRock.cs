using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickRock : MonoBehaviour
{
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public LayerMask guardMask;
    public Vector3 Direction { get; set; }
    private GameObject itemHolding;

    
    [SerializeField] private GameObject RockDisplay;
    [SerializeField] private GameObject cantPickUpText;
    [SerializeField] private GameObject pickUpText;

    [SerializeField] private float alertRadius = 50f; 

    void Update()
    {
        if (Input.GetKeyDown("c") && itemHolding)
        {
                itemHolding.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(ThrowItem(itemHolding));
                itemHolding = null;  
        }
        Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
        if (pickUpItem)
        {
            RockDisplay.SetActive(true);
            itemHolding = pickUpItem.gameObject;
            itemHolding.GetComponent<SpriteRenderer>().enabled = false;
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;
            if (itemHolding.GetComponent<Rigidbody2D>())
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
        }
            
    }

    IEnumerator ThrowItem(GameObject item)
    {
        RockDisplay.SetActive(false);
        Vector3 startPoint = item.transform.position;
        Vector3 endPoint = transform.position + Direction * 5;
        item.transform.parent = null;
        for (int i = 0; i < 50; i++)
        {
            item.transform.position = Vector3.Lerp(startPoint, endPoint, i * .02f);
            yield return null;
        }
        if (item.GetComponent<Rigidbody2D>())
            item.GetComponent<Rigidbody2D>().simulated = true;
        
        item.layer = LayerMask.NameToLayer("discarded");
        AlertGuards(item.transform.position);
    }

    void AlertGuards(Vector3 position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, alertRadius, guardMask);
        foreach (Collider2D hit in hits)
        {
            GuardMovement guard = hit.GetComponent<GuardMovement>();
            if (guard != null)
            {
                guard.OnAlert(position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        Debug.Log("Collided with layer: " + layerName);
        if (layerName == "discarded")
        {
            StartCoroutine(ActivateForSeconds(cantPickUpText, 3));
        } 

        if (layerName == "firstContact")
        {
            StartCoroutine(ActivateForSeconds(pickUpText, 3));
            //change layer name to "pickup" here
            collision.gameObject.layer = LayerMask.NameToLayer("pickup");
        }   
    }

    IEnumerator ActivateForSeconds(GameObject obj, float duration)
        {
            obj.SetActive(true);  // Activate the GameObject
            yield return new WaitForSeconds(duration);  // Wait for 'duration' seconds
            obj.SetActive(false);  // Deactivate the GameObject
        }
    
}