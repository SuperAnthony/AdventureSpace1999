using UnityEngine;
using System.Collections;


using WhatPumpkin.Sgrid.Characters;
using WhatPumpkin.Sgrid.Markers;

// TODO: Remove

namespace WhatPumpkin.EditorTesting {

	public interface IInitGameController  {

		/// <summary>
		/// Gets the spawn point.
		/// </summary>
		/// <value>The spawn point.</value>

		SpawnPoint SpawnPoint { get; }

		/// <summary>
		/// Gets the active PC.
		/// </summary>
		/// <value>The active P.</value>

		PlayerCharacter ActivePC { get; }

	}
}
