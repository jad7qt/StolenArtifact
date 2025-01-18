using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactScript : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public PlayerMovement script;
    [SerializeField] public GameObject artifact;
    [SerializeField] public GameObject arrow;
    [SerializeField] public GameObject doorArrow;
    [SerializeField] public TMPro.TextMeshProUGUI message;

    // Start is called before the first frame update
    void Start()
    {
        script = player.GetComponent<PlayerMovement>();
        player = GameObject.FindWithTag("Player");
        doorArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player"){
            script.hasArtifact = true;
            artifact.SetActive(false);
            arrow.SetActive(false);
            doorArrow.SetActive(true);
            message.text = "ESCAPE THE MUSEUM";
        }
    }
}
