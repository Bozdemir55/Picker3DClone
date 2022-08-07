using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformScript : MonoBehaviour
{
    public int targetValue;
    public int platformID;
    public TextMeshProUGUI textFinal;
    public GameObject scaleObject;
    public GameObject levelEndPlatform;
    public GameObject door;
    public GameObject confetti;
    public int maxID;
    public bool levelDesing;
    // Start is called before the first frame update
    void Start()
    {
        if (!levelDesing)
        {
            foreach (var platform in ObjectManager.instance.roads)
            {

                if (platform.GetComponent<PlatformScript>().platformID > maxID)
                {
                    maxID = platform.GetComponent<PlatformScript>().platformID;
                }

            }
            if (maxID == platformID)
            {
                GameManager.instance.uIManager.levelEndtransform = door.transform;
                GameManager.instance.uIManager.FullDistanceCall();
                door.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
