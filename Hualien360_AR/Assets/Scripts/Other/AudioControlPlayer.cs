using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class AudioControlPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool Main_Bgm_Bool;
    public AudioPlayScriptable Main_Bgm;

    public bool Lobby_Bgm_Bool;
    public AudioPlayScriptable Lobby_Bgm;
    
    public bool Boss_Bgm_Bool;
    public AudioPlayScriptable Boss_Bgm;


    void Start()
    {
        if (Main_Bgm_Bool == true)
        {
            Main_Bgm.BGM();
        }
        if (Lobby_Bgm_Bool == true)
        {
            Lobby_Bgm.BGM();
        }
        if (Boss_Bgm_Bool == true)
        {
            Boss_Bgm.BGM();
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Main_Bgm.BGM();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Lobby_Bgm.BGM();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Boss_Bgm.BGM();
        }
    }

    public void Main_BGM_Play()
    {
        Main_Bgm.BGM();
    }
    public void Lobby_BGM_Play()
    {
        Lobby_Bgm.BGM();
    }
    public void Boss_BGM_Play()
    {
        Boss_Bgm.BGM();
    }
}
