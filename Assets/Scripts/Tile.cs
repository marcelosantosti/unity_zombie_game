using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour 
{
	public GameController gameController;
	public AudioClip audioClipHit;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		this.gameController = GameObject.Find("Main Camera").GetComponent<GameController> ();

		if (this.gameController != null) 
		{
			Debug.Log ("Game controller is not null");	
		}
		else
		{
			Debug.Log ("Game controller is null");	
		}	

		this.audioSource = base.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		this.gameController.AddScore();
		this.audioSource.PlayOneShot (this.audioClipHit);
	}
}
