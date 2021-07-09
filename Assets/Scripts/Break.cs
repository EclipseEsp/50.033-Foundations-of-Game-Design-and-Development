// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Break : MonoBehaviour
// {   
//     private Rigidbody2D rigidbody;
//     private Vector3 scaler;
//     public GameConstants gameConstants;
//     // Start is called before the first frame update
//     void Start()
//     {
//        scaler = transform.localScale / (float) gameConstants.breakTimeStep;
//        rigidbody = GetComponent<Rigibody2D>();
//        StartCoroutine("ScaleOut"); 
//     }

//     IEnumerator ScaleOut(){
//         Vector2 direction = new Vector2(Random.Range(-1.0f,1.0f),1);
//         rigidBody.AddForce(direction.normalized * gameConstants.breakDebrisForce, ForceMode2D.Impulse);
//         rigidBody.AddTorque(gameConstants.breakDebrisTorque, ForceMode2D.Impulse);
//         // wait for next frame
//         yield return null;

//         // render for 0.5 second
//         for (int step = 0; step < gameConstants.breakTimeStep; step++)
//         {
//             this.transform.localScale = this.transform.localScale - scaler;
//             // wait for next frame
//             yield return null;
//         }

//         Destroy(gameObject);
//     }
//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
