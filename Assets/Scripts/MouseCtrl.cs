using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MouseCtrl : MonoBehaviour
{
    private Camera cam;

    bool Hammering = false;

    public GameObject Fire;
    public GameObject coin;
    public ParticleSystem upgrade;
    public ScoreManager scoreManager;

    float rand;

    void Start()
    {
        rand = Random.Range(0.0f, 100.0f);
        scoreManager.percent = 100f;

        upgrade = GameObject.Find("PlasmaExplosionEffect").GetComponent<ParticleSystem>();
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();

        Instantiate(Fire, new Vector3(Random.Range(-9, 9), Random.Range(11, 15), Random.Range(-4, -0.5f)), Quaternion.identity);
    }
    void Update()
    {
        Vector3 pos = cam.WorldToViewportPoint(transform.position);
        Vector3 posMouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        transform.position = posMouse;

        //확률
        per();

        //레벨 비례 최대확률 감소
        if (SceneManager.GetActiveScene().name == "GameScene2")
        {
            LvPer();
        }

        //마우스 포인터 숨기기
        if (pos.x < 0f) Cursor.visible = true;
        else if (pos.x > 1f) Cursor.visible = true;
        else if (pos.y < 0f) Cursor.visible = true;
        else if (pos.y > 1f) Cursor.visible = true;
        else Cursor.visible = false;


        //마우스 좌우클릭
        if (Input.GetMouseButtonDown(0) && !Hammering && scoreManager.count < 10)
        {
            transform.rotation = Quaternion.Euler(9, 90, 90);
            Hammering = true;
        }
        else if (Input.GetMouseButtonDown(1) && !Hammering && scoreManager.count >= 10) 
        {
            transform.rotation = Quaternion.Euler(9, 90, 90);
            upgrade.Play();
            Hammering = true;

            rand = Random.Range(0, 100);

            //강화 성공 시
            if (rand <= scoreManager.percent)
            {
                scoreManager.bonus = scoreManager.bonus * scoreManager.multi;
                scoreManager.Lv += 1;
                scoreManager.count -= 10;
                scoreManager.percent = scoreManager.MaxPercent;
            }
            else
            {
                Cursor.visible = true;

                scoreManager.gameover = true;
            }
        }


        //물체 회전
        if(Hammering == true && transform.rotation != Quaternion.Euler(87.5f, 0, 0))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(87.5f, 0, 0), Time.deltaTime * 150f);
        }
        else
        {
            Hammering = false;
            upgrade.Stop();
        }
    }

    void per()
    {
        if (scoreManager.percent <= scoreManager.MaxPercent)
        {
            scoreManager.multi = 1.2f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.9f)
        {
            scoreManager.multi = 1.3f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.8f)
        {
            scoreManager.multi = 1.4f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.7f)
        {
            scoreManager.multi = 1.5f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.6f)
        {
            scoreManager.multi = 1.6f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.5f)
        {
            scoreManager.multi = 1.7f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.4f)
        {
            scoreManager.multi = 1.8f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.3f)
        {
            scoreManager.multi = 1.9f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.2f)
        {
            scoreManager.multi = 2f;
        }
        else if (scoreManager.percent <= scoreManager.MaxPercent * 0.1f)
        {
            scoreManager.multi = 3f;
        }
    }

    void LvPer()
    {
        if(scoreManager.Lv == 1)
        {
            scoreManager.MaxPercent = 100f;
        }
        else if (scoreManager.Lv == 2)
        {
            scoreManager.MaxPercent = 81f;
        }
        else if (scoreManager.Lv == 3)
        {
            scoreManager.MaxPercent = 64f;
        }
        else if (scoreManager.Lv == 4)
        {
            scoreManager.MaxPercent = 50f;
        }
        else if (scoreManager.Lv == 5)
        {
            scoreManager.MaxPercent = 26f;
        }
        else if (scoreManager.Lv == 6)
        {
            scoreManager.MaxPercent = 15f;
        }
        else if (scoreManager.Lv == 7)
        {
            scoreManager.MaxPercent = 7f;
        }
        else if (scoreManager.Lv == 8)
        {
            scoreManager.MaxPercent = 4f;
        }
        else if (scoreManager.Lv == 9)
        {
            scoreManager.MaxPercent = 2f;
        }
        else if (scoreManager.Lv == 10)
        {
            scoreManager.MaxPercent = 1f;
        }

        if(scoreManager.percent > scoreManager.MaxPercent)
        {
            scoreManager.percent = scoreManager.MaxPercent;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "click")
        {
            if (Input.GetMouseButtonDown(0) && !Hammering && scoreManager.count < 10)
            {
                Destroy(other.gameObject);
                scoreManager.count += 1;
                scoreManager.Score = scoreManager.Score + (scoreManager.Lv * scoreManager.bonus);
                Instantiate(coin, transform.position + new Vector3(-3f,0,0), Quaternion.identity);
                Instantiate(Fire, new Vector3(Random.Range(-9, 9), Random.Range(11, 15), Random.Range(-4, -0.5f)), Quaternion.identity);
            }
        }
    }
}
