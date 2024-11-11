using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public static int candyCount = 0;
    public AudioClip audioSource;
    public int candiesToWin = 7;

    // public Text candyCountText;
    // Start is called before the first frame update
    void Start()
    {
        candyCount = 0;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        candyCount++;
        GameObject player = collision.GetComponent<GameObject>();
        Debug.Log("Candy Count:" + candyCount);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(audioSource, gameObject.transform.position, 1);
        // candyCountText.text = ("Candy Count: " + candyCount);
        if(candyCount >= candiesToWin)
        {
            Debug.Log("Player Wins!!!!");
        }
    }

}
