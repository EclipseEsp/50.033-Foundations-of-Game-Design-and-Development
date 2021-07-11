using System.Collections;
using UnityEngine;

public  class EnemyController2 : MonoBehaviour
{
	public  GameConstants gameConstants;
	private  int moveRight;
	private  float originalX;
	private  Vector2 velocity;
	private  Rigidbody2D enemyBody;
	private  SpriteRenderer spriteRenderer;
	private Animator animator;
	// public static EnemyController2 enemyController2;
	
	void  Start()
	{
		enemyBody  =  GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		
		// get the starting position
		originalX  =  transform.position.x;
	
		// randomise initial direction
		moveRight  =  Random.Range(0, 2) ==  0  ?  -1  :  1;
		
		// compute initial velocity
		ComputeVelocity();

		GameManager.OnPlayerDeath += EnemyRejoice; // Subscribe to event in GameManager
	}
	
	void  ComputeVelocity()
	{
			velocity  =  new  Vector2((moveRight) *  gameConstants.maxOffset  /  gameConstants.enemyPatroltime, 0);
	}
  
	void  MoveEnemy()
	{
		enemyBody.MovePosition(enemyBody.position  +  velocity  *  Time.fixedDeltaTime);
	}

	void  Update()
	{
		if (Mathf.Abs(enemyBody.position.x  -  originalX) <  gameConstants.maxOffset)
		{// move gomba
			MoveEnemy();
		}
		else
		{
			// change direction
			moveRight  *=  -1;
			ComputeVelocity();
			MoveEnemy();
		}
	}

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with Mario
		if (other.gameObject.tag  ==  "Player"){
			// check if collides on top
			float yoffset = (other.transform.position.y  -  this.transform.position.y);
			if (yoffset  >  0.75f){
				KillSelf();
				// GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(Object)
				CentralManager.centralManagerInstance.increaseScore();
				CentralManager.centralManagerInstance.damageEnemy();
			}
			else{
				// hurt player, implement later
				EnemyRejoice();
                CentralManager.centralManagerInstance.damagePlayer();
			}
		}
	}

    void  KillSelf(){
		// enemy dies
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
    // animation when player is dead
    public void EnemyRejoice(){
        Debug.Log("Enemy killed Mario");
		if(gameObject.activeSelf){
			StartCoroutine("Rejoice");
		}
        // do whatever you want here, animate etc
        // ...
    }
	IEnumerator Rejoice(){
		enemyBody.constraints = RigidbodyConstraints2D.FreezeAll;
		animator.SetBool("onDeath",true);
		// enemyBody.constraints = RigidbodyConstraints2D.FreezeAll;
		// spriteRenderer.flipX = true;
		yield return null;
	}
}