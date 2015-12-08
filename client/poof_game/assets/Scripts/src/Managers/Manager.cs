using UnityEngine;

/**
 * An abstract managers object
 * from which all managers can be related
 */
public abstract class Manager : MonoBehaviour {

    /**
     * All managers must contain some form of start to
     * initialize their singleton behavior
     *
     * Serves as a reality check
     */
    abstract public void Start();
}
