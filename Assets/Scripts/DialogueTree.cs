using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GG
{
	public class DialogueTree : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Node class that we will use for serialization.
		[Serializable]
		public struct SerializableNode
		{
			public string text;
			public int childCount;
			public int indexOfFirstChild;
		}

		public TextAsset dialogueFile;


		// The __root node__ used for runtime tree representation. Not serialized.
		DialogueNode root = new DialogueNode();
		// This is the field we give Unity to serialize.
		public List<SerializableNode> serializedNodes;
		public void OnBeforeSerialize()
		{
			// Unity is about to read the serializedNodes field's contents.
			// The correct data must now be written into that field "just in time".
			if (serializedNodes == null) serializedNodes = new List<SerializableNode>();
			if (root == null) root = new DialogueNode();
			// seed children for dialogue generation
			SeedChildren(out root.children);
			serializedNodes.Clear();
			AddNodeToSerializedNodes(root);
			// Now Unity is free to serialize this field, and we should get back the expected 
			// data when it is deserialized later.
		}
		void AddNodeToSerializedNodes(DialogueNode n)
		{
			var serializedNode = new SerializableNode() {
				text = n.text,
				childCount = n.children.Count,
				indexOfFirstChild = serializedNodes.Count + 1
			}
			;
			serializedNodes.Add(serializedNode);
			foreach (var child in n.children)
				AddNodeToSerializedNodes(child);
		}
		public void OnAfterDeserialize()
		{
			//Unity has just written new data into the serializedNodes field.
			//let's populate our actual runtime data with those new values.
			if (serializedNodes.Count > 0) {
				ReadNodeFromSerializedNodes(0, out root);
			} else
				root = new DialogueNode();
		}
		int ReadNodeFromSerializedNodes(int index, out DialogueNode node)
		{
			var serializedNode = serializedNodes[index];
			// Transfer the deserialized data into the internal Node class
			DialogueNode newNode = new DialogueNode() {
				text = serializedNode.text,
				children = new List<DialogueNode>()
			}
			;
			// The tree needs to be read in depth-first, since that's how we wrote it out.
			for (int i = 0; i != serializedNode.childCount; i++) {
				DialogueNode childNode;
				index = ReadNodeFromSerializedNodes(++index, out childNode);
				newNode.children.Add(childNode);
			}
			node = newNode;
			return index;
		}

		// This OnGUI draws out the node tree in the Game View, with buttons to add new nodes as children.
		//void OnGUI()
		//{
		//	if (root != null)
		//		Display(root);
		//}
		//void Display(Node node)
		//{
		//	GUILayout.Label("Value: ");
		//	// Allow modification of the node's "interesting value".
		//	node.text = GUILayout.TextField(node.text, GUILayout.Width(200));
		//	GUILayout.BeginHorizontal();
		//	GUILayout.Space(20);
		//	GUILayout.BeginVertical();
		//	foreach (var child in node.children)
		//		Display(child);
		//	if (GUILayout.Button("Add child"))
		//		node.children.Add(new Node());
		//	GUILayout.EndVertical();
		//	GUILayout.EndHorizontal();
		//}

		void SeedChildren(out List<DialogueNode> children)
		{
			string[] sentences = dialogueFile.text.Split('\n');
			children = new List<DialogueNode>();
			foreach (string sentence in sentences) {
				DialogueNode child = new DialogueNode();
				child.text = sentence;
				children.Add(child);
			}
		}

	}

}