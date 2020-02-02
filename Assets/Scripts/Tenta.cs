using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenta : NPC
{
	// Start is called before the first frame update
	void Start()
	{
		Name = "Tenta";
	}

	// Update is called once per frame
	void Update()
	{

	}

	public override IEnumerator Interact(int actionID)
	{
		yield return ShowDialogue(transform.position, "Clicked on the tenta. Amongst other things. asfuuef fiewuf kirenlorem jeofbla Click anywhere to continue");

		yield return ShowChoices(transform.position);

		yield return ShowDialogue(transform.position, "Your choice was numbah " + (choiceResult + 1).ToString() + ".");

	}
}
