using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AgacScript : MonoBehaviour
{
    GameManager gameManager;
    public GameObject agacParticle;
    public int agacMoveSpeed = 20;
    float rotateSpeed = 5;
    GameObject efekt;
    GameObject audioSource;
    public AudioClip spraySound, yaprakSound;
    bool sprayMusait = true;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GameObject.Find("SoundManager");
    }
    private void Update()
    {
        Vector2 direction = transform.position - gameManager.dunya.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        gameObject.GetComponent<Rigidbody2D>().AddForce(-direction / 60 * agacMoveSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Dunya")
        {
            if (gameManager.isinmaMiktar > 10)
            {
                gameManager.isinmaMiktar -= 10;
            }
            else if (gameManager.isinmaMiktar <= 10 && gameManager.isinmaMiktar > 5)
            {
                gameManager.isinmaMiktar -= 5;
            }
            else if (gameManager.isinmaMiktar <= 5 && gameManager.isinmaMiktar > 0)
            {
                gameManager.isinmaMiktar -= 1;
            }
            audioSource.GetComponent<AudioSource>().PlayOneShot(yaprakSound);
            StartCoroutine(Gumgum());

            if (sprayMusait)
            {
                audioSource.GetComponent<AudioSource>().volume = 0.43f;
            }
            StartCoroutine(SprayTime(Random.Range(3, 50)));
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.transform.SetParent(collision.gameObject.transform);
            efekt = Instantiate(agacParticle, gameObject.transform);
            Color lastColor = gameManager.dunya.GetComponent<SpriteRenderer>().color;
            gameManager.dunya.GetComponent<SpriteRenderer>().color = Color.Lerp(lastColor, Color.white, 0.1f);
        }
    }
    IEnumerator Gumgum()    // Dünya büyüyüp küçülmesi
    {
        float deger = 0.1f;
        gameManager.dunya.transform.DOScaleX(0.9143493f, deger);
        gameManager.dunya.transform.DOScaleY(1.051502f, deger);
        yield return new WaitForSeconds(deger);
        gameManager.dunya.transform.DOScaleX(0.8901376f, deger);
        gameManager.dunya.transform.DOScaleY(0.9807873f, deger);

    }
    IEnumerator SprayTime(int sure)
    {
        yield return new WaitForSeconds(sure + 2);
        audioSource.GetComponent<AudioSource>().volume = 0.4f;
        audioSource.GetComponent<AudioSource>().PlayOneShot(spraySound);
        yield return new WaitForSeconds(sure);
        sprayMusait = true;
    }
}
