using UnityEngine;
using System.Collections;

// This class helps with returning values from a coroutine
public class CoroutineWithData {

	public Coroutine coroutine { get; private set; }
	public object result;
	private IEnumerator target;

	// constructs the coroutine
	public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
		this.target = target;
		this.coroutine = owner.StartCoroutine(Run());
	}

	// This runs the coroutine
	private IEnumerator Run() {
		while(target.MoveNext()) {
			result = target.Current;
			yield return result;
		}
	}
}

