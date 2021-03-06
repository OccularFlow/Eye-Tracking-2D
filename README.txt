Eye Tracking 2D

You can either copy the folder Assets/Assets/Scripts to your own project or download this folder and open it in Unity.

This asset requires the 'Tobii Eye Tracking SDK' asset and relevant eye tracking hardware.

The example scene shows a square that will change colours when gazing at it.

To enable a 2D game object to become eye tracking compatible, you will need a Collider2D component added to the GameObject.
A script must be created which has a class which inherits the IGazeableObject interface. This interface has the following methods:
- gazeAction()      This method is called when the user has successfully gazed at the object for longer amount than the gazeTime
- startedGazing()   This method is called when the user starts to gaze at an object - good for implementing visual feedback for the user
- stoppedGazing()   This method is called when the user looks away from an object after they have started gazing at it
- getGazeTime()     This should return the time that the user has to look at the GameObject for in order to register a successful gazeAction.

Once this script has been created, add it to your GameObject and then add the GazeableObject script to the GameObject as well.
Your GameObject can now be interacted with via eye tracking.

If you have a scene where Time.timescale is being adjusted (such as a Pause Menu where it may be set to 0), you will need to use the UniformTimeGazeableObject script instead of the GazeableObject script.
