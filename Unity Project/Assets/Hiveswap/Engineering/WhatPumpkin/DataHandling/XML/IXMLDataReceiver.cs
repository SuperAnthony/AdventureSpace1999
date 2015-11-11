using UnityEngine;
using System.Collections;
using System.Xml;

namespace WhatPumpkin.Data.XML {

	/// <summary>
	/// IXML data receiver. Implement this interface to objects that receive and parse xml data.
	/// </summary>

	public interface IXMLDataReceiver {

		/// <summary>
		/// Parses the XML data.
		/// </summary>
		/// <param name="xmlData">Xml data.</param>

		bool ParseXMLData(XmlDocument xmlData, params object[] parameters);

	}
}
