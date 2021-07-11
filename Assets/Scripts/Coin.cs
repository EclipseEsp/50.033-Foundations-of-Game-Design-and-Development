using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameConstants gameConstants;
    private Rigidbody2D rigidbody;
    private float originalY;
    private Vector2 velocity;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        originalY = transform.position.y;
        // ComputeVelocity();
        StartCoroutine("PopOut");
        
    }

    void ComputeVelocity()
    {
        velocity = new Vector2(0, 1 * gameConstants.maxOffset / gameConstants.enemyPatroltime);
    }

    IEnumerator PopOut(){
        Vector2 direction = new Vector2(0,5);
        rigidbody.AddForce(direction.normalized, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        // ComputeVelocity();
        // rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Mathf.Abs(rigidbody.position.y - originalY) < gameConstants.maxOffset)
        // {
        //     rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        // }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Debug.Log("Coin collected! score + 1");
            audioSource.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            CentralManager.centralManagerInstance.increaseScore();
            CentralManager.centralManagerInstance.damageEnemy();
           
            Destroy(gameObject, audioSource.clip.length - 0.3f);
            
        }
    }
}
