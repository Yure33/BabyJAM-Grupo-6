using UnityEngine;

public class AudioManager_Script : MonoBehaviour
{
    [SerializeField] AudioSource[] audios;
    
    public void ChangeAudio(int Qual, int Valor)
    {
        //0 É SFX | 1 É MUSICA
        audios[Qual].volume = Valor;
    }
}
