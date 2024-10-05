using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ScreenSet : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject videoPanel;

    private void Awake()
    {
        Invoke("framelimit", 1f);
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "showWatagames")
            StartCoroutine(videoPlayingCheck());
    }

    void framelimit()
    {
        Application.targetFrameRate = 60;
    }

    public void DataClear()
    {
        DataBase.DB.playerData = new PlayerData();
        DataBase.DB.playerData.itemDatas = new List<ItemData>();
    }

    IEnumerator videoPlayingCheck()
    {
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => videoPlayer.isPlaying == false);
        videoPanel.SetActive(false);
        SceneManager.LoadScene("Title");
    }
}
