using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{

    private bool onScreen = true;
    [SerializeField] GameObject SpaceToBeginText;
    // Start is called before the first frame update
    void Start()
    {
        SpaceToBeginText.SetActive(false);
        StartCoroutine(StoryCrawl());
    }

    // Update is called once per frame
    void Update()
    {

        if(onScreen){
            transform.position = new Vector2(transform.position.x, transform.position.y + 1.0f * Time.deltaTime);
        }
        else {
            if(Input.GetKeyDown("space")){
                SceneManager.LoadScene(2);
            }
        }
    }

    IEnumerator StoryCrawl(){
        yield return new WaitForSeconds(30);
        onScreen = false;
        SpaceToBeginText.SetActive(true);
    }
}
