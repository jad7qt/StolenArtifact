using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 2.0f;
    public bool hasArtifact = false;
    public bool hasCloak = false;
    public bool cloaked = false;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private Vector2 movement;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] public GameObject CloakTimer;

    private pickRock pickUp;
    private Vector3 change;



    // Start is called before the first frame update
    void Start()
    {
        horizontal = 0.0f;
        vertical = 0.0f;
        movement.x = horizontal;
        movement.y = horizontal;
        pickUp = gameObject.GetComponent<pickRock>();
        pickUp.Direction = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        movement.x = horizontal;
        movement.y = vertical;
        
        change = Vector3.zero;
        change.x = horizontal;
        change.y = vertical;
        if(change.sqrMagnitude > .1f){
            pickUp.Direction = change.normalized;
        }

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        if(Input.GetKeyDown(KeyCode.X) && hasCloak) {
            hasCloak = false;
            cloaked = true;
            CloakTimer.SetActive(true);
            sr.color = new Color(1f, 1f, 1f, 0.5f);
        }
        if(!cloaked) {
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * playerMoveSpeed * Time.fixedDeltaTime);
    }
}
