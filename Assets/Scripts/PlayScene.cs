using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayScene : MonoBehaviour
{
    public GameObject panel, particles, mirrorMain, teamAeria;
    public AudioSource music;
    public Text playText;
    public GameObject gameNameText;
    public GameObject mirrorPanel;
    public void Play()
    {
        StartCoroutine(SceneTransition());
    }
    private void Start()
    {
        gameNameText.transform.DOMoveX(0f, 0.9f).SetEase(Ease.OutElastic);
        mirrorPanel.transform.DOMoveX(-1.85f, 1.7f);   
    }
    IEnumerator SceneTransition()
    {
        playText.color = Color.grey;
        playText.transform.DOScaleX(0.35f, 0.8f).SetEase(Ease.OutBounce);
        playText.transform.DOScaleY(1.2f, 0.8f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.5f);
        //DontDestroyOnLoad(music);
        panel.transform.DOLocalMoveX(193, 1);
        yield return new WaitForSeconds(0.5f);
        particles.SetActive(false);
        mirrorMain.SetActive(false);
        teamAeria.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
