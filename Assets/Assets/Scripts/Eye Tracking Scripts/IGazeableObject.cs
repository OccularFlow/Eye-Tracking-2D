/*
- gazeAction()      This method is called when the user has successfully gazed at the object for longer amount than the gazeTime
- startedGazing()   This method is called when the user starts to gaze at an object - good for implementing visual feedback for the user
- stoppedGazing()   This method is called when the user looks away from an object after they have started gazing at it
- getGazeTime()     This should return the time that the user has to look at the GameObject for in order to register a successful gazeAction.
*/

interface IGazeableObject
{
    void gazeAction();

    void currentlyGazing();

    void stoppedGazing();

    float getGazeTime();
}