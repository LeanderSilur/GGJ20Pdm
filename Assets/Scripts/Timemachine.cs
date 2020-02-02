using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timemachine : NPC
{
	public EventHandler Jump;
    private enum TIME { Past = 0, Present = 1 };

	// Start is called before the first frame update
	void Start()
	{
		
		
	} 

	// Update is called once per frame
	void Update()
	{

	}

	public override IEnumerator Interact(int actionID)
	{
		string[] choices = { "Jump.", "Stay." };
		SpeechBubble.Colors[] colors = { SpeechBubble.Colors.Red, SpeechBubble.Colors.Gray };
		yield return ShowChoices(choices, colors);
		if (choiceResult == 0)
        {
			if (Jump != null)
				Jump(this, new EventArgs());
        }
        /*
		if (actionID == 3) { // actionID 3 => State, der besagt, dass Zeit geswitcht werden kann.
            if (Name == "Gegenwart")
            {
                Sh
				yield return switchTo(TIME.Past);
            }
            else
            {
				yield return switchTo(TIME.Present);
            }
		}

		//yield return base.Interact(-1);
		yield return 0;
        */
	}

    private IEnumerator switchTo(Timemachine.TIME time)
    {
		yield return 0;
    }
}
