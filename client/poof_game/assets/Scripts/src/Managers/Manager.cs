using UnityEngine;
using System.Collections;

public abstract class Manager : MonoBehaviour {

    /**
     * All managers must contain some form of start to
     * initialize their singleton behavior
     *
     * Serves as a reality check
     */
    abstract public void Start();
}
