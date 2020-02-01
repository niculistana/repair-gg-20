using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GG {
	public class DialogueNode : MonoBehaviour {
		public string text = "value";
		public List<DialogueNode> children = new List<DialogueNode>();
	}
}