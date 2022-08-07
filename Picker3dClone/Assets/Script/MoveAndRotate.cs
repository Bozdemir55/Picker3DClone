using System;
using UnityEngine;


public class MoveAndRotate : MonoBehaviour
{
    public Vector3andSpace moveUnitsPerSecond;
    public Vector3andSpace rotateDegreesPerSecond;
    public bool ignoreTimescale;
    private float m_LastRealTime;


    private void Start()
    {
        m_LastRealTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        // if(!gameManager.ExecuteGame)
        // {
        //     return;
        // }


        float deltaTime = Time.fixedDeltaTime;
        if (ignoreTimescale)
        {
            deltaTime = (Time.realtimeSinceStartup - m_LastRealTime);
            m_LastRealTime = Time.realtimeSinceStartup;
        }
        transform.Translate(moveUnitsPerSecond.value * deltaTime, moveUnitsPerSecond.space);
        transform.Rotate(rotateDegreesPerSecond.value * deltaTime, moveUnitsPerSecond.space);


    }


    public void SpeedUpdate()
    {
        if (moveUnitsPerSecond.value.z == 15)
        {
            moveUnitsPerSecond.value.z = 25;
        }
        else
        {
            moveUnitsPerSecond.value.z = 15;
        }
    }


    [Serializable]
    public class Vector3andSpace
    {
        public Vector3 value;
        public Space space = Space.Self;
    }
}
