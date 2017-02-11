using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public GameObject selectedZombie;
	public List<GameObject> listZombie;

	public Vector3 selectedZombieScale;
	public Vector3 unselectedZombieScale;

	private int selectedZombieIndex;

	// Use this for initialization
	void Start () 
	{
		if (this.selectedZombie != null)
		{
			this.SelectZombie();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.GetNewZombieIndex();
		this.PushZombieUp();
	}

	private void GetNewZombieIndex()
	{
		int newSelectedZombieIndex = this.selectedZombieIndex;

		if (Input.GetKeyDown(KeyCode.RightArrow) == true)
		{
			if (newSelectedZombieIndex + 1 == this.listZombie.Count)
			{
				newSelectedZombieIndex = 0;
			}
			else
			{
				newSelectedZombieIndex = newSelectedZombieIndex + 1;
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
		{
			if (newSelectedZombieIndex == 0)
			{
				newSelectedZombieIndex = this.listZombie.Count - 1;
			}
			else
			{
				newSelectedZombieIndex = this.selectedZombieIndex - 1;
			}
		}

		if (this.selectedZombieIndex != newSelectedZombieIndex)
		{
			this.UnselectZombie();
			this.selectedZombieIndex = newSelectedZombieIndex;
			this.selectedZombie = this.listZombie[this.selectedZombieIndex];
			this.SelectZombie();
		}
	}

	private void PushZombieUp()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) == true)
		{
			Rigidbody rigidBody = this.selectedZombie.GetComponent<Rigidbody>();
			rigidBody.AddForce(new Vector3(0, 0, 10f), ForceMode.Impulse);
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
}
