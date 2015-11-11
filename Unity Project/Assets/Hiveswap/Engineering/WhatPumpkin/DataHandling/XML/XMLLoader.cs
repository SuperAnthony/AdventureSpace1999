#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
#endregion 

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Xml;

namespace WhatPumpkin.Data.XML {

	public class XMLLoader : MonoBehaviour {


		/// <summary>
		/// Load the file of the specified path and let's the receiver attempt to parse that data.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="receiver">Receiver.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>

		static public bool Load<T>(string path, T receiver, params object [] parameters) where T:IXMLDataReceiver {
		
			// Get the XML data
			XmlDocument xmlData = new XmlDocument();
//			
			try {
				xmlData.Load(path);
			}
			catch (DirectoryNotFoundException e) {
			
				Debug.LogWarning("Could not locate the directory: " + path);
				throw(e);
				return false;

			}
			catch(Exception e) {
			
				throw(e);
				return false;
			}


			try{
				// Tell the receiver to parse the data
//				Debug.Log (xmlData.InnerXml);
				receiver.ParseXMLData (xmlData, parameters);
				return true;
			}
			catch (Exception e) {

				throw(e);
				return false;;
			}



			return false;
		
		}
	}
}
