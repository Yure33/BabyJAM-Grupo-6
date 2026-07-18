using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D r2d;
    [SerializeField] float[] Velocidade_PadraoCorrida;
    [SerializeField] Transform PlayerVisual;
    [SerializeField] Animator playerAnime;
    bool ControllerOn = false;
    public bool Interagindo;
    public bool NoHUD;
    float Velocidade;
    Vector2 direção;
    Vector2 lookPos;

    void Start()
    {
        Velocidade = Velocidade_PadraoCorrida[0];
    }
    void Update()
    {
        if (Interagindo || NoHUD)
        {
            return;
        }
        Vector2 LookDirection;

        if (!ControllerOn)
        {
            lookPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            LookDirection = (lookPos - (Vector2)transform.position).normalized;
        }
        else
        {
            if(lookPos != Vector2.zero)
            {
                LookDirection = lookPos.normalized;
            }
            else
            {
                LookDirection = direção.normalized;
            }
        }
        PlayerVisual.rotation = Quaternion.Euler(0, 0, -(Mathf.Atan2(LookDirection.x, LookDirection.y)*Mathf.Rad2Deg));
    }
    void FixedUpdate()
    {
        if (Interagindo || NoHUD)
        {
            return;
        }
        r2d.position += Time.fixedDeltaTime * Velocidade * direção.normalized;
    }

    public void OnWalk(InputAction.CallbackContext contexto)
    {
        direção = contexto.ReadValue<Vector2>();
        if(direção == Vector2.zero && !Interagindo && !NoHUD)
        {
            playerAnime.SetBool("Walking", false);
        }
        else if(!Interagindo && !NoHUD)
        {
            playerAnime.SetBool("Walking", true);
        }
    }

    public void OnRun(InputAction.CallbackContext contexto)
    {
        Debug.Log("Nao Corre");
    }

    public void OnLook(InputAction.CallbackContext contexto)
    {
        if (contexto.control.device is Mouse)
        {
            ControllerOn = false;
        }
        else
        {
            ControllerOn = true;
            lookPos = contexto.ReadValue<Vector2>();
        }
    }
}
