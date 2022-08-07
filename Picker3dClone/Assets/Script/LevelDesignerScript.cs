using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Lean.Pool;

public class LevelDesignerScript : MonoBehaviour
{
    public static LevelDesignerScript instance;

    public GameData gameDatas;
    public int camMoveInt;
    public Camera mainCamera;

    public GameObject roadPrefab;
    public List<GameObject> collectibleGameObjects;
    public List<GameObject> roadObjects;
    public List<GameObject> instantiateObjects;
    public List<int> targetValueList;

    public List<Vector3> collectibleameObjectPositions;
    private Vector3 roadPos;
    private const float offset = 90.05f;

    public TMP_InputField levelIDInput;
    public TMP_InputField targetValueInput;
    public Toggle moveToggle;
    public Toggle adCollectibleToggle;
    public TMP_Dropdown collectibleSelectDropDown;

    private float zPosPlatform;

    public GameObject road;
    public GameObject sphere;
    public GameObject pyramid;
    public GameObject cube;
    public GameObject cylinder;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mainCamera.transform.Translate(Vector3.forward * Time.deltaTime * camMoveInt);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.Translate(-Vector3.forward * Time.deltaTime * camMoveInt);
        }
        if (Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.Rotate(new Vector3(0, -Time.deltaTime * camMoveInt, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.Rotate(new Vector3(0, Time.deltaTime * camMoveInt, 0));
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            Vector3 gridPos = Vector3.zero;
            int layerMask = 1 << 8;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, layerMask))
            {
                gridPos = hit.point;
            }
            if (hit.transform != null)
            {
                if (adCollectibleToggle.isOn)
                {
                    CollectibleInstantiate(gridPos);
                }
            }
        }
    }
    public void CollectibleInstantiate(Vector3 gridPos)
    {

        switch (collectibleSelectDropDown.value)
        {
            case 0:
                GameObject sphere_Prefab = Instantiate(instantiateObjects[collectibleSelectDropDown.value], new Vector3(gridPos.x, 0.4f, gridPos.z), Quaternion.identity);
                sphere_Prefab.layer = 9;
                sphere_Prefab.GetComponent<CollectibleItem>().isLevelEditor = true;
                sphere_Prefab.GetComponent<CollectibleItem>().itemType = 0;
                collectibleGameObjects.Add(sphere_Prefab);
                break;
            case 1:
                GameObject trianglePyramid_Prefab = Instantiate(instantiateObjects[collectibleSelectDropDown.value], new Vector3(gridPos.x, 0.4f, gridPos.z), Quaternion.identity);
                trianglePyramid_Prefab.layer = 9;
                trianglePyramid_Prefab.GetComponent<CollectibleItem>().isLevelEditor = true;
                trianglePyramid_Prefab.GetComponent<CollectibleItem>().itemType = 1;
                collectibleGameObjects.Add(trianglePyramid_Prefab);
                break;
            case 2:
                GameObject cube_Prefab = Instantiate(instantiateObjects[collectibleSelectDropDown.value], new Vector3(gridPos.x, 0.4f, gridPos.z), Quaternion.identity);
                cube_Prefab.layer = 9;
                cube_Prefab.GetComponent<CollectibleItem>().isLevelEditor = true;
                cube_Prefab.GetComponent<CollectibleItem>().itemType = 2;
                collectibleGameObjects.Add(cube_Prefab);
                break;
            case 3:
                GameObject cylinder_Prefab = Instantiate(instantiateObjects[collectibleSelectDropDown.value], new Vector3(gridPos.x, 0.4f, gridPos.z), Quaternion.identity);
                cylinder_Prefab.layer = 9;
                cylinder_Prefab.GetComponent<CollectibleItem>().isLevelEditor = true;
                cylinder_Prefab.GetComponent<CollectibleItem>().itemType =3;
                collectibleGameObjects.Add(cylinder_Prefab);
                break;
        }
    }
    public void LoadEditScene()
    {
        if (int.TryParse(levelIDInput.text, out int levelıd))
        {

            PlayerPrefs.SetInt("LevelId", levelıd);
            gameDatas.levelInd = PlayerPrefs.GetInt("LevelId");
            LevelData levelData = gameDatas.levels.FirstOrDefault(r => r.levelind == levelıd);

            if (levelData.levelind == gameDatas.levelInd)
            {
                for (int i = 0; i < levelData.collectibles.Count; i++)
                {
                    switch (levelData.collectibles[i].collectibleType)
                    {
                        case 0:
                            GameObject sphereClone = Lean.Pool.LeanPool.Spawn(sphere);

                            sphereClone.transform.position = levelData.collectibles[i].pos;
                            sphereClone.layer = 9;
                            sphereClone.GetComponent<CollectibleItem>().isLevelEditor = true;
                            sphereClone.GetComponent<CollectibleItem>().itemType = 2;
                            collectibleGameObjects.Add(sphereClone);
                            break;
                        case 1:
                            GameObject pyramidClone = Lean.Pool.LeanPool.Spawn(pyramid);
                            pyramidClone.transform.position = levelData.collectibles[i].pos;
                            pyramidClone.layer = 9;
                            pyramidClone.GetComponent<CollectibleItem>().isLevelEditor = true;
                            pyramidClone.GetComponent<CollectibleItem>().itemType = 1;
                            collectibleGameObjects.Add(pyramidClone);
                            break;
                        case 2:
                            GameObject cubeClone = Lean.Pool.LeanPool.Spawn(cube);
                            cubeClone.transform.position = levelData.collectibles[i].pos;
                            cubeClone.layer = 9;
                            cubeClone.GetComponent<CollectibleItem>().isLevelEditor = true;
                            cubeClone.GetComponent<CollectibleItem>().itemType = 2;
                            collectibleGameObjects.Add(cubeClone);
                            break;
                        case 3:
                            GameObject cylinderClone = Lean.Pool.LeanPool.Spawn(cylinder);
                            cylinderClone.transform.position = levelData.collectibles[i].pos;
                            cylinderClone.layer = 9;
                            cylinderClone.GetComponent<CollectibleItem>().isLevelEditor = true;
                            cylinderClone.GetComponent<CollectibleItem>().itemType = 3;
                            collectibleGameObjects.Add(cylinderClone);
                            break;
                    }
                }
                for (int i = 0; i < levelData.platforms.Count; i++)
                {
                    GameObject roadClone = Lean.Pool.LeanPool.Spawn(road);
                    roadClone.transform.position = levelData.platforms[i].pos;
                    roadClone.layer = 8;
                    roadObjects.Add(roadClone);
                }
                var platforms = GameObject.FindGameObjectsWithTag("Platform");

                for (int i = 0; i < platforms.Length; i++)
                {
                    targetValueList.Add(levelData.targetValue[i]);
         
                }
            }


        }
    }
    public void AddPlane()
    {
        if (int.TryParse(targetValueInput.text, out int targetValue))
        {
            targetValueList.Add(targetValue);
            if (roadObjects.Count == 0)
            {
                zPosPlatform = 0;
                roadPos = new Vector3(0, 0, zPosPlatform);
                GameObject firstPlane = Instantiate(roadPrefab, roadPos, Quaternion.identity);
                firstPlane.GetComponent<PlatformScript>().levelDesing = true;
                //if (editToggle.isOn)
                //    firstPlane.GetComponent<PanelController>().EnableMovement();
                firstPlane.layer = 8;
                roadObjects.Add(firstPlane);
                return;
            }

            Vector3 lastRoadPos = roadObjects[roadObjects.Count - 1].transform.position;
            zPosPlatform = lastRoadPos.z + offset;
            roadPos = new Vector3(lastRoadPos.x, lastRoadPos.y, zPosPlatform);
            GameObject plane = Instantiate(roadPrefab, roadPos, Quaternion.identity);
            plane.GetComponent<PlatformScript>().levelDesing = true;
            plane.layer = 8;
            //if (editToggle.isOn || selectToggle.isOn)
            //    plane.GetComponent<PanelController>().EnableMovement();
            roadObjects.Add(plane);
        }
        else
        {
        }

    }
    public void Save()
    {

        if (int.TryParse(levelIDInput.text, out int levelId))
        {
            LevelData levelData = gameDatas.levels.FirstOrDefault(r => r.levelind == levelId);
            if (levelData != null)
            {
                gameDatas.levels.Remove(levelData);
            }
            else
            {
                levelData = new LevelData();
                //   gameDatas.levels.Add(levelData);
            }
            levelData.collectibles = new List<Collectible>();
            levelData.platforms = new List<Platforms>();
            levelData.targetValue = new List<int>();
            levelData.levelind = levelId;
            for (int i = 0; i < collectibleGameObjects.Count; i++)
            {
                levelData.collectibles.Add(new Collectible { collectibleType = collectibleGameObjects[i].gameObject.GetComponent<CollectibleItem>().itemType, pos = collectibleGameObjects[i].transform.position });
            }
            for (int i = 0; i < roadObjects.Count; i++)
            {
                levelData.platforms.Add(new Platforms { pos = roadObjects[i].transform.position });
            }
            for (int i = 0; i < targetValueList.Count; i++)
            {
                levelData.targetValue.Add(targetValueList[i]);
            }
            gameDatas.levels.Add(levelData);
        }
    }
}
