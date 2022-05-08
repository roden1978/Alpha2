using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Simple patrol behavior.
	/// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="_targets"/> array.
	/// Upon reaching a target it will wait for <see cref="delay"/> seconds.
	///
	/// See: <see cref="Pathfinding.AIDestinationSetter"/>
	/// See: <see cref="Pathfinding.AIPath"/>
	/// See: <see cref="Pathfinding.RichAI"/>
	/// See: <see cref="Pathfinding.AILerp"/>
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_patrol.php")]
	public class Patrol : VersionedMonoBehaviour {
		/// <summary>Target points to move to in order</summary>
		private Vector3[] _targets;

		[SerializeField] private float _patrolDistance;
		/// <summary>Time in seconds to wait at each target</summary>
		public float delay;
		

		/// <summary>Current target index</summary>
		int index;

		IAstarAI agent;
		float switchTime = float.PositiveInfinity;
		private Vector3 _startPoint;

		protected override void Awake () {
			base.Awake();
			agent = GetComponent<IAstarAI>();
			_targets = new Vector3[2];
			_startPoint = transform.position;
			_targets[0] = new Vector3(_startPoint.x + _patrolDistance, transform.position.y, 0);
			_targets[1] = _startPoint;
		}

		/// <summary>Update is called once per frame</summary>
		void Update () {
			if (_targets.Length == 0) return;

			bool search = false;

			// Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
			// if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
			if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime)) {
				switchTime = Time.time + delay;
			}

			if (Time.time >= switchTime) {
				index = index + 1;
				search = true;
				switchTime = float.PositiveInfinity;
			}

			index = index % _targets.Length;
			agent.destination = _targets[index];

			if (search) agent.SearchPath();
		}
	}
}
