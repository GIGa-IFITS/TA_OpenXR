using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Android;
using System.IO;
//using VRTK;
using TMPro;


public class EventHandler : MonoBehaviour
{
    [Header("Koneksi dari unity ke database - Event Handler")]
    // koneksi dari unity ke database
    public GameObject configPanel;
    public GameObject retry;
    public Text retryMessage;
    public GameObject connectionMessagePanel;
    public string URL;

    [Header("Dashboard")]
    // total publikasi - dashboard
    public GameObject DashboardBar;
    public GameObject DashboardBar2;
    public GameObject DashboardBar3;
    public GameObject DashboardBar4;
    public List<GameObject> listDashboard2;

    public Text publikasiJurnal;
    public Text publikasiKonferensi;
    public Text publikasiBuku;
    public Text publikasiTesis;
    public Text publikasiPaten;
    public Text publikasiPenelitian;
    public bool dashboardStatus = false;
    bool dashboardRefreshed = false;



    [Header("Pengaturan Node")]
    // peneliti ( secara umum )
    public GameObject parentPenelitiScatter;
    public GameObject NodePeneliti;
    public GameObject peekPeneliti;
    public float sizeCoef = 0.005f;
    GameObject[] listPeneliti;
    int transparency = 1;

    [Header("bool data")]
    bool penelitiAbjadRefreshed = false;
    bool penelitiInisialRefreshed = false;
    bool penelitiFakultasRefreshed = false;
    bool penelitiDepartemenRefreshed = false;
    bool penelitiGelarFakultasRefreshed = false;
    bool penelitiGelarDepartemenRefreshed = false;
    bool penelitiLabFakultasRefreshed = false;
    bool penelitiLabDepartemenRefreshed = false;

    [Header("Material")]
    public Material AbjadMaterial;
    public Material InisialMaterial;
    public Material FakultasMaterial;
    public Material DepartemenMaterial;
    public Material F_SCIENTICS;
    public Material F_INDSYS;
    public Material F_CIVPLAN;
    public Material F_MARTECH;
    public Material F_ELECTICS;
    public Material F_CREABIZ;
    public Material F_VOCATIONS;
    public Material lessTransparentMaterial;
    public Material normalTransparentMaterial;
    public Material moreTransparentMaterial;

    [Header("Animation")]
    public IEnumerator animate;
    public Vector3 initialScale;
    public Quaternion InitialRotation;

    public Vector3 endMarker = new Vector3(5.47f, -0.57f, -11);
    public Vector3 endMarkerLegend = new Vector3(-6.8256f, -0.09f, -10.991f);
    public Vector3 endScaleChart = new Vector3(13.08183f, 7.320017f, 1);
    public Vector3 endScaleLegend = new Vector3(9.951063f, 6.884685f, 0.4520478f);
    bool destroyedStatus = false;

    [Header("Detail")]
    // detail peneliti
    
    public GameObject DetailPenelitiBar;
    public Text namaPeneliti;
    public Text tanggalPeneliti;
    public Text fakultasPeneliti;
    public Text departemenPeneliti;
    public Text jurnalPeneliti;
    public Text konferensiPeneliti;
    public Text bukuPeneliti;
    public Text tesisPeneliti;
    public Text patenPeneliti;
    public Text penelitianPeneliti;
    public bool detPenelitiStatus = false;

    [Header("Tombol Navigasi")]
    // public VRTK_InteractableObject tombolDashboard;
    // public VRTK_InteractObjectHighlighter highlightOption;
    // public GameObject TableButton;

    [Header("Pengaturan")]

    public GameObject OptionBar;
    public bool detOptionStatus = false;

    [Header("Navigator")]

    public TMP_Text NavigatorText;

    RequestHandler requestPeneliti = new RequestHandler();
    
    void Start()
    {
        //TableButton = GameObject.Find("NavigationButton");
    }

    public void ApplyURL(Config config)
    {
        if (config.GetPort() == "")
        {
            URL = config.GetUrl();
        }
        else
        {
            URL = config.GetWebAPI();
        }
        getDetailPenelitiITS(4987.ToString());
    }

