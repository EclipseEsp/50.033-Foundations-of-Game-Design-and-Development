using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{  
    public GameConstants gameConstants;
    public float speed;
    // public float speed;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudioSource;

    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool countScoreState = false;


    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudioSource = GetComponent<AudioSource>();
    }

    public float maxSpeed = 10;
    public float upSpeed = 10;
    void FixedUpdate()
    {
        // Input.GetAxis, returns value (left)-1...1(right) for keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal,0);
            //Vector of 2D position (x,y) -1=left or down , 1=right or up
            if(marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            //stop if "a" or "d" key lifted
            marioBody.velocity = Vector2.zero;
        }
        
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = true; // check if Gomba is underneath
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            StartCoroutine(Timer());
        }
    }

    void PlayJumpSound(){
        marioAudioSource.PlayOneShot(marioAudioSource.clip);
    }
    IEnumerator Timer(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }


    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = false; // reset score state
            //scoreText.text = "Score: " + score.ToString();
        }

        if (col.gameObject.CompareTag("Obstacles") && Mathf.Abs(marioBody.velocity.y) < 0.1)
        {
            onGroundState= true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }



    // Update is called once per frame 
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;

            // check velocity
            if (Mathf.Abs(marioBody.velocity.x) > 1.0){
                marioAnimator.SetTrigger("onSkid");
            }

        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;

             // check velocity
            if (Mathf.Abs(marioBody.velocity.x) > 1.0){
                marioAnimator.SetTrigger("onSkid");
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState =false;
                score++;
                Debug.Log(score);
            }
        }
    }

}
