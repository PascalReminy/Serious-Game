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
   
    public GameObject DumpsterDisplayer;
    public Transform _exemple;
    public GameObject Papier;
    public GameObject Plastique;
    public GameObject Verre;
    public GameObject Info;
    public Transform[] target;
    public GameObject[] dechet;
    public GameObject[] Point;
    public Material[] resultat = new Material [2];
    public int score = 0;

    InfoScript info;

    void Start()
    {
        _cible = "papier";
        info = Info.GetComponent<InfoScript>();
        DumpsterDisplayer.SendMessage("changeDumpsterSprite", _cible,SendMessageOptions.RequireReceiver);
    }

    
	// Update is called once per frame
	void Update () 
    {

        if (Input.GetKeyDown(KeyCode.Space))
            _start = true;

        if(_start)
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

        if ((int)info.timer <= 0)
        {
            Pause();
            _start = false;
            Debug.Log("fin");
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
            if (hit.collider.tag == _cible && _bug)
            {
                hit.collider.renderer.material = resultat[0];
                Instantiate(Point[0], _exemple.position, _exemple.rotation);
                _bug = false;
                score += 5;
            }
            else
            {
                if (hit.collider.tag == "verre" || hit.collider.tag == "plastique" || hit.collider.tag == "papier")
                {
                    hit.collider.renderer.material = resultat[1];
                    Instantiate(Point[1], _exemple.position, _exemple.rotation);
                    _bug = false;
                    score -= 6;
                }
            }

    }

    void Pause()
    {
        Time.timeScale = 0.0f; 
    }
}
