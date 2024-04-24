using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TurnSystem : MonoBehaviour {

	private List<UnitStats> unitsStats;

	private GameObject playerParty;

    private List<UnitStats> completedTheMoveUnits; 

	public GameObject enemyEncounter;

	[SerializeField]
	private GameObject actionsMenu, enemyUnitsMenu;

	void Start() 
	{
		this.playerParty = GameObject.Find ("PlayerParty");

		unitsStats = new List<UnitStats> ();
		completedTheMoveUnits = new List<UnitStats>();
		GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
		
		foreach (GameObject playerUnit in playerUnits)
		{
			UnitStats currentUnitStats = playerUnit.GetComponent<UnitStats> ();
			currentUnitStats.calculateNextActTurn (0);
			unitsStats.Add (currentUnitStats);
		}
		GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
		foreach (GameObject enemyUnit in enemyUnits)
		{
			UnitStats currentUnitStats = enemyUnit.GetComponent<UnitStats> ();
			currentUnitStats.calculateNextActTurn (0);
			unitsStats.Add (currentUnitStats);
		}
		unitsStats.Sort ();

		this.actionsMenu.SetActive (false);
		this.enemyUnitsMenu.SetActive (false);

        

		this.nextTurn ();	
	}

	private void returnUnitsInList()
    {
		if(completedTheMoveUnits.Count != 0)
        {
			UnitStats currentUnitStats = completedTheMoveUnits[0];
			unitsStats.Add(currentUnitStats);
			completedTheMoveUnits.Remove(currentUnitStats);

			this.returnUnitsInList();
        }
    }
	public void nextTurn() {
		GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag ("EnemyUnit");
		if (remainingEnemyUnits.Length == 0)
		{
			this.enemyEncounter.GetComponent<CollectReward> ().collectReward ();
			SceneManager.LoadScene ("Town");
		}

		GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag ("PlayerUnit");
		if (remainingPlayerUnits.Length == 0)
		{
			SceneManager.LoadScene("Title");
		}

		UnitStats currentUnitStats = unitsStats [0];
		unitsStats.Remove (currentUnitStats);

          if (unitsStats.Count == 0)
          {
				
			Debug.Log("работаешб?");
			Debug.Log(completedTheMoveUnits[0]);
			completedTheMoveUnits.Sort();
			this.returnUnitsInList();
			
          }

		unitsStats.Sort();

		if (!currentUnitStats.isDead())
		{
			GameObject currentUnit = currentUnitStats.gameObject;

			currentUnitStats.calculateNextActTurn(currentUnitStats.nextActTurn);
            //unitsStats.Add(currentUnitStats);

            completedTheMoveUnits.Add(currentUnitStats);
            unitsStats.Sort();
			
			if (currentUnit.tag == "PlayerUnit")
			{
				this.playerParty.GetComponent<SelectUnit>().selectCurrentUnit(currentUnit.gameObject);
			} 
			else 
			{
				currentUnit.GetComponent<EnemyUnitAction>().act();
			}
		} 
		else 
		{
			this.nextTurn ();
		}
	}
}
