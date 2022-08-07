using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    public GameObject nearest;
    public TextMeshProUGUI levelEndText;
    public Image progressFill;
    public float levelTargetValue;
    public float levelCollectCountValue;
    public float fillAmountValue;
    public GameObject swipeTutorial;

    public TextMeshProUGUI lastLevelID;
    public TextMeshProUGUI nextLevelID;

    public GameObject levelCompllete;
    public GameObject levelFail;
    public GameObject startLevelUı;
    public LevelData levelData;


    [Header("ProgresBar")]
    public Transform levelEndtransform;
    public Transform pickerTransform;
    public float fullDistance;

    // Start is called before the first frame update
    public void Setup()
    {
        var distance = Mathf.Infinity;
        var position = GameManager.instance.pickerScript.transform.position;
        var levelEndobjects = GameObject.FindGameObjectsWithTag("LevelEnd");
        foreach (var canvas in levelEndobjects)
        {
            var diff = (canvas.transform.position - position);
            var curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nearest = canvas.gameObject;
                distance = curDistance;
            }
        }
        levelEndText = nearest.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        levelEndText.text = "0/" + GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().targetValue;

        if (GameManager.instance.gameDatas.levelInd == 1 && !GameManager.instance.touched)
        {
            startLevelUı.SetActive(true);
        }
        lastLevelID.text = GameManager.instance.gameDatas.lastPlayedLevelInd.ToString();
        nextLevelID.text = (GameManager.instance.gameDatas.levelInd + 1).ToString();

        levelData = GameManager.instance.gameDatas.levels.FirstOrDefault(r => r.levelind == GameManager.instance.gameDatas.levelInd);

    }
    public void FullDistanceCall()
    {
        pickerTransform = GameManager.instance.pickerScript.transform;
        fullDistance = GetDistance();
    }
    // Update is called once per frame
    void Update()
    {
        if (levelEndtransform != null)
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(fullDistance, 0, newDistance);
            SetProgressFillBar(progressValue);
        }

    }
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        levelFail.SetActive(true);
    }
    private float GetDistance()
    {
        return (levelEndtransform.position - pickerTransform.position).sqrMagnitude;
    }
    public void LevelCompllete(List<GameObject> parObject)
    {
        if (levelData.levelind == GameManager.instance.gameDatas.levelInd)
        {
            if (GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().platformID >= (levelData.targetValue.Count - 1))
            {
                nearest.transform.GetChild(1).gameObject.SetActive(true);

                levelCompllete.SetActive(true);
                progressFill.transform.parent.gameObject.SetActive(false);

            }
            else
            {

                GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().scaleObject.SetActive(true);
                foreach (var item in parObject)
                {
                    item.GetComponent<CollectibleItem>().Par();
                }
                GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().door.GetComponent<DoorScript>().RotateMethod();
            }
        }

    }
    public IEnumerator LevelEndCall()
    {

        yield return new WaitForSeconds(.1f);
        GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().levelEndPlatform.transform.DOMoveY(0, 1f).SetEase(Ease.Linear).OnComplete(() =>
         {
             //for (int i = 0; i < GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().scaleObject.transform.childCount; i++)
             //{
             //    GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().scaleObject.transform.GetChild(i).gameObject.SetActive(true);
             //}
             GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().confetti.SetActive(true);
             GameManager.instance.pickerScript.ScalePicker();
         });
    }
    public void SetFiilUI()
    {
        fillAmountValue = 0;
        DOTween.To(() => progressFill.fillAmount, x => progressFill.fillAmount = x, fillAmountValue, 1.5f);
    }
    public void NextLevelButton()
    {
        // ScripTable objectte levelID lerimi tutuyorum
        GameManager.instance.gameDatas.levelInd++;
        GameManager.instance.gameDatas.lastPlayedLevelInd++;

        // PlayerPrefs e kaydettim ki cdd de istenen oyundan çıkıp geri dönüldüğünde aynı leveldan başlayabileyeyim
        PlayerPrefs.SetInt("LevelId", GameManager.instance.gameDatas.levelInd);
        PlayerPrefs.SetInt("LastLevelId", GameManager.instance.gameDatas.lastPlayedLevelInd);


        lastLevelID.text = GameManager.instance.gameDatas.lastPlayedLevelInd.ToString();
        nextLevelID.text = (GameManager.instance.gameDatas.levelInd + 1).ToString();

        SetCanvas();
        //GameManager.instance.pickerScript.transform.GetComponent<Rigidbody>().isKinematic = false;
        SceneManager.LoadScene(0);
    }
    public void RetryButton()
    {
        SetCanvas();
        SceneManager.LoadScene(0);
    }
    private void SetCanvas()
    {
        SetFiilUI();
        progressFill.transform.parent.gameObject.SetActive(true);
        levelFail.SetActive(false);
        levelCompllete.SetActive(false);
    }
    public void SetProgressFillBar(float value)
    {
        DOTween.To(() => progressFill.fillAmount, x => progressFill.fillAmount = x, value, 1f).OnComplete(() =>
        {

        });
    }
    public void SetProgressBar(List<GameObject> parObjects)
    {
        if ((GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().scaleObject.transform.parent.GetChild(0).GetComponent<LevelEndScript>().collectCount < GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().targetValue))
        {
            StartCoroutine(GameOver());
        }
        else
        {
            LevelCompllete(parObjects);
        }
        //print("Fill : " + ((float)GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().scaleObject.transform.parent.GetChild(0).GetComponent<LevelEndScript>().collectCount / (float)GameManager.instance.pickerScript.activePlatform.GetComponent<PlatformScript>().targetValue));

    }
}
