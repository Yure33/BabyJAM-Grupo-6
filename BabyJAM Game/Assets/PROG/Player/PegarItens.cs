using UnityEngine;
using UnityEngine.InputSystem;

public class PegarItens : MonoBehaviour
{
    bool PossoInteragir = false;
    bool InInter;
    [SerializeField] PlayerMovement PlayerMov;
    [SerializeField] CampoDeVisao Camp;

    public void OnInteract(InputAction.CallbackContext contexto)
    {
        if(contexto.performed && PossoInteragir)
        {
            if (!InInter)
            {
                Interação(false);
                Debug.Log("Começei interação");
            }
            else
            {
                Interação(true);
                Debug.Log("Terminei interação");
            }
        }
    }
    void Interação(bool parar)
    {
        PlayerMov.Interagindo = Camp.Interagindo = !parar;
        InInter = !parar;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interagivel"))
        {
            Debug.Log(collision.tag);
            PossoInteragir = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interagivel"))
        {
            PossoInteragir = false;
        }
    }
}
