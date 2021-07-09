using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{   
    public GameObject obstacle;
    private Transform mainCamera;
    public float speed;
    private float obstacleHeight;
    private float viewportHalfHeightY;
    private Vector3 bottomLeft;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Cliff").GetComponent<Transform>();
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y  -  mainCamera.position.y);
        obstacleHeight = obstacle.GetComponent<SpriteRenderer>().sprite.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if( (obstacle.transform.position.y - obstacleHeight/2) > ( bottomLeft.y + 2* viewportHalfHeightY ) ){
            Destroy(gameObject);
        }
    }
}
