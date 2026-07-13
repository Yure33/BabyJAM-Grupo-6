using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CampoDeVisao : MonoBehaviour
{
    [SerializeField] float FOV;
    [SerializeField] float DistanciaFOV;
    [SerializeField] int QntRaios;
    [SerializeField] LayerMask LayerColisão;
    [SerializeField] LayerMask LayerEnemy;
    [SerializeField] Transform PlayerPos;
    [SerializeField] Color corDoCampo;
    public bool Interagindo;
    Vector2 lookPos;
    Vector2 walkDirection;
    bool ControllerOn = false;
    float AnguloLook;
    Mesh mesh;

    void Start()
    {
        //CRIAR UMA MESH VIA CÓDIGO
        mesh = new();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material.color = corDoCampo;
        AnguloLook = 90;
    }

    void Update()
    {
        if (Interagindo)
        {
            return;
        }
        Vector2 LookDirection;
        if (!ControllerOn)
        {
            lookPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            LookDirection = (lookPos - (Vector2)PlayerPos.position).normalized;
        }
        else
        {
            if(lookPos != null)
            {
                LookDirection = lookPos;
            }
            else
            {
                LookDirection = walkDirection;
            }
        }
        AnguloLook = -(Mathf.Atan2(LookDirection.x, LookDirection.y)*Mathf.Rad2Deg-90);
    }

    void LateUpdate()
    {
        //CRIAR UMA MESH VIA CÓDIGO
        Vector3[] vertices = new Vector3[QntRaios+2];
        Vector2[] UV = new Vector2[vertices.Length];
        int[] triangulos = new int[QntRaios*3];
        
        int verticeID = 1;
        int trianguloID = 0;
        float anguloAtual = AnguloLook+(FOV/2);
        float AumentoAngulo = FOV/QntRaios;
        
        vertices[0] = PlayerPos.position;
        for(int i = 0; i <= QntRaios; i++)
        {
            //TRNAFORMAR DIREÇÃO EM ANGULO
            Vector3 direcaoVetor = new Vector3(Mathf.Cos(anguloAtual*Mathf.Deg2Rad), Mathf.Sin(anguloAtual*Mathf.Deg2Rad))*DistanciaFOV;
            RaycastHit2D hit = Physics2D.Raycast(PlayerPos.position, direcaoVetor, DistanciaFOV, LayerColisão);
            if(hit.collider != null)
            {
                vertices[verticeID] = hit.point;
            }
            else
            {
                vertices[verticeID] = PlayerPos.position + direcaoVetor;
            }

            //DETECTAR ESTÁTUA COM OS RAYCASTS
            if(i%5 == 0)
            {
                RaycastHit2D hitEstatua;
                hitEstatua = Physics2D.Raycast(PlayerPos.position, direcaoVetor, DistanciaFOV, LayerEnemy);
                if(hitEstatua.collider != null && hitEstatua.transform.gameObject.CompareTag("Enemy"))
                {
                    hitEstatua.transform.GetComponent<Enemy_Script>().CanWalk = false;
                }
            }

            if(i > 0)
            {
                triangulos[trianguloID] = 0;
                triangulos[trianguloID+1] = verticeID-1;
                triangulos[trianguloID+2] = verticeID;

                trianguloID += 3;
            }
            verticeID++;

            anguloAtual -= AumentoAngulo;
        }

        mesh.vertices = vertices;
        mesh.uv = UV;
        mesh.triangles = triangulos;
        mesh.RecalculateBounds();
    }

    public void OnLook(InputAction.CallbackContext contexto)
    {
        if (contexto.control.device is Mouse)
        {
            ControllerOn = false;
        }
        else
        {
            ControllerOn = true;
            lookPos = contexto.ReadValue<Vector2>();
        }
    }

    public void OnWalk(InputAction.CallbackContext contexto)
    {
        if (ControllerOn)
        {
            walkDirection = contexto.ReadValue<Vector2>();
        }
    }
}
