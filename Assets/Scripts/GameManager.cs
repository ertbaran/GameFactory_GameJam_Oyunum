using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    AgacScript agacScript;
    EtkenleriUret etkenleriUret;
    public Sprite dunyaSpriteHappy, dunyaSpriteSad;
    public GameObject dunya, panel, effect1, effect2, oyunSonuPanel, agacUretButon, kureselIsinmaText, menuButton;
    public Text kureselIsinmaDegerText, agacSayisiText;
    public float isinmaMiktar = 0;
    int pandemi = 0;
    public int ilerlemeSayac = 0;
    public AudioSource audioSource;
    public AudioClip coughClip;
    bool oyunDevam = true;
    public float isinmaKatsayi = 1;
    private void Awake()
    {
        dunya.SetActive(false);
        effect1.SetActive(false);
        effect2.SetActive(false);
        menuButton.SetActive(false);
    }
    void Start()
    {
        InvokeRepeating("KureselIsinma", 3, 3f);    //renk değişim süre aralığı değişebilir
        agacScript = FindObjectOfType<AgacScript>();
        etkenleriUret = FindObjectOfType<EtkenleriUret>();
        SceneRestart();
    }

    void Update()
    {
        Debug.Log(isinmaMiktar);
        dunya.transform.Rotate(0, 0, 0.035f);
        kureselIsinmaDegerText.text = "% " + isinmaMiktar.ToString("F1");
        if (isinmaMiktar < 100)
        {
            isinmaMiktar += 0.01f * 2;
        }
        else if (isinmaMiktar <= 0)
        {
            isinmaMiktar = 0;
        }
        else
        {
            isinmaMiktar = 100;
        }
        if (isinmaMiktar >= 50)
        {
            dunya.GetComponent<SpriteRenderer>().sprite = dunyaSpriteSad;
        }
        if (isinmaMiktar <= 50)
        {
            dunya.GetComponent<SpriteRenderer>().sprite = dunyaSpriteHappy;
        }

        if (isinmaMiktar >= 100 && oyunDevam)    //Gameover
        {
            if (GameObject.FindWithTag("Agac"))
            {
                Destroy(GameObject.FindWithTag("Agac"));
            }
            oyunDevam = false;
            GameOver();
        }
    }
    void GameOver()
    {
        audioSource.PlayOneShot(coughClip);
        agacSayisiText.text = etkenleriUret.agacSayisi.ToString();
        dunya.SetActive(false);
        agacUretButon.SetActive(false);
        kureselIsinmaText.SetActive(false);
        kureselIsinmaDegerText.gameObject.SetActive(false);
        menuButton.SetActive(false);
        oyunSonuPanel.SetActive(true);

    }
    void KureselIsinma()
    {

        if (isinmaMiktar < 100)
        {
            isinmaMiktar += 5;
            Color lastColor = dunya.GetComponent<SpriteRenderer>().color;
            dunya.GetComponent<SpriteRenderer>().color = Color.Lerp(lastColor, Color.red, isinmaMiktar * 0.0035f);
        }
    }
    void PandemiEtkenleri()
    {
        pandemi++;
        Color lastColor = dunya.GetComponent<SpriteRenderer>().color;
        dunya.GetComponent<SpriteRenderer>().color = Color.Lerp(lastColor, Color.magenta, 0.01f);
    }
    void SceneRestart()
    {
        StartCoroutine(SceneTransition());
    }
    public IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(0.5f);
        panel.transform.DOLocalMoveX(-1270, 1);
        yield return new WaitForSeconds(0.3f);
        dunya.SetActive(true);
        effect1.SetActive(true);
        effect2.SetActive(true);
        menuButton.SetActive(true);
        yield return new WaitForSeconds(2);
        Destroy(panel);
    }
}
