using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public GameObject obstacle;
    // public Transform startXLimit;
    // public Transform endXLimit;
    // public Transform startYLimit;
    // public Transform endYLimit;
    public Transform mainCamera;
    private float startX;
    private float endX;
    private float startY;
    private float endY;
    private float centerX;
    private float centerY;
    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private float obstacleWidth;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        
        viewportHalfWidthX  =  Mathf.Abs(bottomLeft.x  -  mainCamera.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y  -  mainCamera.position.y);

        obstacleWidth = obstacle.GetComponent<SpriteRenderer>().sprite.rect.width;

        Rigidbody2D obstacleBody = obstacle.GetComponent<Rigidbody2D>();

        Debug.Log(obstacleWidth);
    
        Instantiate(obstacle,
        new Vector3(
            Random.Range(bottomLeft.x, bottomLeft.x + 2 * viewportHalfWidthX - obstacleWidth),
                bottomLeft.y, 0) , Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        // new Vector3(Random.Range(startX,endX), startY);
        // Instantiate(obstacle, new Vector3(Random.Range(startX,endX), startY) , Quaternion.identity);
    }
}
