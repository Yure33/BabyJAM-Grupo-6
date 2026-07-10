using UnityEngine;

public class Inventario : MonoBehaviour
{
    // Array simples: 0 = não tem, 1 = tem. O índice é o ID do item.
    // Coloquei tamanho 50, mas você pode mudar para o total de itens do jogo.
    public int[] itensPossuidos = new int[50]; 

    // Arraste a tela (Canvas) do inventário para cá no Inspetor
    public GameObject telaInventarioVisual; 
    
    private bool inventarioAberto = false;

    void Update()
    {
        // Se apertar E, liga ou desliga a tela do inventário
        if (Input.GetKeyDown(KeyCode.R))
        {
            inventarioAberto = !inventarioAberto;
            telaInventarioVisual.SetActive(inventarioAberto);
        }
    }

    public void PegarNovoItem(int id)
    {
        // Muda o índice do item para 1
        itensPossuidos[id] = 1; 
        Debug.Log("Item ID " + id + " foi guardado no inventário!");
    }
}