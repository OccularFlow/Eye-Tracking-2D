using UnityEngine;
using Tobii.Gaming;
/*
This abstract class has all the code used to register gaze input on GameObjects
A Collider2D component must be added to the GameObject in order for this script to register gaze input
Subclasses of this class must implement three methods:
- gazeAction()      This method is called when the user has successfully gazed at the object for longer amount than the gazeTime
- startedGazing()   This method is called when the user starts to gaze at an object - good for implementing visual feedback for the user
- stoppedGazing()   This method is called when the user looks away from an object after they have started gazing at it

The default subclass of this class is the GazeableObject class

Since this class uses the Update() method from the MonoBehaviour class, it will be affected by changes to Time.timescale
If you require your script to not be affected by Time.timescale, use the UniformTimeGazeableObject class
*/
public abstract class GazeAware2d : MonoBehaviour
{
    Collider2D objectCollider;
    bool objectBeingGazed = false;
    bool startedGaze = false;
    float timer = 0f;
    protected float gazeTime;
    Camera mainCamera;

    protected abstract void gazeAction();
    protected abstract void startedGazing();
    protected abstract void stoppedGazing();
    protected virtual void Awake() {
        objectCollider = GetComponent<Collider2D>();
        if (!TobiiAPI.IsConnected || objectCollider == null) {
            enabled = false;
        } 
        mainCamera = Camera.main;
    }
    protected void Update() {
        if (!TobiiAPI.GetUserPresence().IsUserPresent()) {
            timer = 0f;
            return;
        }
        timer += Time.deltaTime;
        if (objectCollider.bounds.IntersectRay(mainCamera.ScreenPointToRay(TobiiAPI.GetGazePoint().Screen))) {
            startedGaze = true;
            if (timer > gazeTime && !objectBeingGazed) {
                objectBeingGazed = true;
                gazeAction();
            } else if (!objectBeingGazed){
                startedGazing();
            }
        } else {
            timer = 0f;
            if (startedGaze)
                stoppedGazing();
            startedGaze = false;
            objectBeingGazed = false;
        }
    }
}
