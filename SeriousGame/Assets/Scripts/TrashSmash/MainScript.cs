using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

    private bool _start = false;
    private bool _bug;
    private string _cible;
    private GameObject Poubelle;
    private GameObject currentPoint;
    private float plus = .7F;
    private float nextTime = 0.0F;
    private bool isPaused = false;
    private bool isActive = false;
    private bool gameOver = false;
   
    public GameObject DumpsterDisplayer;
    public GameObject PauseMenu;
    public GameObject Score;
    public GameObject Resultat;
    public GameObject TestResultat;
    public GameObject limite;
    public GameObject HitTrash;
    public GameObject Papier;
    public GameObject Plastique;
    public GameObject Verre;
    public GameObject Info;
    public GameObject[] dechet;
    public Transform _exemple;
    public Transform[] target;
    public Material[] resultat = new Material [2];
    public float point = 0;

    Vector3 limitBorn = new Vector3(0.0f, 1.0f, 0.0f);

    InfoScript info;

    void Start()
    {
        _cible = "papier";
        info = Info.GetComponent<InfoScript>();
        DumpsterDisplayer.SendMessage("changeDumpsterSprite", _cible,SendMessageOptions.RequireReceiver);
        Time.timeScale = 1.0f;
    }

    
	// Update is called once per frame
	void Update () 
    {
        if (limite.transform.position.y > 0.20f)
            gameOver = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _start = true;
            isPaused = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SendMessage("PressEscape", isActive, SendMessageOptions.RequireReceiver);
            Pause();
            isActive = !isActive;
            isPaused = !isActive;
        }

        if (_start && isPaused)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _cible = "papier";
                DumpsterDisplayer.SendMessage("changeDumpsterSprite", _cible, SendMessageOptions.RequireReceiver);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                _cible = "plastique";
                DumpsterDisplayer.SendMessage("changeDumpsterSprite", _cible, SendMessageOptions.RequireReceiver);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _cible = "verre";
                DumpsterDisplayer.SendMessage("changeDumpsterSprite", _cible, SendMessageOptions.RequireReceiver);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                _bug = true;
                kill();
            }

            StartCoroutine("trash"); 
        }

        if ((int)info.timer <= 0 || gameOver)
        {
            Resultat.SetActive(true);
            Pause();
            isPaused = false;
            HitTrash.SendMessage("SetNumberOfRecycledWastes", SendMessageOptions.RequireReceiver);
            TestResultat.SendMessage("SeeScore", point, SendMessageOptions.RequireReceiver);
        }
    }

    void trash()
    {
        int i = Random.Range(0, 6);
        int y = Random.Range(0, 6);

        if (Time.time > nextTime)
        {
            nextTime = Time.time + plus;
            Instantiate(dechet[i], target[y].position, target[y].rotation);
        }

    }

    void kill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == _cible && _bug)
            {
                hit.collider.renderer.material = resultat[0];
                point += 100;
                HitGeneralEffect(hit);
                Hitchoose();
            }
            else
            {
                if (hit.collider.tag == "verre" || hit.collider.tag == "plastique" || hit.collider.tag == "papier")
                {
                    hit.collider.renderer.material = resultat[1];
                    point -= 150;
                    limite.transform.position += limitBorn;
                    HitGeneralEffect(hit);
                }
            }
        }

    }

    void Pause()
    {
        Time.timeScale = 1.0f;
        if (!isActive)
        {
            if (isPaused)
                isPaused = isActive;
           Time.timeScale = 0.0f;
        }
    }

    void HitGeneralEffect(RaycastHit hit)
    {
        hit.collider.enabled = false;
        _bug = false;
        Score.SendMessage("SeeScore", point, SendMessageOptions.RequireReceiver);
    }
    void Hitchoose()
    {
        HitTrash.SendMessage("SeeHit", _cible, SendMessageOptions.RequireReceiver);
    }
}
