using UnityEngine;

namespace WhatPumpkin.Sgrid {

	public interface IAIPath {
	    
	    /// <summary>
	    /// The transform of the pathing character
	    /// </summary>

	    Transform Transform { get; }

	    /// <summary>
	    /// The target of the pathing character
	    /// </summary>

	    Transform Target { get; set; }

	    /// <summary>
	    /// What is the max speed
	    /// </summary>

	    float Speed { get; set; }

	    /// <summary>
	    /// What is the current velocity
	    /// </summary>

	    Vector3 CurrentVelocity { get; }

	    /// <summary>
	    /// Enable path finding
	    /// </summary>

	    void Enable();

	    /// <summary>
	    /// Disable the path finding
	    /// </summary>

	    void Disable();

	    /// <summary>
	    /// Did the pathing character reach its target
	    /// </summary>

	    bool TargetReached { get; }

	    /// <summary>
	    /// Tell the pathing character to search for a path
	    /// </summary>

	    void SearchPath();

	}
}
