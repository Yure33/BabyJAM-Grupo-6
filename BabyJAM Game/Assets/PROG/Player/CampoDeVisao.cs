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
    [SerializeField] Transform PlayerPos;
    [SerializeField] Color corDoCampo;
    Vector2 mousePos;
    float AnguloLook;
    Mesh mesh;

    void Start()
    {
        //CRIAR UMA MESH VIA CÓDIGO
        mesh = new();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material.color = corDoCampo;
    }

    void Update()
    {
        Vector2 LookDirection = (mousePos - (Vector2)PlayerPos.position).normalized;
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
    }

    public void OnLook(InputAction.CallbackContext contexto)
    {
        mousePos = Camera.main.ScreenToWorldPoint(contexto.ReadValue<Vector2>());
    }
}
