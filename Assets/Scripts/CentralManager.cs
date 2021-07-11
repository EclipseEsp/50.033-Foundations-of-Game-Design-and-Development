using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this has methods callable by players
public  class CentralManager : MonoBehaviour
{
	public  GameObject gameManagerObject;
	private  GameManager gameManager;
	
	// add reference to PowerupManager
	public GameObject powerupManagerObject;
	private PowerupManager powerupManager;


	public  static  CentralManager centralManagerInstance;
	// static - any other script can call CentralManager eg. CentralManager.centralManagerInstance.method(parameters...)

	void  Awake(){
		centralManagerInstance  =  this;
	}
	// Start is called before the first frame update
	void  Start()
	{
		gameManager  =  gameManagerObject.GetComponent<GameManager>();
		powerupManager = powerupManagerObject.GetComponent<PowerupManager>();
	}

	public  void  increaseScore(){
		gameManager.increaseScore();
	}

	public void damagePlayer(){
		gameManager.damagePlayer();
	}

	public void damageEnemy(){
		gameManager.damageEnemy();
	}

	public void consumePowerup(KeyCode k, GameObject g){
		powerupManager.consumePowerup(k,g);
	}

	public void addPowerup(Texture t, int i , ConsumableInterface c){
		powerupManager.addPowerup(t, i, c);
	}
}