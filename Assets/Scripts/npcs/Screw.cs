using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : NPC
{
	// Start is called before the first frame update
	void Start()
	{
		Name = "Screwie ";
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
