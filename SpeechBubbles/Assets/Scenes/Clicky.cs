using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicky : MonoBehaviour {
	public GameObject speechBubble;

	private bool _locked;
	
	BoxCollider coll;

	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {

        // Move this object to the position clicked by the mouse.
        if (!_locked && Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (coll.Raycast(ray, out hit, 100.0f))
            {
				var pos = ray.GetPoint(100.0f);
				
				StartCoroutine(ShowStuff(pos));

            }

        }
	}

	private IEnumerator ShowStuff(Vector3 pos) {
		
		yield return ShowDialogue(pos, "Yesss, you clicked on the Cube. Click anywhere again for multiple choice thingy.");
		
		yield return ShowChoices(pos);
		yield return ShowDialogue(pos, "Your choice was numbah " + (choiceResult + 1).ToString() + ".");
		yield return 0;
	}
 
	private IEnumerator ShowDialogue(Vector3 pos, string msg)
	{
		_locked = true;

		GameObject ob = (GameObject)GameObject.Instantiate(speechBubble);
		var bubble = ob.GetComponent<SpeechBubble>();
		bubble.TextPosition = pos;
		bubble.AlignLeft = false;
		bubble.Text = msg;
		bubble.Color = SpeechBubble.Colors.Teal;
		
		yield return 0;
		yield return bubble.WaitForClick();
		
		Destroy(ob);

		_locked = false;
	}
	int choiceResult;
	private IEnumerator ShowChoices(Vector3 pos)
	{
		_locked = true;

		string[] choices = { "Hi, Alien.", "Look, a penny.\nAmongst other things.", "Your mom, lawl." };
		SpeechBubble.Colors[] colors = { SpeechBubble.Colors.Green , SpeechBubble.Colors.Teal, SpeechBubble.Colors.Red };
		
		float offset = 0;
		List<SpeechBubble> bubbles = new List<SpeechBubble>();
		for (int i = 0; i < choices.Length; i++)
		{
			GameObject ob = (GameObject)GameObject.Instantiate(speechBubble);
			var bubble = ob.GetComponent<SpeechBubble>();
			bubble.TextPosition = pos;
			bubble.ShiftUp(offset);
			bubble.Text = choices[i];
			bubble.Color = colors[i];
			bubbles.Add(bubble);
			offset += bubble.BoxHeight + 20;
		}

		choiceResult = -1;

		while (choiceResult == -1) {
			if (Input.GetMouseButtonUp(0)) {
				for (int i = 0; i < bubbles.Count; i++)
				{
					if (bubbles[i].ContainsMouse()) {
						choiceResult = i;
						break;
					}
				}
			}
			yield return 0;
		}
		
		foreach (var bubble in bubbles)
		{
			Destroy(bubble.gameObject);
		}


		_locked = false;
	}

}
