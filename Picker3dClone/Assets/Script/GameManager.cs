using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData gameDatas;
    public PickerScript pickerScript;
    public VoiceManager voiceManager;
    [HideInInspector] public UIManager uIManager;
    public bool touched;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        touched = false;
    }
    void Start()
    {

        pickerScript = FindObjectOfType<PickerScript>();
        uIManager = FindObjectOfType<UIManager>();
        FindObjectOfType<VoiceManager>()?.Setup();
        gameDatas.pickerData.pickerPos = pickerScript.transform.position;
        StartCoroutine(LoadSceneDelay());
    }
    IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(.1f);
        if (PlayerPrefs.HasKey("LevelId"))
        {
            gameDatas.levelInd = PlayerPrefs.GetInt("LevelId");
            gameDatas.lastPlayedLevelInd = PlayerPrefs.GetInt("LastLevelId");
        }
        else
        {
            PlayerPrefs.SetInt("LevelId", 1);
            PlayerPrefs.SetInt("LastLevelId",1);
            gameDatas.lastPlayedLevelInd = PlayerPrefs.GetInt("LastLevelId");
            gameDatas.levelInd = PlayerPrefs.GetInt("LevelId");

            if (gameDatas.levelInd==1)
            {
                uIManager.swipeTutorial.SetActive(true);
            }
        }

        ObjectManager.instance.Setup();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touched = true;
            GameManager.instance.uIManager.swipeTutorial.SetActive(false);
            uIManager.startLevelUı.SetActive(false);
            GameManager.instance.uIManager.Setup();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
