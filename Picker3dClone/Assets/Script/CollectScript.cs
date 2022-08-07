using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnTriggerEnter(Collider other)
    {

        if (transform.CompareTag("Collect") && other.transform.CompareTag("Collectible"))
        {
            //print(other.transform.name);
            other.transform.tag = "Sphere";
            other.transform.parent = transform.parent;
            GameManager.instance.voiceManager.PlayCollectVoice();
            ObjectManager.instance.collectiblegameObjects.Add(other.gameObject);
        }
        if (other.transform.CompareTag("Platform"))
        {
            GameManager.instance.pickerScript.activePlatform = other.gameObject;
        }
    }
}
