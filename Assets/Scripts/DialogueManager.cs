using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GG {
	public class DialogueManager : MonoBehaviour
	{
		[SerializeField]
		public DialogueTree dialogue_1;
		[SerializeField]
		public DialogueTree dialogue_2;
		[SerializeField]
		public DialogueTree dialogue_3;
		[SerializeField]
		public Text text;

		// Start is called before the first frame update
		void Start()
		{
			text.text = dialogue_1.serializedNodes[1].text;
		}

		// Update is called once per frame
		void Update()
		{

		}
	}

}