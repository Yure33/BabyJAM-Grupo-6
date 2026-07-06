using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D r2d;
    [SerializeField] float[] Velocidade_PadraoCorrida;
    float Velocidade;
    Vector2 direção;

    void Start()
    {
        Velocidade = Velocidade_PadraoCorrida[0];
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
}
