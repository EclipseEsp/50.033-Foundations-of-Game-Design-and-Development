using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public EdgeCollider2D edgeCollider;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    private bool broken = false;
    public GameObject prefab;
    public GameObject prefab2;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col){
        Debug.Log("Break!");
        if (col.gameObject.CompareTag("Player") && !broken){
            Debug.Log("Break!");
            broken = true;
            audioSource.Play();
            // assume we have 5 debris per box
            // prefab.SetActive(true);
            for (int x = 0; x < 5; x++ ){
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            Instantiate(prefab2, transform.position + new Vector3 (0,1,0), Quaternion.identity);
            // gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            // gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            spriteRenderer.enabled=false;
            boxCollider.enabled=false;
            edgeCollider.enabled=false;
            // GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
}
