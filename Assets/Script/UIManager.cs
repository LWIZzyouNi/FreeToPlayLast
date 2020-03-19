using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject optionMenu;
    public static UIManager s_Singleton;
    public Animation animSwipeToPlay;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSoundButton()
    {
        SoundManager.sM_Singleton.Sound();
    }

    public void OnClickOptionButton()
    {
        optionMenu.SetActive(!optionMenu.activeSelf);
    }

    public void OnClickQuitButton()
    {
        GameManager.s_Singleton.QuitTheGame();
    }

    public void OnClickRelancerButton()
    {
        GameManager.s_Singleton.LoadScene();
    }

    public void DoAnimSwipeToLoad()
    {
        animSwipeToPlay.Play();
    }
}
