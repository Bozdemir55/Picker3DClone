using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickerScript : MonoBehaviour
{
    public Rigidbody rb;

    Vector3 firstPos, endPos;
    public float playerSpeed;
    public bool isStoped;
    private float swipeValue;
    public GameObject activePlatform;
    public GameObject leftpropeller;
    public GameObject rightpropeller;
    public GameObject propeller;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.94f, 3.94f), transform.position.y, transform.position.z);
        if (!isStoped && GameManager.instance.touched)
        {
            rb.velocity = Vector3.forward * 7.5f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            float distanceX = endPos.x - firstPos.x;

            //Burada fizik çarpışmalarını daha performanslı alabilmek için velocitye clamp atarak istediğim seviyeye getirebildim taşıma mekaniğini
            swipeValue = Mathf.Clamp(distanceX / playerSpeed, -5, 5);
            if (distanceX < 0)
            {
                rb.velocity = new Vector3(swipeValue, (0), 7.5f);
            }
            else if (distanceX > 0)
            {
                rb.velocity = new Vector3(swipeValue, (0), 7.5f);
            }
        }
        else if(GameManager.instance.touched)
        {
            rb.velocity = new Vector3(0, 0, 7.5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (transform.CompareTag("Picker") && other.transform.CompareTag("LevelEnd"))
        {
            rb.isKinematic = true;
            isStoped = true;
            other.tag = "Untagged";
            
            leftpropeller.GetComponent<BoxCollider>().enabled = false;
            rightpropeller.GetComponent<BoxCollider>().enabled = false;

            other.transform.GetChild(0).GetComponent<LevelEndScript>().collectiblegameObjectsCount = ObjectManager.instance.collectiblegameObjects.Count;
            foreach (var item in ObjectManager.instance.collectiblegameObjects)
            {
                item.GetComponent<Rigidbody>().AddForce(Vector3.forward * 475, ForceMode.Force);
            }
        }
    }
    public void ScalePicker()
    {
        //Sucsess hissini vermek için picker ı scale yaptım
        transform.DOScale(new Vector3(1.2f, 1.2f,1.2f), .5f).SetEase(Ease.Linear).OnComplete(() =>
           {
               transform.DOScale(Vector3.one, .5f).SetEase(Ease.Linear).OnComplete(() =>
               {
                   GameManager.instance.pickerScript.rb.isKinematic = false;
                   GameManager.instance.pickerScript.isStoped = false;
                   leftpropeller.GetComponent<BoxCollider>().enabled = true;
                   rightpropeller.GetComponent<BoxCollider>().enabled = true;
                   //GameManager.instance.uIManager.SetFiilUI();
               });
           });
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            activePlatform = collision.gameObject;
        }      
    }
}
