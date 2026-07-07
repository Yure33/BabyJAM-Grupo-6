using UnityEngine;
using Pathfinding;
using System.Collections;
[RequireComponent(typeof(AIPath))]
public class Enemy_Script : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_5 = new WaitForSeconds(0.5f);
    AIPath path;
    [SerializeField] float Velocidade;
    Transform Player;
    public bool CanWalk;

    void Start()
    {
        path = GetComponent<AIPath>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePersonalizado());
    }

    // Update is called once per frame
    void Update()
    {
        path.canMove = CanWalk;
    }

    IEnumerator UpdatePersonalizado()
    {
        path.destination = Player.position;

        yield return _waitForSeconds0_5;
        CanWalk = true;
        StartCoroutine(UpdatePersonalizado());
    }
}
