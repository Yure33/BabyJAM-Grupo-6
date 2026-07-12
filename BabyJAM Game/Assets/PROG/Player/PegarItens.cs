using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PegarItens : MonoBehaviour
{
    bool StartingScene;
    [SerializeField] string[] startingTXT;
    bool PossoInteragir = false;
    bool InInter = false;
    [SerializeField] PlayerMovement PlayerMov;
    [SerializeField] CampoDeVisao Camp;
    
    // Arraste o script do Inventário e da interação para cá no Inspetor
    [SerializeField] Inventario inventarioDoJogador; 
    [SerializeField] GameObject[] interactOBJ_TXT;

    // Variável para lembrar qual item estamos tocando agora
    ItemInteragivel itemAtual; 

    //Pegar qnt de textos, vamos fazer um contador :)
    int PassoInteraçao;

    void Start()
    {
        StartingScene = true;
        InteraçãoInduzida(false);
    }

    public void OnInteract(InputAction.CallbackContext contexto)
    {
        if(contexto.performed && PossoInteragir && !PlayerMov.NoHUD)
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
        if (contexto.performed && StartingScene)
        {
            if (!InInter)
            {
                Debug.Log("Comecei interaçao inicial");
                InteraçãoInduzida(false);
            }
            else
            {
                InteraçãoInduzida(true);
                Debug.Log("Terminei interação");
            }
        }
    }
    
    void Interação(bool parar)
    {
        if(parar == false)
        {
            //MOSTRAR CAIXA DE TEXTO
            interactOBJ_TXT[0].SetActive(true);
            interactOBJ_TXT[1].GetComponent<TextMeshProUGUI>().text = itemAtual.textos[PassoInteraçao];
            
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
            interactOBJ_TXT[0].SetActive(false);
            InInter = false;
        }
        PlayerMov.Interagindo = Camp.Interagindo = !parar;
    }

    void InteraçãoInduzida(bool parar)
    {
        if(parar == false)
        {
            //MOSTRAR CAIXA DE TEXTO
            interactOBJ_TXT[0].SetActive(true);
            interactOBJ_TXT[1].GetComponent<TextMeshProUGUI>().text = startingTXT[PassoInteraçao];
            
            // AQUI ENTRARIA A SUA LÓGICA DE MOSTRAR O TEXTO NA TELA
            // Exemplo: MostrarCaixaDeTexto(itemAtual.textos);

            Debug.Log(startingTXT[PassoInteraçao]);
            PassoInteraçao++;
            if(PassoInteraçao == startingTXT.Length)
            {
                //Ja passei por todos os textos, posso encerrar interação :)
                PassoInteraçao = 0;
                InInter = true;
            }
        }
        if(parar == true)
        {
            //terminou a interação
            StartingScene = false;
            InInter = false;
            interactOBJ_TXT[0].SetActive(false);
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