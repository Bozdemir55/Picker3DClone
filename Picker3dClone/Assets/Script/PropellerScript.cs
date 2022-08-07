using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerScript : MonoBehaviour
{
    public GameObject parentParticle;
    public ParticleSystem triggerParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Picker"))
        {

            other.transform.GetComponent<PickerScript>().propeller.SetActive(true);
            parentParticle.SetActive(false);
            triggerParticle.Play();
            gameObject.SetActive(false);
        }
    }
}
