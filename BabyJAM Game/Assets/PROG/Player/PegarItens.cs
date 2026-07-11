using UnityEngine;
using UnityEngine.InputSystem;

public class PegarItens : MonoBehaviour
{
    bool PossoInteragir = false;
    bool InInter = false;
    [SerializeField] PlayerMovement PlayerMov;
    [SerializeField] CampoDeVisao Camp;
    
    // Arraste o script do Inventário para cá no Inspetor
    [SerializeField] Inventario inventarioDoJogador; 

    // Variável para lembrar qual item estamos tocando agora
    ItemInteragivel itemAtual; 

    //Pegar qnt de textos, vamos fazer um contador :)
    int PassoInteraçao;

    public void OnInteract(InputAction.CallbackContext contexto)
    {
        if(contexto.performed && PossoInteragir)
        {
            if (!InInter)
            {
                Debug.Log("Comecei interação com o item ID: " + itemAtual.idItem);
                Interação(false);
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
        if(parar == false)
        {
            // AQUI ENTRARIA A SUA LÓGICA DE MOSTRAR O TEXTO NA TELA
            // Exemplo: MostrarCaixaDeTexto(itemAtual.textos);

            Debug.Log(itemAtual.textos[PassoInteraçao]);
            PassoInteraçao++;
            if(PassoInteraçao == itemAtual.textos.Length)
            {
                //Ja passei por todos os textos, posso encerrar interação :)
                InInter = true;
                PassoInteraçao = 0;
            }
        }
        if(parar == true)
        {
            //como terminou a interação, se for um item podemos pegar ele
            // Se o item for pegável, adiciona no inventário e se destrói
            if (itemAtual.pegavel)
            {
                Debug.Log("Peguei");
                inventarioDoJogador.PegarNovoItem(itemAtual.idItem);
                Destroy(itemAtual.gameObject);
            }
            InInter = false;
        }
        PlayerMov.Interagindo = Camp.Interagindo = !parar;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interagivel"))
        {
            Debug.Log("Encostei no interagivel");
            itemAtual = collision.GetComponent<ItemInteragivel>(); 
            PossoInteragir = true;
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