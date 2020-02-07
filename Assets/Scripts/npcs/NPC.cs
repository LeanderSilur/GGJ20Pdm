using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject speechBubbleTemplate;
    public string Name;
	public int choiceResult;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
	}

    public virtual IEnumerator Interact(int actionID)
    {
        yield return ShowDialogue(transform.position, "Hello, I'm " + Name + ".");
        yield return 0;
    }
 
    public IEnumerator ShowDialogue(Vector3 pos, string msg)
    {
        GameObject ob = (GameObject)GameObject.Instantiate(speechBubbleTemplate);
        var bubble = ob.GetComponent<SpeechBubble>();
        bubble.TextPosition = pos;
        bubble.AlignLeft = true;
        bubble.Text = msg;
        bubble.Color = SpeechBubble.Colors.Teal;

        yield return 0;
        yield return bubble.WaitForClick();

        Destroy(ob);
    }

	public IEnumerator ShowChoices(Vector3 pos)
	{
		string[] choices = { "Hi, Alien.", "Look, a penny.\nAmongst other things. asfuuef fiewuf kirenlorem jeofbla", "Your mom, lawl." };
		SpeechBubble.Colors[] colors = { SpeechBubble.Colors.Green, SpeechBubble.Colors.Teal, SpeechBubble.Colors.Red };
		yield return 0;
	}
	public IEnumerator ShowChoices(string[] choices, SpeechBubble.Colors[] colors)
	{
		yield return ShowChoices(this.transform.position, choices, colors);
	}

	public IEnumerator ShowChoices(Vector3 pos, string[] choices, SpeechBubble.Colors[] colors)
	{
		float offset = 0;
		List<SpeechBubble> bubbles = new List<SpeechBubble>();
		for (int i = 0; i < choices.Length; i++)
		{
			GameObject ob = (GameObject)GameObject.Instantiate(speechBubbleTemplate);
			var bubble = ob.GetComponent<SpeechBubble>();
			bubble.TextPosition = pos;
			bubble.ShiftUp(offset);
			bubble.Text = choices[i];
			bubble.Color = colors[i];
			bubbles.Add(bubble);
			offset += bubble.BoxHeight + 20;
		}

		choiceResult = -1;

		while (choiceResult == -1)
		{
			if (Input.GetMouseButtonUp(0))
			{
				for (int i = 0; i < bubbles.Count; i++)
				{
					if (bubbles[i].ContainsMouse())
					{
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

	}
}
