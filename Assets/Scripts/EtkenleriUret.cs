using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EtkenleriUret : MonoBehaviour
{
    GameManager gameManager;
    public GameObject[] agaclar;
    public Transform[] konumlar;
    public Image agacButonImage;
    public Image agacButonAgacImage;
    public AudioSource audioSource;
    PlayScene playScene;
    bool uretilebilir = true;
    int rndKonum;
    public int agacSayisi = 0;
    int sayacClip = 0;

    public void AgacUret()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(BekletUret());
        rndKonum = Random.Range(0, konumlar.Length);
        agacSayisi++;
        
    }
    IEnumerator BekletUret()
    {
        if (uretilebilir)
        {
            audioSource.Play();
            if (audioSource.time <= 95)
            {
                audioSource.time = sayacClip++;
            }
            else
            {
                audioSource.time = 0;
            }
            Instantiate(agaclar[Random.Range(0, agaclar.Length)], konumlar[rndKonum]);
            uretilebilir = false;
            agacButonImage.color = Color.grey;
            agacButonAgacImage.color = Color.grey;
            yield return new WaitForSeconds(1f);
            uretilebilir = true;
            audioSource.Stop();
            agacButonImage.color = Color.white;
            agacButonAgacImage.color = Color.white;
        }
    }
}
