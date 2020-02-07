using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Cube : NPC
{
	// Start is called before the first frame update
	void Start()
	{
		Name = "Cubi";
	} 

	// Update is called once per frame
	void Update()
	{

	}

	public override IEnumerator Interact(int actionID)
	{
		yield return base.Interact(-1);
	}
}
