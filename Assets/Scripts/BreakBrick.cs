using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    private bool broken = false;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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
            // assume we have 5 debris per box
            // prefab.SetActive(true);
            for (int x = 0; x < 5; x++ ){
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            // gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            // gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            spriteRenderer.enabled=false;
            boxCollider.enabled=false;
            // GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
}
