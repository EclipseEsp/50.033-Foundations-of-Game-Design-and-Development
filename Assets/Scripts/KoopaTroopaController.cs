using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaTroopaController : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = 1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private Animator koopaTroopaAnimator;
    private SpriteRenderer koopaTroopaSprite;
    private bool faceRightState = true;

    private GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        enemyBody = GetComponent<Rigidbody2D>();
        koopaTroopaSprite = GetComponent<SpriteRenderer>();
        koopaTroopaAnimator = GetComponent<Animator>();
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*maxOffset / enemyPatroltime, 0);
    }
    void MoveKoopaTroopa(){
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move gomba
            MoveKoopaTroopa();
            koopaTroopaAnimator.SetFloat("xSpeed",Mathf.Abs(enemyBody.velocity.x));
        }
        else{// change direction
            moveRight *= -1;
            if (faceRightState) 
            {
                koopaTroopaSprite.flipX = true;
                faceRightState = !faceRightState;
            }
            else
            {
                koopaTroopaSprite.flipX = false;
                faceRightState = !faceRightState;
            }
            ComputeVelocity();
            MoveKoopaTroopa();
            koopaTroopaAnimator.SetFloat("xSpeed",Mathf.Abs(enemyBody.velocity.x));
        }
    }

	void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with Mario
        Debug.Log("Collided with KoopaTroopa");
		if (other.gameObject.tag  ==  "Player"){
			// check if collides on top
			float yoffset = (other.transform.position.y  -  this.transform.position.y);
			if (yoffset  >  0.75f){
				KillSelf();
			}
			else{
				// hurt player, implement later
			}
		}
	}

	void  KillSelf(){
		// enemy dies
		CentralManager.centralManagerInstance.increaseScore();
		StartCoroutine(flatten());
		Debug.Log("Kill sequence ends");
	}

	IEnumerator  flatten(){
		Debug.Log("Flatten starts");
		int steps =  5;
		float stepper =  1.0f/(float) steps;

		for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		yield  break;
	}

}
