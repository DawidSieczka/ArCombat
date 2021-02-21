using UnityEngine;

public class Character_Animator : MonoBehaviour
{
    private Animator newAnimator;

    public void HorizontalAnimator()
    {
        newAnimator.SetBool("anim", true);
    }

    private void Start()
    {
        newAnimator = GetComponent<Animator>();
        FindObjectOfType<TouchScreen>().InitCharacterAnimator();
    }

    private void Update()
    {
    }
}