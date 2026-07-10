using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeAudio_Script : MonoBehaviour
{
    [SerializeField] Slider[] audios;
    [SerializeField] TextMeshProUGUI[] txt;
    [SerializeField] AudioManager_Script gerenciador;
    [SerializeField] ConfigsPersist config;

    void Start()
    {
        audios[0].value = config.audioSFX*10;
        audios[1].value = config.audioMUSIC*10;
        MudeiAudio(0);
        MudeiAudio(1);
    }
    public void MudeiAudio(int SFX0_Music1)
    {
        gerenciador.ChangeAudio(SFX0_Music1, (int)audios[SFX0_Music1].value);
        txt[SFX0_Music1].text = ((int)audios[SFX0_Music1].value).ToString();
        if(SFX0_Music1 == 0)
        {
            config.audioSFX = (int)(audios[0].value/10);
        }
        else
        {
            config.audioSFX = (int)(audios[0].value/10);
        }
    }
}
