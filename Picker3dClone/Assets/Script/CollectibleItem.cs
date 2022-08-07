using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private LevelDesignerScript levelDesignerScript;

    Vector3 startPos;
    Vector3 dist;

    public int itemType;
   
    public bool isLevelEditor;
    private void Start()
    {
        levelDesignerScript = FindObjectOfType<LevelDesignerScript>();
    }
    public void Par()
    {
        if (transform.childCount>0)
        {
            GameObject parObje = transform.GetChild(0).gameObject;
            transform.GetChild(0).transform.parent = null;

            parObje.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (LevelDesignerScript.instance != null)
        {
            if (LevelDesignerScript.instance.moveToggle.isOn && isLevelEditor)
            {
                Transform objTransform = transform;
                startPos = Camera.main.WorldToScreenPoint(objTransform.position);
                dist = objTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z));
            }
        }
    }

    private void OnMouseDrag()
    {
        if (LevelDesignerScript.instance != null)
        {
            if (LevelDesignerScript.instance.moveToggle.isOn && isLevelEditor)
            {
                Transform objTransform = transform;
                Vector3 lastPos = new Vector3(Input.mousePosition.x, 1 + Input.mousePosition.y, startPos.z);//+Mathf.Clamp(Input.mousePosition.y/200, -15, 15));
                objTransform.position = Camera.main.ScreenToWorldPoint(lastPos) + dist;
            }
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            levelDesignerScript.collectibleGameObjects.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
