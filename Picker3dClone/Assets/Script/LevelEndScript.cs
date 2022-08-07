using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelEndScript : MonoBehaviour
{
    public int collectCount;
    public bool setCanvas;
    public int collectiblegameObjectsCount;
    public List<GameObject> particleObjects;
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

        if (other.transform.CompareTag("Sphere") && !setCanvas)
        {
            collectCount++;
            GameManager.instance.voiceManager.PlayCollectVoice();
            GameManager.instance.uIManager.levelCollectCountValue++;
            particleObjects.Add(other.gameObject);
            if (collectCount >= collectiblegameObjectsCount)
            {
                if (collectCount >= GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().targetValue)
                {
                    setCanvas = true;
                    GameManager.instance.uIManager.SetProgressBar(particleObjects);
                }
                else
                {
                    GameManager.instance.uIManager.SetProgressBar(particleObjects);
                }
            }
            ObjectManager.instance.collectiblegameObjects.Remove(other.gameObject);

            //if (other.GetComponent<BoxCollider>())
            //{
            //    other.GetComponent<BoxCollider>().enabled = true;
            //}
            //if (other.GetComponent<MeshCollider>())
            //{
            //    other.GetComponent<MeshCollider>().enabled = true;
            //}
            //if (other.GetComponent<SphereCollider>())
            //{
            //    other.GetComponent<SphereCollider>().enabled = true;
            //}
            GameManager.instance.uIManager.fillAmountValue = (float)GameManager.instance.uIManager.levelCollectCountValue / (float)GameManager.instance.uIManager.levelTargetValue;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = collectCount + "/" + GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().targetValue;
        }
    }
}
