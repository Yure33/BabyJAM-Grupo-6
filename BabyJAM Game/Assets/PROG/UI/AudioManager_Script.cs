using UnityEngine;

public class AudioManager_Script : MonoBehaviour
{
    [SerializeField] AudioSource[] audios;
    [SerializeField] ConfigsPersist persistAudio;
    [SerializeField] bool OnMenu;

    void Start()
    {
        if(!OnMenu){
            audios[0].volume = (float)persistAudio.audioSFX/100;
            audios[1].volume = (float)persistAudio.audioMUSIC/100;
            Debug.Log(persistAudio.audioSFX.ToString() + " " + persistAudio.audioMUSIC.ToString());
        }
    }
    
    public void ChangeAudio(int Qual, int Valor)
    {
        //0 É SFX | 1 É MUSICA
        audios[Qual].volume = (float)Valor/100;
    }
}