    public void Dashboard()
    {
        if(dashboardRefreshed == false)
        {
            requestPeneliti.URL = URL;
            //NavigatorText.text = "Dashboard";
            StartCoroutine(requestPeneliti.RequestData((result) =>
            {
                // mengambil jumlah jurnal, conference, books, thesis, paten dan research yang ada
                hasilPublikasiITS(result);
               
            }, (error) => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }));
        }
    }

    // hasilPublikasiITS adalah data pertama yang ditampilkan di dashboard
    void hasilPublikasiITS(RawData rawdata)
    {
        listDashboard2.Add(DashboardBar);
        listDashboard2.Add(DashboardBar2);
        listDashboard2.Add(DashboardBar3);
        listDashboard2.Add(DashboardBar4);


        foreach (GameObject dashboardPlane in listDashboard2)
        {
            Text publikasiJurnalTest = dashboardPlane.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>();
            Text publikasiKonferensiTest = dashboardPlane.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>();
            Text publikasiBukuTest = dashboardPlane.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>();
            Text publikasiTesisTest = dashboardPlane.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>();
            Text publikasiPatenTest = dashboardPlane.transform.GetChild(0).GetChild(6).GetChild(1).GetComponent<Text>();
            Text publikasiPenelitianTest = dashboardPlane.transform.GetChild(0).GetChild(7).GetChild(1).GetComponent<Text>();

            publikasiJurnalTest.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[0].journals.ToString();
            publikasiKonferensiTest.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[1].conferences.ToString();
            publikasiBukuTest.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[2].books.ToString();
            publikasiTesisTest.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[3].thesis.ToString();
            publikasiPatenTest.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[4].paten.ToString();
            publikasiPenelitianTest.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[5].research.ToString();
        }
    }

    public void getPenelitiAbjadITS()
    {
        if(penelitiAbjadRefreshed == false)
        {
            flushNode();

            requestPeneliti.URL = URL + "/peneliti?abjad=none";
            NavigatorText.text = "Initials";
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].inisial_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    //NodeAbjadPeneliti.GetComponent<VRTK_PressedObject>().Start();
                    NodeAbjadPeneliti.name = data.inisial;
                    NodeAbjadPeneliti.tag = "ListPenelitiAbjad";
                    int jumlah = data.total;

                    float size = jumlah * sizeCoef;

                    int test = Random.Range(0, 2);
                    if (test == 0) { NodeAbjadPeneliti.GetComponent<FloatingSphere>().orientation = -1; }

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_peneliti = data.inisial;
                    tambahan.nama = data.inisial;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran2 = new Vector3(size, size, size);

                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiAbjad");
                foreach(GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0) ;
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
        else
        {

        }

    }

    public void peekNodePeneliti(GameObject NodePeneliti, string jenis="researchers")
    {
        GameObject peekNodePeneliti = peekPeneliti;
        peekNodePeneliti.SetActive(true);
        var peekNode = peekNodePeneliti.transform;

        var peek = peekNode.GetChild(0);

        var peekNodeNama = peek.GetChild(1).GetComponent<TMP_Text>();
        var PeekNodeTitle = peek.GetChild(2).GetComponent<TMP_Text>();
        var peekNodeJumlah = peek.GetChild(3).GetComponent<TMP_Text>();
        var NodeVariable = NodePeneliti.GetComponent<NodeVariable>().nama;
        var jumlahPublikasi = NodePeneliti.GetComponent<NodeVariable>().jumlah;

        if (jenis == "researchers")
        {
            PeekNodeTitle.text = "Total Researchers";
        }
        else
        {
            PeekNodeTitle.text = "Total Publications";
        }

        peekNodeNama.text = NodeVariable;
        peekNodeJumlah.text = jumlahPublikasi.ToString();
    }

    public void getPenelitiInisialITS(string inisial)
    {
        if(penelitiInisialRefreshed == false)
        {
            flushNode();
            NavigatorText.text = "Initial :" + inisial;
            requestPeneliti.URL = URL + "/peneliti?abjad=" + inisial;
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].nama_peneliti)
                {
                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    Debug.Log(data.nama + " " + data.kode_dosen + " " + data.jumlah);
                    NodeAbjadPeneliti.name = data.kode_dosen;
                    int jumlah = data.jumlah;
                    float size = jumlah * sizeCoef;
                    NodeAbjadPeneliti.tag = "ListPenelitiInisial";

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_peneliti = data.kode_dosen;
                    tambahan.nama = data.nama;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);

                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiInisial");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
    }

    public void getPenelitiFakultasITS()
    {
        if(penelitiFakultasRefreshed == false)
        {

            flushNode();

            requestPeneliti.URL = URL + "/peneliti?fakultas=none";
            NavigatorText.text = "Faculties";
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].fakultas_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama_fakultas;
                    NodeAbjadPeneliti.tag = "ListPenelitiFakultas";

                    int jumlah = data.jumlah;
                    float size = jumlah * sizeCoef;

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_alternate = data.kode_fakultas.ToString();
                    tambahan.kode_peneliti = data.kode_fakultas.ToString();
                    tambahan.nama = data.nama_fakultas;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);


                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiFakultas");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
        
    }

    public void getPenelitiDepartemenITS(string kode_fakultas)
    {
        if(penelitiDepartemenRefreshed == false)
        {

            flushNode();
            requestPeneliti.URL = URL + "/peneliti?fakultas=" + kode_fakultas.ToString();
            StartCoroutine(requestPeneliti.RequestData((result) => {
                NavigatorText.text = result.data[0].departemen_peneliti[0].nama_fakultas;
                foreach (var data in result.data[0].departemen_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama_departemen;
                    NodeAbjadPeneliti.tag = "ListPenelitiDepartemen";

                    int jumlah = data.jumlah;
                    float size = jumlah * sizeCoef;


                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_alternate = data.kode_fakultas.ToString();
                    tambahan.kode_peneliti = data.kode_departemen.ToString();
                    tambahan.nama = data.nama_departemen;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);

                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiDepartemen");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
                
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
        
    }

    public void getPenelitiDepartemenDetailITS(string kode_departemen)
    {
        if (penelitiDepartemenRefreshed == false)
        {
            flushNode();

            requestPeneliti.URL = URL + "/peneliti?departemen=" + kode_departemen.ToString();
            StartCoroutine(requestPeneliti.RequestData((result) => {
                NavigatorText.text = result.data[0].nama_peneliti[0].nama_departemen;
                foreach (var data in result.data[0].nama_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama;
                    NodeAbjadPeneliti.tag = "ListPenelitiDepartemenDetail";

                    int jumlah = data.jumlah;
                    //float sizeCoef = 0.005f;
                    float size = jumlah * sizeCoef;


                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_peneliti = data.kode_dosen.ToString();
                    tambahan.nama = NodeAbjadPeneliti.name;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);

                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiDepartemenDetail");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }

    }

    public void getGelarPenelitiITS()
    {
        if (penelitiGelarFakultasRefreshed == false)
        {
            flushNode();

            requestPeneliti.URL = URL + "/gelar?kode=none";
            NavigatorText.text = "Academic Degree";
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].gelar_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.gelar;
                    NodeAbjadPeneliti.tag = "ListGelar";

                    int jumlah = data.jumlah;
                    float size = jumlah * sizeCoef;

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_peneliti = data.gelar.ToString();
                    tambahan.nama = tambahan.kode_peneliti;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);


                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListGelar");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
    }

    public void getGelarPenelitiDetail(string kode)
    {
        if (penelitiGelarFakultasRefreshed == false)
        {
            flushNode();

            requestPeneliti.URL = URL + "/gelar?kode="+kode;
            NavigatorText.text = "Degree : " + kode;
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].nama_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama;
                    NodeAbjadPeneliti.tag = "ListGelarDetail";

                    int jumlah = data.jumlah;
                    float size = jumlah * sizeCoef;

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_peneliti = data.kode_dosen.ToString();
                    tambahan.nama = NodeAbjadPeneliti.name;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);


                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListGelarDetail");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
    }

    public void getPublikasiFakultas()
    {
        if (penelitiLabFakultasRefreshed == false)
        {
            flushNode();

            requestPeneliti.URL = URL + "/publikasi?fakultas=none";
            NavigatorText.text = "Publications";
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].fakultas_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama_fakultas;
                    NodeAbjadPeneliti.tag = "ListPublikasiFakultas";

                    //jumlah disini adalah jumlah publikasi, bukan jumlah peneliti
                    int jumlah = data.jumlah;
                    float size = jumlah * sizeCoef * 0.1f;

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_alternate = data.kode_fakultas.ToString();
                    tambahan.kode_peneliti = data.kode_fakultas.ToString();
                    tambahan.nama = NodeAbjadPeneliti.name;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);


                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPublikasiFakultas");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
    }

    public void getPublikasiKataKunci(string kode)
    {
        if(penelitiLabDepartemenRefreshed == false)
        {
            flushNode();

            requestPeneliti.URL = URL + "/publikasi?fakultas=" + kode;
            StartCoroutine(requestPeneliti.RequestData((result) => {
                NavigatorText.text = result.data[0].fakultas_publikasi[0].nama_fakultas;
                foreach (var data in result.data[0].fakultas_publikasi)
                {
                    

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.kata_kunci;
                    NodeAbjadPeneliti.tag = "ListPublikasiKataKunci";

                    int jumlah = int.Parse(data.df);
                    float size = jumlah * sizeCoef;

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_alternate = data.kode_fakultas.ToString();
                    tambahan.kode_peneliti = data.kode_fakultas.ToString();
                    tambahan.nama = NodeAbjadPeneliti.name;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = float.Parse(data.idf);
                    tambahan.ukuran2 = new Vector3(size, size, size);


                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPublikasiKataKunci");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
    }

    public void getKataKunciPeneliti(string fakultas, string katakunci)
    {
        if (penelitiLabDepartemenRefreshed == false)
        {
            flushNode();

            requestPeneliti.URL = URL + "/publikasi?fakultas=" + fakultas + "&katakunci=" + katakunci;
            NavigatorText.text = "Keyword : " + katakunci;
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].nama_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama;
                    NodeAbjadPeneliti.tag = "ListKataKunciPeneliti";

                    int jumlah = data.jumlah;
                    float size = jumlah * sizeCoef * 10;

                    NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                    tambahan.kode_peneliti = data.kode_dosen.ToString();
                    tambahan.nama = NodeAbjadPeneliti.name;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                    tambahan.ukuran2 = new Vector3(size, size, size);


                    spawnNode(NodeAbjadPeneliti, size);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListKataKunciPeneliti");
                foreach (GameObject node in listPeneliti)
                {
                    animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2, endMarker, InitialRotation, 0);
                    StartCoroutine(animate);
                }
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
    }

    public void spawnNode(GameObject node, float size)
    {
        int test = Random.Range(0, 2);
        if (test == 0) { node.GetComponent<FloatingSphere>().orientation = -1; }

        node.transform.SetParent(parentPenelitiScatter.transform, false);
        node.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
        node.transform.localScale = new Vector3(0f, 0f, 0f);

        if (node.CompareTag("ListPenelitiAbjad"))
        {
            node.GetComponent<Renderer>().material = AbjadMaterial;
        }
        else if (node.CompareTag("ListPenelitiInisial"))
        {
            node.GetComponent<Renderer>().material = InisialMaterial;
        }
        else if (node.CompareTag("ListPenelitiFakultas") || node.CompareTag("ListPenelitiDepartemen") || node.CompareTag("ListPublikasiFakultas") || node.CompareTag("ListPublikasiKataKunci") || node.CompareTag("ListPublikasiKataKunci"))
        {
            switch (int.Parse(node.GetComponent<NodeVariable>().kode_alternate))
            {
                case 1:
                    
                    node.GetComponent<Renderer>().material = F_SCIENTICS;
                    break;
                case 2:
                    node.GetComponent<Renderer>().material = F_INDSYS;
                    break;
                case 3:
                    node.GetComponent<Renderer>().material = F_CIVPLAN;
                    break;
                case 4:
                    node.GetComponent<Renderer>().material = F_MARTECH;
                    break;
                case 5:
                    node.GetComponent<Renderer>().material = F_ELECTICS;
                    break;
                case 6:
                    node.GetComponent<Renderer>().material = F_CREABIZ;
                    break;
                case 7:
                    node.GetComponent<Renderer>().material = F_VOCATIONS;
                    break;
                default:
                    node.GetComponent<Renderer>().material = AbjadMaterial;
                    break;

            }
        }
        else 
        {
            Debug.Log("no material added");
        }
    }

    public IEnumerator animateNode(GameObject node, Vector3 nodeScale, Vector3 nodeLocation , Quaternion nodeRotation ,int mode = 0)
    {
        float timeElapsed = 0f;
        float waitTime = 2f;
        if (mode == 0) // dari kecil ke membesar
        {
            while (node.transform.localScale.x < nodeScale.x)
            {
                node.transform.localScale = Vector3.Lerp(node.transform.localScale, nodeScale, (timeElapsed / waitTime));

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            node.transform.localScale = nodeScale;

            yield return null;
        }
        else // dari besar ke mengecil
        {
            while (node.transform.localScale.x > nodeScale.x)
            {
                node.transform.localScale = Vector3.Lerp(node.transform.localScale, nodeScale, (timeElapsed / waitTime));

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            node.transform.localScale = nodeScale;
            destroyedStatus = true;
            yield return null;
        }
    }

    public void resizeNode(float zoom)
    {
        if(listPeneliti != null)
        {
            foreach (GameObject node in listPeneliti)
            {
                Debug.Log("resized");
                node.transform.localScale = node.transform.localScale + new Vector3(zoom, zoom, zoom);
            }
        }
    }    

    public void changeSpeedNode(float zoom)
    {
        if(listPeneliti != null)
        {
            foreach (GameObject node in listPeneliti)
            {
                node.GetComponent<FloatingSphere>().frequency += zoom;
            }
        }
    }

    public void movePositionNode(string axis, float amount)
    {
        var location = parentPenelitiScatter.transform.localPosition;
        if (axis == "x")
        {
            parentPenelitiScatter.transform.localPosition = parentPenelitiScatter.transform.localPosition + new Vector3(amount, 0, 0);
        }
        else if (axis == "y")
        {
            parentPenelitiScatter.transform.localPosition = parentPenelitiScatter.transform.localPosition + new Vector3(0, amount, 0);
        }
        else if (axis == "z")
        {
            parentPenelitiScatter.transform.localPosition = parentPenelitiScatter.transform.localPosition + new Vector3(0, 0, amount);
        }
        else
        {
            Debug.Log("error, only supporting 3 axis (x,y,z)");
        }
    }

    public void transparentNode(int transparentType)
    {
        if (listPeneliti != null)
        {
            foreach (GameObject node in listPeneliti)
            {
                Debug.Log("tes");

                if (transparentType == 0)
                {
                    node.GetComponent<Renderer>().material = lessTransparentMaterial;
                }
                else if(transparentType == 1)
                {
                    node.GetComponent<Renderer>().material = normalTransparentMaterial;
                }
                else
                {
                    node.GetComponent<Renderer>().material = moreTransparentMaterial;
                }
            }
        }
    }

    public void flushNode()
    {
        if(listPeneliti != null)
        {
            foreach (GameObject node in listPeneliti)
            {
                Destroy(node);
            }
        }
        
    }

    // detailPenelitiITS adalah data yang ditampilkan ketika melihat salah satu peneliti ITS
    public void getDetailPenelitiITS(string id_peneliti)
    {
        requestPeneliti.URL = URL + "/detailpeneliti?id_peneliti=" + id_peneliti;
        StartCoroutine(requestPeneliti.RequestData((result) =>
        {
            // mengambil jumlah jurnal, conference, books, thesis, paten dan research yang ada
            detailPenelitiITS(result);
        }, (error) => {
            if (error != "")
            {
                retryMessage.text = error;
                retry.SetActive(true);
                connectionMessagePanel.SetActive(false);
            }
        }));
    }

    void detailPenelitiITS(RawData rawdata)
    {
        // namaPeneliti.text = rawdata.data[0].detail_peneliti[0].nama;
        // tanggalPeneliti.text = rawdata.data[0].detail_peneliti[0].tanggal_lahir;
        // fakultasPeneliti.text = rawdata.data[0].detail_peneliti[0].fakultas;
        // departemenPeneliti.text = rawdata.data[0].detail_peneliti[0].departemen;

        // jurnalPeneliti.text = rawdata.data[0].detail_peneliti[0].jurnal.ToString();
        // konferensiPeneliti.text = rawdata.data[0].detail_peneliti[0].konferensi.ToString();
        // bukuPeneliti.text = rawdata.data[0].detail_peneliti[0].buku.ToString();
        // tesisPeneliti.text = rawdata.data[0].detail_peneliti[0].tesis.ToString();
        // patenPeneliti.text = rawdata.data[0].detail_peneliti[0].paten.ToString();
        // penelitianPeneliti.text = rawdata.data[0].detail_peneliti[0].penelitian.ToString();
    }

    public void buttonPressed(string identifier, string name = null)
    {
        Debug.Log("button pressed <- eventHandler");
        if(identifier == "ListPenelitiAbjad")
        {
            Debug.Log(identifier + "<- eventhandler");
            getPenelitiInisialITS(name);
        }
        else if(identifier == "ListInisialPeneliti")
        {
            Debug.Log(identifier + "<- eventhandler");
            getDetailPenelitiITS(name);
        }
        else if(identifier == "DashboardButton")
        {
            dashboardStatus = !dashboardStatus;
            Debug.Log("dashboard button pressed <- eventHandler");
        }
        else if(identifier == "PenelitiButton")
        {
            detPenelitiStatus = !detPenelitiStatus;
            DetailPenelitiBar.SetActive(detPenelitiStatus);

            Debug.Log("peneliti button pressed <- eventHandler");
        }

    }


}   