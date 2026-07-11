using UnityEngine;
using UnityEngine.InputSystem;

public class Inventario : MonoBehaviour
{
    // Array simples: 0 = não tem, 1 = tem. O índice é o ID do item.
    // Coloquei tamanho 50, mas você pode mudar para o total de itens do jogo.
    public int[] itensPossuidos = new int[10]; 

    // Arraste a tela (Canvas) do inventário para cá no Inspetor
    public GameObject telaInventarioVisual; 
    
    private bool inventarioAberto = false;

    public void OpenInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            switch (inventarioAberto)
            {
                case false:
                    telaInventarioVisual.SetActive(true);
                    inventarioAberto = true;
                    break;
                case true:
                    telaInventarioVisual.SetActive(false);
                    inventarioAberto = false;
                    break;
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