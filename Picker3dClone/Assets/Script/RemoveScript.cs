using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveScript : MonoBehaviour
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

        if (transform.CompareTag("PickerRemove") && other.transform.CompareTag("Collectible"))
        {

            other.gameObject.SetActive(false);
            ObjectManager.instance.collectiblegameObjects.Remove(other.gameObject);
        }
        if (transform.CompareTag("Picker") && other.transform.CompareTag("Sphere"))
        {

            other.gameObject.SetActive(false);
            ObjectManager.instance.collectiblegameObjects.Remove(other.gameObject);
        }
    }
}
