using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GG {
	[System.Serializable]
	class DialogEvent : UnityEvent<int> {
	}
	public class DialogueManager : MonoBehaviour
	{
		[SerializeField]
		public DialogueTree dialogue_1;
		[SerializeField]
		public DialogueTree dialogue_2;
		[SerializeField]
		public DialogueTree dialogue_3;
		[SerializeField]
		public Text dialogueText;
		[SerializeField]
		public Text choiceText_1;
		[SerializeField]
		public Text choiceText_2;
		[SerializeField]
		public Text choiceText_3;
		[SerializeField]
		public Text choiceText_4;

		private DialogEvent dialogEvent;

		// Start is called before the first frame update
		void Start()
		{
			if (dialogEvent == null) {
				dialogEvent = new DialogEvent();
			}
			dialogueText.text = dialogue_1.serializedNodes[0].text;
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void ChoiceSelect(int choice) {
			Debug.Log("Choice: " + choice);
		}
	}

}