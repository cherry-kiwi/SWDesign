using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static int Game1 = 0;
    public static int Game2 = 0;
    public static float totalTime1 = 0;
    public static float totalTime2 = 0;

    public GameObject ScoreG;
    public GameObject PercentG;
    public GameObject TimeG;
    public GameObject LvG;
    public GameObject CountG;
    public TMP_Text ScoreT;
    public TMP_Text PercentT;
    public TMP_Text TimeT;
    public TMP_Text LvT;
    public TMP_Text CountT;

    public TMP_Text Pt1;
    public TMP_Text Pt2;
    public TMP_Text TT1;
    public TMP_Text TT2;

    public float Score = 0f;
    public int Lv = 1;
    public int count = 0;
    public float MaxPercent = 100f;
    public float percent = 0f;
    public float time = 0f;
    public float bonus = 1f;
    public float multi = 1f;

    public bool gameover = false;

    // Start is called before the first frame update
    void Start()
    {
        ScoreT = ScoreG.GetComponent<TMP_Text>();
        PercentT = PercentG.GetComponent<TMP_Text>();
        TimeT = TimeG.GetComponent<TMP_Text>();
        LvT = LvG.GetComponent<TMP_Text>();
        CountT = CountG.GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Start")
        {
            time += Time.deltaTime;
        }
        else
        {
            TT1.text = "1 Total Time\n" + totalTime1.ToString();
            TT2.text = "2 Total Time\n" + totalTime2.ToString();

            Pt1.text = "1 Play Time: " + Game1.ToString();
            Pt2.text = "2 Play Time: " + Game2.ToString();
        }

        ScoreT.text = $"Score: <color=#ffffff>{Score:#,##0}</color>";
        PercentT.text = $"{percent:##.#}%";
        TimeT.text = $"Time\n{time:#}";
        LvT.text = "Lv: " + Lv.ToString();

        if (count < 10)
        {
            CountT.text = count.ToString() + " Hit";
        }
        else
        {
            CountT.text = "<size=200>Try Upgrade NoW!!</size>";
        }

        if(gameover == true)
        {
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                totalTime1 += time;

                SceneManager.LoadScene("Start");
            }
            else if (SceneManager.GetActiveScene().name == "GameScene2")
            {
                totalTime2 += time;

                SceneManager.LoadScene("Start");
            }
        }
    }

    public void PercentChange()
    {
        if (percent <= 0.1f) 
        {
            percent = MaxPercent;
        }
        else
        {
            percent -= MaxPercent*0.1f;
            if (percent < 0.1f)
            {
                percent = MaxPercent;
            }
        }
    }

    public void SceneStart()
    {
        Game1 += 1;
        SceneManager.LoadScene("GameScene");
    }
    public void SceneStart2()
    {
        Game2 += 1;
        SceneManager.LoadScene("GameScene2");
    }
}
