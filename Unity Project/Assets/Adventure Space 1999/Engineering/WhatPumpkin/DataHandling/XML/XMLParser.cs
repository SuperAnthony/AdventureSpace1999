// Date 1/12/15

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
#endregion


namespace WhatPumpkin.Data.XML {

	/// <summary>
	/// XML parser. Used to locate data in an xml node.
	/// </summary>

	public class XMLParser {

		#region static methods
		/// <summary>
		/// Gets the child attribute values.
		/// </summary>
		/// <returns>The child attribute values.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="attribute">Attribute.</param>

		static public List<string> GetChildAttributeValues(XmlNode parent, string attribute) {
		
			// TODO: Error Check

			List<string> values = new List<string> ();

			foreach (XmlNode childNode in parent.ChildNodes) {
			
				string value = childNode.Attributes[attribute].Value;
			


				if(value != null) {
					values.Add(value);
				}
			}

			return values;

		}


		/// <summary>
		/// Finds the first node of a given name.
		/// </summary>
		/// <returns>The node by name.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="name">Name.</param>

		static public XmlNode FindNodeByName(XmlNode parent, string name) {

			foreach (XmlNode childNode in parent.ChildNodes) {
				
				if(childNode.Name == name) {
					return childNode;
				}
			}

			return null;
		}

		/// <summary>
		/// Finds the nodes of a given name.
		/// </summary>
		/// <returns>The nodes by name.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="name">Name.</param>

		static public List<XmlNode> FindNodesByName(XmlNode parent, string name) {
		
			List<XmlNode> nodes = new List<XmlNode> ();

			foreach (XmlNode childNode in parent.ChildNodes) {
			
				if(childNode.Name == name) {
					nodes.Add(childNode);
				}
			}

			return nodes;

		}
		#endregion

	}

}
