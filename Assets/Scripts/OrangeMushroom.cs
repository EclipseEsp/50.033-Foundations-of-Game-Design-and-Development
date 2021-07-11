using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture texture;
    public void consumedBy(GameObject player){
        // give player speed boost
        player.GetComponent<PlayerController>().maxSpeed *= 2;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player){
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().maxSpeed /= 2;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            // update UI
            CentralManager.centralManagerInstance.addPowerup(texture, 0, this);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
