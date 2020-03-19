using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palet : MonoBehaviour
{
    public bool pile = true;
    public Animator anim;
    public int X = 0;
    public int y = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            var targetPos = transform.position;

            Vector3 touchposition = Camera.main.ScreenToWorldPoint(touch.position);
            touchposition.z = 0F;

            switch (touch.phase)
            {
                case TouchPhase.Began :
                        if (Vector3.Distance(transform.position, touchposition) <= 0.5f)
                        {
                            GameManager.s_Singleton.ChangePalet(X, y);
                            SoundManager.sM_Singleton.PlaySound("TouchPalet");
                        }
                    break;
            }
        }
    }

    public void PlayAnim ()
    {
        if (pile == true)
        {
            anim.SetTrigger("Jaune_To_Rouge");
            anim.ResetTrigger("Rouge_To_Jaune");
        }
        else
        {
            anim.SetTrigger("Rouge_To_Jaune");
            anim.ResetTrigger("Jaune_To_Rouge");
        }
    }
}
