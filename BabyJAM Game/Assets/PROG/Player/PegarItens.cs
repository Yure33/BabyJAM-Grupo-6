using UnityEngine;
using UnityEngine.InputSystem;

public class PegarItens : MonoBehaviour
{
    bool PossoInteragir = false;
    bool InInter;
    [SerializeField] PlayerMovement PlayerMov;
    [SerializeField] CampoDeVisao Camp;
    
    // Arraste o script do Inventário para cá no Inspetor
    [SerializeField] Inventario inventarioDoJogador; 

    // Variável para lembrar qual item estamos tocando agora
    ItemInteragivel itemAtual; 

    public void OnInteract(InputAction.CallbackContext contexto)
    {
        if(contexto.performed && PossoInteragir && itemAtual != null)
        {
            if (!InInter)
            {
                Interação(false);
                Debug.Log("Comecei interação com o item ID: " + itemAtual.idItem);
                
                // AQUI ENTRARIA A SUA LÓGICA DE MOSTRAR O TEXTO NA TELA
                // Exemplo: MostrarCaixaDeTexto(itemAtual.textos);

                // Se o item for pegável, adiciona no inventário e se destrói
                if (itemAtual.pegavel)
                {
                    inventarioDoJogador.PegarNovoItem(itemAtual.idItem);
                    Destroy(itemAtual.gameObject);
                    
                    // Como o item sumiu, já encerramos a interação
                    Interação(true); 
                    itemAtual = null;
                    PossoInteragir = false;
                }
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
            Debug.Log("Encostei no interagivel");
            itemAtual = collision.GetComponent<ItemInteragivel>(); 
            if (itemAtual != null)
            {
                PossoInteragir = true;
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interagivel"))
        {
            PossoInteragir = false;
            itemAtual = null; // Esquece o item quando o jogador se afasta
        }
    }
}