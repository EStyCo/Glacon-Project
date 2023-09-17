using UnityEngine;

public class CosmoCat : MonoBehaviour
{
    enum States 
    {
        hide = 1,
        idle = 2,
        talk = 3
    }
    public static CosmoCat Instance;

    private States currentState;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        animator.Play("Hide CC");
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
        this.animator.Play("Show CC");
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
