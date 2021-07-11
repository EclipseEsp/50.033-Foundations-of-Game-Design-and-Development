using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class GameManager : MonoBehaviour
{
	public  Text score;
	private  int playerScore =  0;
	public delegate void gameEvent();
	public static event gameEvent OnPlayerDeath; //creating a delegate so other scripts can subscript to it
	public static event gameEvent OnEnemyDeath;

	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
		Debug.Log(score);
	}

	public void damagePlayer(){
		OnPlayerDeath(); // the delegate event is triggered when damagePlayer is called from any script, triggering all subscribers.
	}

	public void damageEnemy(){
		OnEnemyDeath();
	}
}