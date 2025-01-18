using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    public Transform[] points;
    int current;
    public float speed;

    [SerializeField] public GameObject LossScreen;
    [SerializeField] public PlayerMovement player;
    public bool happen = false;

    [SerializeField] public GameObject Timer;
    [SerializeField] public GameObject Objective;
    public float horizontal = 0.0f;
    public float vertical = 0.0f;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject flashlight;

    void Start()
    {
        current = 0;
        LossScreen.SetActive(false);
            // Loop through each point in the points array
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[current].position) < 0.2f){
            current = (current + 1) % points.Length;            
        }
        horizontal = points[current].position.x - transform.position.x;
        vertical = points[current].position.y - transform.position.y;
        transform.position = Vector2.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        animator.SetFloat("speed", 1);  

        bool isHorizontal = vertical < 0.2 && vertical > -0.2f;
        bool isVertical = horizontal < 0.2 && horizontal > -0.2f;
        int flashlightAngle = 180;
        
        if(horizontal > 0.1f && isHorizontal){
            flashlightAngle = -90;
        }
        else if(horizontal < -0.1f && isHorizontal){
            flashlightAngle = 90;
        }
        else if(vertical > 0.1f && isVertical){
            flashlightAngle = 0;
        }

        flashlight.transform.rotation = Quaternion.Euler(0, 0, flashlightAngle);
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" && !player.cloaked){
            LossScreen.SetActive(true);
            Timer.SetActive(false);
            Objective.SetActive(false);
            Time.timeScale = 0;
            happen = true;
        }
    }

    public void OnAlert(Vector3 source)
    {
        StartCoroutine(InvestigateNoise(source));
    }

    IEnumerator InvestigateNoise(Vector3 source)
    {
        Vector3 originalPosition = transform.position;  // Save the original position
        float originalSpeed = speed;  // Save the original speed if needed

        // Move towards the noise source
        while (Vector2.Distance(transform.position, source) > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, source, speed * Time.deltaTime);
            UpdateAnimator(source);  // Update animator if needed
            yield return null;
        }

        // Wait at the source of the noise
        yield return new WaitForSeconds(5);

        // Return to original path
        speed = originalSpeed;  // Reset speed if it was changed
        while (Vector2.Distance(transform.position, originalPosition) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            UpdateAnimator(originalPosition);  // Update animator if needed
            yield return null;
        }

        // Resume normal patrol
        current = ClosestPointIndex();  // Find the closest point in the patrol route to resume
    }

    int ClosestPointIndex()
    {
        float minDistance = float.MaxValue;
        int index = 0;
        for (int i = 0; i < points.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, points[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }
        return index;
    }

    void UpdateAnimator(Vector3 target)
    {
        float h = target.x - transform.position.x;
        float v = target.y - transform.position.y;
        animator.SetFloat("horizontal", h);
        animator.SetFloat("vertical", v);
        animator.SetFloat("speed", 1);  // Assuming '1' is the speed value when moving

        // Update flashlight direction as needed
        UpdateFlashlightDirection(h, v);
    }

    void UpdateFlashlightDirection(float horizontal, float vertical)
    {
        bool isHorizontal = Mathf.Abs(vertical) < 0.2f;
        bool isVertical = Mathf.Abs(horizontal) < 0.2f;
        int flashlightAngle = 180;
        
        if(horizontal > 0.1f && isHorizontal){
            flashlightAngle = -90;
        }
        else if(horizontal < -0.1f && isHorizontal){
            flashlightAngle = 90;
        }
        else if(vertical > 0.1f && isVertical){
            flashlightAngle = 0;
        }

        flashlight.transform.rotation = Quaternion.Euler(0, 0, flashlightAngle);
    }

}
