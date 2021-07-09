using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffScroller : MonoBehaviour
{
    public Renderer[] layers;
    public float[] speedMultiplier;
    private float previousXPositionMario;
    private float previousXPositionCamera;
    public Transform mario;
    public Transform mainCamera;
    private float[] offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new float[layers.Length];
        for(int i = 0; i < layers.Length; i++){
            offset[i] = 0.0f;	
		}
        previousXPositionCamera = mainCamera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< layers.Length; i ++){
            float newOffset = 0.01f;
            offset[i] = offset[i] + newOffset * speedMultiplier[i];
            layers[i].material.mainTextureOffset = new Vector2(0 , -offset[i]);
        }
        previousXPositionCamera = mainCamera.transform.position.y;
    }
}
