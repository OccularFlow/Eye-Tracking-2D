
/*
This is the default subclass of the GazeAware2d class. 
Drag and drop this script onto a GameObject for the GameObject to register gaze inputs.

For this script to work, the user must implement the IGazeableObject interface to the other script attached to the gameObject.
*/
public class GazeableObject : GazeAware2d
{
    IGazeableObject gazeableObject;

    protected override void Awake() {
        base.Awake();
        gazeableObject = GetComponent<IGazeableObject>();
    }
    void Start() {
        gazeTime = gazeableObject.getGazeTime();
    }

    void OnEnable() {
        gazeTime = gazeableObject.getGazeTime();
    }
    
    protected override void gazeAction() {
        gazeableObject.gazeAction();
    }

    protected override void startedGazing() {
        gazeableObject.currentlyGazing();
    }
    protected override void stoppedGazing() {
        gazeableObject.stoppedGazing();
    }
}
