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

    public Transform _exemple;
    public GameObject Papier;
    public GameObject Plastique;
    public GameObject Verre;
    public Transform[] target;
    public GameObject[] dechet;
    public GameObject[] Point;
    public Material[] resultat = new Material [2];
    public GameObject _text;


    void Start()
    {
        _cible = "papier";
        Destroy(Poubelle);
        Poubelle = Instantiate(Papier, _exemple.position, _exemple.rotation) as GameObject;
        Poubelle.layer = 9;
    }

	// Update is called once per frame
	void Update () 
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _start = true;
            _text.SetActive(false);
        }

        if(_start)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _cible = "papier";
                Destroy(Poubelle);
                Poubelle = Instantiate(Papier, _exemple.position, _exemple.rotation) as GameObject;
                Poubelle.layer = 9;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                _cible = "plastique";
                Destroy(Poubelle);
                Poubelle = Instantiate(Plastique, _exemple.position, _exemple.rotation) as GameObject;
                Poubelle.layer = 9;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _cible = "verre";
                Destroy(Poubelle);
                Poubelle = Instantiate(Verre, _exemple.position, _exemple.rotation) as GameObject;
                Poubelle.layer = 9;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                _bug = true;
                kill();
            }

            StartCoroutine("trash"); 
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
            }
            else
            {
                if (hit.collider.tag == "verre" || hit.collider.tag == "plastique" || hit.collider.tag == "papier")
                {
                    hit.collider.renderer.material = resultat[1];
                    Instantiate(Point[1], _exemple.position, _exemple.rotation);
                    _bug = false;
                }
            }

    }
}
