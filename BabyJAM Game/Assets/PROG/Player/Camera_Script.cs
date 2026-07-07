using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public Transform Player;
    [SerializeField] float Suavidade;
    
    void FixedUpdate()
    {
        if(Player == null)
        {
            return;
        }
        Vector2 alvo = Vector2.Lerp(transform.position, Player.position, Suavidade);
        transform.position = new Vector3(alvo.x, alvo.y, -10);
    }
}
