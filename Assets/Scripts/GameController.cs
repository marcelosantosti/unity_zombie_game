using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject selectedZombie;
	public List<GameObject> listZombie;
	public Text textScore;

	public Vector3 selectedZombieScale;
	public Vector3 unselectedZombieScale;

	private int lastZombiePushed;
	private int selectedZombieIndex;
	private int score = 0;

	// Use this for initialization
	void Start () 
	{
		if (this.selectedZombie != null)
		{
			this.SelectZombie();
		}

		this.UpdateScore ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.RemoveZombieIfNeeded ();
		this.GetNewZombieIndex();
		this.PushZombieUp();
	}

	private void GetNewZombieIndex()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow) == true)
		{
			this.SelectZombieRight ();
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
		{
			this.SelectZombieLeft ();
		}
	}

	private void SelectZombieLeft()
	{
		int newSelectedZombieIndex = this.selectedZombieIndex;

		if (newSelectedZombieIndex == 0)
		{
			newSelectedZombieIndex = this.listZombie.Count - 1;
		}
		else
		{
			newSelectedZombieIndex = this.selectedZombieIndex - 1;
		}

		this.UnselecAndSelectZombie (newSelectedZombieIndex);
	}

	private void SelectZombieRight()
	{
		int newSelectedZombieIndex = this.selectedZombieIndex;

		if (newSelectedZombieIndex + 1 == this.listZombie.Count)
		{
			newSelectedZombieIndex = 0;
		}
		else
		{
			newSelectedZombieIndex = newSelectedZombieIndex + 1;
		}

		this.UnselecAndSelectZombie (newSelectedZombieIndex);
	}

	private void UnselecAndSelectZombie(int newSelectedZombieIndex)
	{
		if (this.selectedZombieIndex != newSelectedZombieIndex || newSelectedZombieIndex == 0)
		{
			this.UnselectZombie();
			this.selectedZombieIndex = newSelectedZombieIndex;
			Debug.Log ("SelectedZombieIndex: " + this.selectedZombieIndex + " New Selected Zombie: " + newSelectedZombieIndex + " Total Zombie List: " + this.listZombie.Count);
			if (this.listZombie.Count != 0)
			{
				this.selectedZombie = this.listZombie[this.selectedZombieIndex];
				this.SelectZombie();	
			}
		}
	}

	private void PushZombieUp()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) == true)
		{
			Rigidbody rigidBody = this.selectedZombie.GetComponent<Rigidbody>();
			rigidBody.AddForce(new Vector3(0, 0, 10f), ForceMode.Impulse);

			this.lastZombiePushed = this.selectedZombieIndex;
		}
	}

	private void SelectZombie()
	{
		this.selectedZombie.transform.localScale = this.selectedZombieScale;
	}

	private void UnselectZombie()
	{
		this.selectedZombie.transform.localScale = this.unselectedZombieScale;
	}

	public void AddScore()
	{
		this.score = this.score + 1;
		this.UpdateScore ();
	}

	private void UpdateScore()
	{
		this.textScore.text = "Score: " + this.score;
	}

	private void RemoveZombieIfNeeded()
	{
		for (int i = 0; i < this.listZombie.Count; i++)
		{
			GameObject currentZombie = this.listZombie [i];

			bool outsideBlock = currentZombie.transform.position.z <= -11;

			if (currentZombie.transform.position.z >= 8 || (outsideBlock && currentZombie == this.selectedZombie)) 
				
			{
				this.listZombie.Remove (currentZombie);

				//verify if the user did not changed the zombie position
				if (this.lastZombiePushed == this.selectedZombieIndex || outsideBlock)
				{
					this.SelectZombieLeft ();
				}

				Debug.Log ("Removed Zombie. Total zombies now: " + this.listZombie.Count);
			}
		}
	}
}
