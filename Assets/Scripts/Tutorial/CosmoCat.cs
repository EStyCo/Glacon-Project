using UnityEngine;

public class CosmoCat : MonoBehaviour
{
    enum States 
    {
        hide = 1,
        idle = 2,
        talk = 3
    }

    private States currentState;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentState == States.idle)
        {
            animator.Play("Idle CC");
        }
        else if (currentState == States.talk) 
        {
            animator.Play("Talk CC");
        }
    }

    public void HideCC()
    {
        animator.Play("Hide CC");
        currentState = States.hide;
    }

    public void ShowCC()
    {
        animator.Play("Show CC");
    }

    public void TalkCC()
    {
        currentState = States.talk;
    }

    public void IdleCC()
    {
        currentState = States.idle;
    }
}
