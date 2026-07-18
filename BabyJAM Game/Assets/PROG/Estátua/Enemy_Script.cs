using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using System.Collections;
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy_Script : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_5 = new WaitForSeconds(0.5f);
    AIPath path;
    [SerializeField] float Velocidade;
    Transform Player;
    public bool CanWalk;
    bool ReadyTurbo;
    float margemSprite;

    void Start()
    {
        path = GetComponent<AIPath>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePersonalizado());
        path.maxSpeed = Velocidade;
        margemSprite = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        //ASSOCIAR VARIÁVEL DO SCRIPT COM UMA FUNCIONALIDADE DO PATHFINDER
        path.canMove = CanWalk;
        path.maxSpeed = Velocidade;
    }

    IEnumerator UpdatePersonalizado()
    {
        //VERIFICAR SE ESTATUA ESTA NAS BORDAR DA TELA
        Vector3 posTela = Camera.main.WorldToScreenPoint(transform.position);
        if(posTela.x+margemSprite < 0 || posTela.x-margemSprite > Screen.width || posTela.y+margemSprite < 0
        || posTela.y-margemSprite > Screen.height)
        {
            //SAIU DA TELA 
            int chance = Random.Range(0, 100);
            if(!ReadyTurbo){
                if(chance <= 10){
                    Velocidade = 20;
                    ReadyTurbo = true;
                }
                else{
                    Velocidade = 1;
                }
            }   
        }
        else{
            Velocidade = 5;
            ReadyTurbo = false;
        }
        //ATUALIZA O CAMINHO QUE A IA VAI TOMAR A CADA 0.5 SEGUNDOS
        path.destination = Player.position;
        path.SearchPath();

        yield return _waitForSeconds0_5;
        CanWalk = true;
        StartCoroutine(UpdatePersonalizado());
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Te peguei jhhahahahahaha");
            SceneManager.LoadScene(0);
        }
    }
}
