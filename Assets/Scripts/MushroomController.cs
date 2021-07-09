using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D mushroom;
    private Vector2 currentDirection;
    private bool directionRight = true;
    // Start is called before the first frame update
    void Start()
    {
        var random = Random.Range(-1f,1f);
        if (random > 0){
            directionRight = true;
            currentDirection = new Vector2(1,0);
        }
        else {
            directionRight = false;
            currentDirection = new Vector2(-1,0);
        }
        mushroom = GetComponent<Rigidbody2D>();
        mushroom.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Pipe"))
        {
            if (directionRight == true){
                directionRight = false;
                currentDirection = new Vector2(-1,0);
            }
            else {
                directionRight = true;
                currentDirection = new Vector2(1,0);
            }
        }

        if(col.gameObject.CompareTag("Player"))
        {
            speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = mushroom.position;
        Vector2 nextPosition = currentPosition + speed * 
        currentDirection.normalized * Time.fixedDeltaTime;
        
        mushroom.MovePosition(nextPosition);
    }

    void OnBecameInvisible(){
        Destroy(gameObject);
    }
}
