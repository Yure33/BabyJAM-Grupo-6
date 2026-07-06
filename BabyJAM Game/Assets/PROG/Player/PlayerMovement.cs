using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D r2d;
    [SerializeField] float[] Velocidade_PadraoCorrida;
    [SerializeField] Transform PlayerVisual;
    float Velocidade;
    Vector2 direção;
    Vector2 mousePos;

    void Start()
    {
        Velocidade = Velocidade_PadraoCorrida[0];
    }
    void Update()
    {
        Vector2 LookDirection = (mousePos - (Vector2)transform.position).normalized;
        PlayerVisual.rotation = Quaternion.Euler(0, 0, -(Mathf.Atan2(LookDirection.x, LookDirection.y)*Mathf.Rad2Deg));
    }
    void FixedUpdate()
    {
        r2d.position += Time.fixedDeltaTime * Velocidade * direção.normalized;
    }

    public void OnWalk(InputAction.CallbackContext contexto)
    {
        direção = contexto.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext contexto)
    {
        if (contexto.performed)
        {
            Velocidade = Velocidade_PadraoCorrida[1]; 
        }
        else
        {
            Velocidade = Velocidade_PadraoCorrida[0];
        }
    }

    public void OnLook(InputAction.CallbackContext contexto)
    {
        mousePos = Camera.main.ScreenToWorldPoint(contexto.ReadValue<Vector2>());
    }
}
