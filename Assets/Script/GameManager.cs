using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lengh = 0;
    public float space;
    public GameObject palet;

    private bool gameAsStarted = false;
    public bool isWin = false;

    private int numberOfInstance;
    public List<GameObject> listOfInstance;
    private GameObject tmpsPalet;

    public static GameManager s_Singleton;

    public float timer, resetTimer, timerMin = 0f;
    private TextMeshProUGUI TimerText;
    public GameObject timerTextObject;

    public GameObject Victory;

    private int inputCountValue = 0;
    public TextMeshProUGUI InputCount;

    public UIManager UI;

    public GameObject victoryParticles;

    public bool isPause = false;


    //private bool gameIsPlaying; 

    private void Awake()
    {
        if (s_Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            s_Singleton = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfInstance = lengh * lengh;
        int ix = 1;
        int iy = 1;

        for (int i = 0; i < numberOfInstance; i++)
        {
            var newPosition = new Vector3(ix * space, iy * space * -1, 0);
            tmpsPalet = Instantiate(palet, transform);
            tmpsPalet.transform.position = newPosition;
            listOfInstance.Add(tmpsPalet);

            tmpsPalet.GetComponent<Palet>().X = ix;
            tmpsPalet.GetComponent<Palet>().y = iy;

            ix++;

            if (ix > lengh)
            {
                ix = 1;
                iy++;
            }
        }

        ResetTimer();
        TimerText = timerTextObject.GetComponent<TextMeshProUGUI>();

        InitialPart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin == false)
        {
            timer += Time.deltaTime;

            if (timer > 60)
            {
                timer = 0;
                timerMin++;
            }
            else if (timer < 60 && timerMin == 0)
            {
                int tmpsTimer = Mathf.FloorToInt(timer);
                TimerText.text = tmpsTimer.ToString("0");
            }
            else if(timerMin > 0)
            {
                int tmpsTimer = Mathf.FloorToInt(timer);
                string tmpsString = timerMin.ToString("0") + (" : ") + tmpsTimer.ToString("0");
                TimerText.text = tmpsString;
            }

            InputCount.text = inputCountValue.ToString("0");
        }
    }

    public void ChangePalet(int ix, int iy)
    {
        if(gameAsStarted)
        {
            inputCountValue++;
        }

        if (isWin == false)
        {
            for (int i = 0; i < numberOfInstance; i++)
            {
                if (iy > 1)
                {
                    if (listOfInstance[i].GetComponent<Palet>().X == ix && listOfInstance[i].GetComponent<Palet>().y == iy - 1)
                    {
                        listOfInstance[i].GetComponent<Palet>().pile = !listOfInstance[i].GetComponent<Palet>().pile;
                        listOfInstance[i].GetComponent<Palet>().PlayAnim();
                        //prend au dessus
                    }
                }

                if (iy < lengh)
                {
                    if (listOfInstance[i].GetComponent<Palet>().X == ix && listOfInstance[i].GetComponent<Palet>().y == iy + 1)
                    {
                        listOfInstance[i].GetComponent<Palet>().pile = !listOfInstance[i].GetComponent<Palet>().pile;
                        listOfInstance[i].GetComponent<Palet>().PlayAnim();
                        //prend au dessous
                    }
                }

                if (ix > 1)
                {
                    if (listOfInstance[i].GetComponent<Palet>().y == iy && listOfInstance[i].GetComponent<Palet>().X == ix - 1)
                    {
                        listOfInstance[i].GetComponent<Palet>().pile = !listOfInstance[i].GetComponent<Palet>().pile;
                        listOfInstance[i].GetComponent<Palet>().PlayAnim();
                        //prend à gauche
                    }
                }

                if (ix < lengh)
                {
                    if (listOfInstance[i].GetComponent<Palet>().y == iy && listOfInstance[i].GetComponent<Palet>().X == ix + 1)
                    {
                        listOfInstance[i].GetComponent<Palet>().pile = !listOfInstance[i].GetComponent<Palet>().pile;
                        listOfInstance[i].GetComponent<Palet>().PlayAnim();
                        //prend à droite
                    }
                }
            }

            Check();
        }
    }

    void InitialPart()
    {
        int tmpsCompt = 0;
        bool isntWin = false;

        for (int i = 0; i < 10; i++)
        {
            ChangePalet(Random.Range(1, lengh + 1), Random.Range(1, lengh + 1));
        }

        while(isntWin == false)
        {
            for (int i = 0; i < numberOfInstance; i++)
            {
                if (listOfInstance[i].GetComponent<Palet>().pile)
                {
                    tmpsCompt++;
                }
            }
            
            if (tmpsCompt >= numberOfInstance)
            {
                isntWin = true;
            }
            else
            {
                ChangePalet(Random.Range(1, lengh + 1), Random.Range(1, lengh + 1));
            }
        }

        gameAsStarted = true;
    }

    void ResetTimer()
    {
        timer = resetTimer;
        timerMin = resetTimer;
    }

    void Check ()
    {
        if (gameAsStarted == true)
        {
            int tmpsCheck = 0;

            for (int i = 0; i < numberOfInstance; i++)
            {
                if (listOfInstance[i].GetComponent<Palet>().pile)
                {
                    tmpsCheck++;
                }
            }

            if (tmpsCheck >= numberOfInstance)
            {
                Win();
            }
        }
    }

    void Win ()
    {
        isWin = true;
        Victory.SetActive(true);
        SoundManager.sM_Singleton.PlaySound("Win");
        UI.GetComponent<Animator>().SetTrigger("End");
        victoryParticles.SetActive(true);
        Debug.Log("Win");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("SwipeScene", LoadSceneMode.Single);
    }
}
