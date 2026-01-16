using UnityEngine;

[RequireComponent (typeof(PlayerMovement))]
[RequireComponent (typeof (Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var state = playerMovement.state;
        switch (state)
        {
            case PlayerMovement.MovementState.Default:
                anim.SetBool("Crouch", false);
                break;

            case PlayerMovement.MovementState.Crouching:
                anim.SetBool("Crouch", true);
                break;
        }
    }
}
