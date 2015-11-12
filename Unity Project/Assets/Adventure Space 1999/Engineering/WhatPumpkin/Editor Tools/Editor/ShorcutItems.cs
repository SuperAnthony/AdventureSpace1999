#region using
using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using WhatPumpkin.CameraManagement;
using WhatPumpkin.Sgrid.Triggers;
using WhatPumpkin.CutScenes;
using WhatPumpkin;
using WhatPumpkin.Entities;
using WhatPumpkin.Sgrid.Markers;
using WhatPumpkin.Actions.Sequences;
#endregion

public class ShortcutItems : MonoBehaviour {

	[MenuItem("HiveSwap/Shortcuts/Set Key To Game Object Name &k")]
	
	/// <summary>
	/// Sets the name of the key to game object.
	/// </summary>
	
	static public void SetKeyToGameObjectName() {
		
		EntityInfo entity = Selection.activeGameObject.GetComponent<EntityInfo>();
		if(entity != null) {
			entity.SetKey(Selection.activeGameObject.name);
		}
		
	}


	[MenuItem("HiveSwap/Shortcuts/Select Parent &p")]

	/// <summary>
	/// Select the highest parent in the hierarchy
	/// </summary>

	static public void SelectParent() {
		
		Transform selection = Selection.activeGameObject.transform;

		if (selection != null) {
		
			while(selection.transform.parent != null) {
			
				selection = selection.parent.transform;

			}

		
			Selection.activeGameObject = selection.gameObject;

		}
		
	}

	[MenuItem("HiveSwap/Shortcuts/Move Up One Level &o")]

	static public void MoveUpOneLevel() {
		
		Transform selection = Selection.activeGameObject.transform;
		
		if (selection != null) {
			
			if(selection.transform.parent != null) {
				
				selection = selection.parent.transform;
				
			}
			
			
			Selection.activeGameObject = selection.gameObject;
			
		}
		
	}

}
