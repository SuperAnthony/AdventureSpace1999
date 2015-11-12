#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 31, 2015
#endregion 

#region using
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Utilities for general data manuplation.
	/// </summary>

	public sealed class DataUtilities {


		private DataUtilities() {
		}

		/// <summary>
		/// Adds an element to an array.
		/// This is meant to be practial and not meant for speed or efficiency.
		/// DO NOT use this multiple times in the same loop in order to add multiple elements to the same array at once; 
		/// that would not be effiecient
		/// </summary>
		/// <returns>The new array.</returns>
		/// <param name="oldArray">Old array.</param>
		/// <param name="newElement">New element.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>

		static public T [] AddArrayElement<T>(T [] oldArray, T newElement) {
		
			// Create a new array with one additional element
			T [] newArray = new T[oldArray.Length + 1] ;

			// Fill in the data of the old array
			for(int i = 0; i < oldArray.Length; i++) {
				newArray[i] = oldArray[i];
			}

			// Add the new element to the last element of temp
			newArray [newArray.Length - 1] = newElement;

			return newArray;

		}

		/// <summary>
		/// Removes the array element.
		/// </summary>
		/// <returns>The array element.</returns>
		/// <param name="oldArray">Old array.</param>
		/// <param name="element">Element.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>

		static public T [] RemoveArrayElement<T>(T [] oldArray, T element) where T : class {
		
			// Create a new array with one less element 
			T [] newArray = new T[oldArray.Length - 1] ;

			// Fill in the data of the old array while preventing the 'element' from being added
			int i = 0;

			foreach (T item in oldArray) {
				// Prevent the removing element from being added to the new array
				if(item != element) {
					newArray[i] = item;
					i++; 
				}
			}




			return newArray;

		
		}

		/// <summary>
		/// Resizes the array.
		/// </summary>
		/// <returns>The array.</returns>
		/// <param name="oldArray">Old array.</param>
		/// <param name="length">Length.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>

		static public T [] ResizeArray<T>(T [] oldArray, int length) where T : new() {
			
			// Create a new array with one additional element
			T [] temp = new T[oldArray.Length + length] ;
			
			// Fill in the data of the old array
			for(int i = 0; i < oldArray.Length; i++) {
				temp[i] = oldArray[i];
			}


			// Add new element
			for (int i = oldArray.Length; i < temp.Length; i++) {
			
				temp[i] = new T();

			}

			return temp;
			
		}


	}
}
