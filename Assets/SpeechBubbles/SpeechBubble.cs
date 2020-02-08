using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class SpeechBubble : MonoBehaviour {

	private Transform background;
	private Image hightlightImage ;
	private Text _text;
	private Camera cam;
	public string Text {
		set {
			TextGenerator textGen = new TextGenerator();
			TextGenerationSettings generationSettings = _text.GetGenerationSettings(_text.rectTransform.rect.size); 
			float height = textGen.GetPreferredHeight(value, generationSettings);
			height += _text.rectTransform.offsetMin.y - _text.rectTransform.offsetMax.y;
			_text.text = value;
			
			RectTransform tr = GetComponent<RectTransform>();
			float width = tr.sizeDelta.x;
			tr.sizeDelta = new Vector2 (width, height);
		}
	}

	public float BoxHeight {
		get {
			return GetComponent<RectTransform>().sizeDelta.y;
		}
	}
	
	public bool AlignLeft {
		set {
			if (value) {
				_text.alignment = TextAnchor.LowerRight;
				background.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
			}
			else {
				_text.alignment = TextAnchor.LowerLeft;
				background.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
			}
		}
	}
	
	public Vector3 TextPosition {
		set {
			if (value != null) {
				this.transform.position = cam.WorldToScreenPoint(value);
			}
		}
	}
	public void ShiftUp(float amount) {
		var pos = this.transform.position;
		this.transform.position = new Vector3(pos.x, pos.y + amount, pos.z);
	}
	
	public Colors Color {
		set {
			Image img = background.gameObject.GetComponent<Image>();
			if (value == Colors.Teal)
				img.color = new Color32(144, 186, 192, 255);
			else if (value == Colors.Purple)
				img.color = new Color32(169, 136, 167, 255);
			else if (value == Colors.Green)
				img.color = new Color32(150, 192, 133, 255);
			else if (value == Colors.Red)
				img.color = new Color32(193, 135, 135, 255);
			else if (value == Colors.Gray)
				img.color = new Color32(139, 139, 142, 255);
		}
	}
	public enum Colors {
		Teal, Purple, Green, Red, Gray
	}

	private void Awake() {
		background = transform.GetChild(0);
		hightlightImage = background.GetChild(1).GetComponent<Image>();
		_text = background.GetChild(0).GetComponent<Text>();
		GameObject canvas = GameObject.Find("Canvas");
		transform.SetParent(canvas.transform);

		GameObject manager = GameObject.Find("GameManager");
		cam = manager.GetComponent<GameManager>().cam;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float alpha = hightlightImage.color.a;
		if (ContainsMouse()) {
			hightlightImage.color = new Color(1, 1, 1, Mathf.Min(1f, alpha + 0.1f));
		}
		else {
			hightlightImage.color = new Color(1, 1, 1, Mathf.Max(0f, alpha - 0.1f));
		}
	}
 
	public IEnumerator WaitForClick()
	{
		while (true) {
			if (Input.GetMouseButtonDown(0)) {
				break;
			}
			
			yield return 0;
		}
	}
	
	
	public bool ContainsMouse()
	{
		var trans = GetComponent<RectTransform>();
		
		Vector2 size = Vector2.Scale(trans.rect.size, trans.lossyScale);
		Rect rect = new Rect((Vector2)trans.position, size);
		return rect.Contains(Input.mousePosition);
	}
}
