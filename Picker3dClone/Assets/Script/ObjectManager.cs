using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public GameObject road;
    public GameObject sphere;
    public GameObject pyramid;
    public GameObject cylinder;
    public GameObject cube;
    public List<GameObject> sceneOjects;
    public List<GameObject> collectiblegameObjects;
    public List<GameObject> roads;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

    }
    public void Setup()
    {
        if (GameManager.instance.gameDatas.levelInd <= GameManager.instance.gameDatas.levels.Count)
        {
            foreach (var leveldata in GameManager.instance.gameDatas.levels)
            {
                if (leveldata.levelind == GameManager.instance.gameDatas.levelInd)
                {
                    for (int i = 0; i < leveldata.collectibles.Count; i++)
                    {
                        switch (leveldata.collectibles[i].collectibleType)
                        {
                            case 0:
                                GameObject sphereClone = Lean.Pool.LeanPool.Spawn(sphere);
 
                                sphereClone.transform.position = leveldata.collectibles[i].pos;
                                sceneOjects.Add(sphereClone);
                                break;
                            case 1:
                                GameObject pyramidClone = Lean.Pool.LeanPool.Spawn(pyramid);
                                pyramidClone.transform.position = leveldata.collectibles[i].pos;
                                sceneOjects.Add(pyramidClone);
                                break;
                            case 2:
                                GameObject cubeClone = Lean.Pool.LeanPool.Spawn(cube);
                                cubeClone.transform.position = leveldata.collectibles[i].pos;
                                sceneOjects.Add(cubeClone);
                                break;
                            case 3:
                                GameObject cylinderClone = Lean.Pool.LeanPool.Spawn(cylinder);
                                cylinderClone.transform.position = leveldata.collectibles[i].pos;
                                sceneOjects.Add(cylinderClone);
                                break;
                        }
                    }
                    for (int i = 0; i < leveldata.platforms.Count; i++)
                    {
                        GameObject roadClone = Lean.Pool.LeanPool.Spawn(road);
                        roadClone.transform.position = leveldata.platforms[i].pos;
                        sceneOjects.Add(roadClone);
                        roads.Add(roadClone);
                    }
                    var platforms = GameObject.FindGameObjectsWithTag("Platform");
                    
                    for (int i = 0; i < platforms.Length; i++)
                    {
                        GameManager.instance.uIManager.levelTargetValue += leveldata.targetValue[i];
                        platforms[i].GetComponent<PlatformScript>().targetValue = leveldata.targetValue[i];
                        platforms[i].GetComponent<PlatformScript>().platformID = i;
                    }
                }
            }
        }
        else
        {
            int randomizeLevel = Random.Range(1, GameManager.instance.gameDatas.levels.Count);
            GameManager.instance.gameDatas.levelInd = randomizeLevel;
            Setup();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnApplicationQuit()
    {
        GameManager.instance.gameDatas.levelInd = PlayerPrefs.GetInt("LevelId");
        GameManager.instance.gameDatas.lastPlayedLevelInd = PlayerPrefs.GetInt("LastLevelId");
    }
    private void OnApplicationPause()
    {
        GameManager.instance.gameDatas.levelInd = PlayerPrefs.GetInt("LevelId");
        GameManager.instance.gameDatas.lastPlayedLevelInd = PlayerPrefs.GetInt("LastLevelId");
    }
}
