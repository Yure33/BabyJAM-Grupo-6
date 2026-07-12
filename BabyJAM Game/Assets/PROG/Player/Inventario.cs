using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    // Array simples: 0 = não tem, 1 = tem. O índice é o ID do item.
    public int[] itensPossuidos; 
    [SerializeField] Color[] itenVisu;
    [SerializeField] TextMeshProUGUI textoItem;
    [SerializeField] string[] textos;
    int IndexAtual = 0;

    // Arraste a tela (Canvas) do inventário para cá no Inspetor
    public GameObject telaInventarioVisual; 
    [SerializeField] Image[] itens_AntesAtuDepois;

    //TRAVAR PLAYER
    [SerializeField] PlayerMovement PlayerMov;
    [SerializeField] CampoDeVisao Camp;
    
    private bool inventarioAberto = false;
    void Start()
    {
        itens_AntesAtuDepois[0].color = itenVisu[0];
        itens_AntesAtuDepois[1].color = itenVisu[0];
        itens_AntesAtuDepois[2].color = itenVisu[0];
    }
    public void OpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed && !PlayerMov.Interagindo)
        {
            switch (inventarioAberto)
            {
                case false:
                    for(int i = 0; i < itensPossuidos.Length; i++)
                    {
                        if(itensPossuidos[i] == 1)
                        {
                            IndexAtual = i;
                            UpdateInventory(0);
                            break;
                        }
                    }
                    telaInventarioVisual.SetActive(true);
                    inventarioAberto = true;
                    break;
                case true:
                    telaInventarioVisual.SetActive(false);
                    inventarioAberto = false;
                    break;
            }
            PlayerMov.NoHUD = Camp.Interagindo = inventarioAberto;
        }
    }
    public void ControlInventory_Itens(InputAction.CallbackContext contexto)
    {
        if (inventarioAberto && contexto.performed)
        {
            Vector2 comando = contexto.ReadValue<Vector2>();
            UpdateInventory((int)comando.x);
        }
    }

    void UpdateInventory(int eixoX)
    {
        if(eixoX > 0)
        {
            //IR PARA DIREITA;
            IndexAtual++;
            if(IndexAtual >= itensPossuidos.Length)
            {
                IndexAtual = 0;
            }
        }
        if(eixoX < 0)
        {
            //IR PARA ESQUERDA
            IndexAtual--;
            if(IndexAtual < 0)
            {
                IndexAtual = itensPossuidos.Length-1;
            }
        }
        //DEFINIR VISUAL DOS ITENS A MOSTRA
        //VALORES PADRAO
        itens_AntesAtuDepois[0].color = itenVisu[0];
        itens_AntesAtuDepois[1].color = itenVisu[0];
        itens_AntesAtuDepois[2].color = itenVisu[0];
        textoItem.text = "Nenhum item destacado";

        //VALORES MODIFICADOS
        //ITEM ANTERIOR
        if(IndexAtual-1 >= 0)
        {
            if(itensPossuidos[IndexAtual-1] == 1)
            {
                itens_AntesAtuDepois[0].color = itenVisu[IndexAtual];
            }
        }
        else if(itensPossuidos[^1] == 1)
        {
            itens_AntesAtuDepois[0].color = itenVisu[^1];
        }
        //ITEM ATUAL
        if(itensPossuidos[IndexAtual] == 1)
        {
            itens_AntesAtuDepois[1].color = itenVisu[IndexAtual+1];
            textoItem.text = textos[IndexAtual];
        }

        //PROXIMO ITEM
        if(IndexAtual+1 <= itensPossuidos.Length - 1)
        {
            if(itensPossuidos[IndexAtual+1] == 1)
            {
                itens_AntesAtuDepois[2].color = itenVisu[IndexAtual+2];
            }
        }
        else
        {
        if(itensPossuidos[0] == 1)
            {
                itens_AntesAtuDepois[2].color = itenVisu[1];
            }
        }
    }

    public void PegarNovoItem(int id)
    {
        // Muda o índice do item para 1
        itensPossuidos[id] = 1; 
        Debug.Log("Item ID " + id + " foi guardado no inventário!");
    }
}