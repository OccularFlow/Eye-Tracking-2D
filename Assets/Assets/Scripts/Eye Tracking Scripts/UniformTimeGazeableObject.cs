using System.Collections;
using UnityEngine;
using Tobii.Gaming;

/*
This class works similarly to the GazeAware2d and GazeableObject classes combined
The key difference is that class uses a coroutine instead of the MonoBehaviour Update() method
This enables GameObjects to behave normally even when Time.timescale is changed
This can be very useful in scenes such as pause menus.

Drag and drop this script onto a GameObject for the GameObject to register gaze inputs independent of Time.timescale.
For this script to work, the user must implement the IGazeableObject interface to the other script attached to the gameObject.

*/
public class UniformTimeGazeableObject : MonoBehaviour {
    IGazeableObject gazeableObject;
    Collider2D objectCollider;
    bool objectBeingGazed = false;
    bool startedGaze = false;
    float timer = 0f;
    Camera mainCamera;

    void Awake() {
        objectCollider = GetComponent<BoxCollider2D>();
        gazeableObject = GetComponent<IGazeableObject>();
        if (!TobiiAPI.IsConnected) {
            enabled = false;
        }
        mainCamera = Camera.main;
    }

    void Start() {
        StartCoroutine("checkForGaze");
    }

    float getGazeTime() {
        return gazeableObject.getGazeTime();
    }
    
    protected void gazeAction() {
        gazeableObject.gazeAction();
    }

    protected void startedGazing() {
        gazeableObject.currentlyGazing();
    }
    protected void stoppedGazing() {
        gazeableObject.stoppedGazing();
    }

    IEnumerator checkForGaze() { 
        float startTime = Time.unscaledTime;
        while (true) {
            if (!TobiiAPI.GetUserPresence().IsUserPresent()) {
                timer = 0f;
                yield return new WaitForSecondsRealtime(0.05f);
                continue;
            }
            yield return new WaitForSecondsRealtime(0.05f);
            timer = Time.unscaledTime - startTime;
            if (mainCamera!= null && objectCollider.bounds.IntersectRay(mainCamera.ScreenPointToRay(TobiiAPI.GetGazePoint().Screen))) {
                startedGaze = true;
                if (timer > getGazeTime() && !objectBeingGazed) {
                    objectBeingGazed = true;
                    gazeAction();
                } else if (!objectBeingGazed){
                    startedGazing();
                }
            } else {
                timer = 0f;
                startTime = Time.unscaledTime;
                if (startedGaze)
                    stoppedGazing();
                startedGaze = false;
                objectBeingGazed = false;
            }
        }
    }
}
