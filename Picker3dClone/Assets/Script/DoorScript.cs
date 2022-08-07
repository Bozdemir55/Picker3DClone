using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftDoor;
    public GameObject rightDoor;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RotateMethod()
    {
        leftDoor.transform.DORotate(new Vector3(0,0, 90), 1f, RotateMode.Fast).SetEase(Ease.Linear);
        rightDoor.transform.DORotate(new Vector3(0, 0,-90), 1f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => 
        {
           GameManager.instance.uIManager.StartCoroutine(GameManager.instance.uIManager.LevelEndCall());
        });
    }
}
