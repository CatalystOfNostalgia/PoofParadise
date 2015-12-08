using System;

/**
 * Use this class to save info between scenes.
 * currently only stores the user info so I'm gonna kill it after we get this
 * info because jeremy hates objects
 */
public class SceneState : Manager {
    
    // singleton
    public static SceneState state;

    public String userInfo { get; set; }

    /**
     * Overrides the start functionality
     * provided by manager
     */
    public override void Start() {

        if (state == null) {
            DontDestroyOnLoad(gameObject);
            state = this;
        } else if (state != this) {
            Destroy(gameObject);
        }
    }
}
