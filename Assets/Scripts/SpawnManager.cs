using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager spawnManager;
    // Start is called before the first frame update
    float groundDistance = -4.0f;
    void Start()
    {
        for (int j = 0; j < 2; j ++){
            spawnFromPooler(ObjectType.greenEnemy);
            spawnFromPooler(ObjectType.gombaEnemy);
        }

        GameManager.OnEnemyDeath += spawnNewEnemy;

    }

    // void startSpawn(Scene scene, LoadSceneMode mode)
    // {
    //     for (int j = 0; j < 2; j++){
    //         spawnFromPooler(ObjectType.gombaEnemy);
    //     }
    // }

    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            if(i == ObjectType.gombaEnemy) item.transform.localScale = new Vector3(1, 1, 1);
            else item.transform.localScale = new Vector3(0.6137f, 0.6137f, 0.6137f);
            // item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), groundDistance + 
            item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy()
    {

        ObjectType i = Random.Range(0, 2) == 0 ? ObjectType.gombaEnemy : ObjectType.greenEnemy;
        spawnFromPooler(i);

    }
    
}
