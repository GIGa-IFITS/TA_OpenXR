using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Android;
using System.IO;
using TMPro;

public class EventHandler : MonoBehaviour
{
    public string URL;

    [Header("Pengaturan Node")]
    // peneliti ( secara umum )
    public GameObject parentPenelitiScatter;
    public GameObject NodePeneliti;
    public float sizeCoef;
    public float nodeArea;
    GameObject[] listPeneliti;

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

    [Header("Disconnect")]
    public GameObject disconnectCanvas;

    public TextMeshProUGUI debugText;

    RequestHandler requestPeneliti = new RequestHandler();

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
    }

    public void Dashboard()
    {
        requestPeneliti.URL = URL;
        StartCoroutine(requestPeneliti.RequestData((result) =>
        {
            // mengambil jumlah jurnal, conference, books, thesis, paten dan research yang ada
            //hasilPublikasiITS(result);
            
        }, (error) => {
            if (error != "")
            {
                // retryMessage.text = error;
                // retry.SetActive(true);
                // connectionMessagePanel.SetActive(false);
            }
        }));
    }

    public void getPenelitiAbjadITS()
    {
        flushNode();

        requestPeneliti.URL = URL + "/peneliti?abjad=none";
        StartCoroutine(requestPeneliti.RequestData((result) => {
            foreach (var data in result.data[0].inisial_peneliti)
            {
                GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                NodeAbjadPeneliti.name = data.inisial;
                NodeAbjadPeneliti.tag = "ListPenelitiAbjad";
                
                int jumlah = data.total;
                float size = jumlah * sizeCoef;

                NodeVariable tambahan = NodeAbjadPeneliti.AddComponent<NodeVariable>();
                tambahan.kode_peneliti = data.inisial;
                tambahan.nama = data.inisial;
                tambahan.jumlah = jumlah;
                tambahan.ukuran = size;
                tambahan.ukuran2 = new Vector3(size, size, size);

                spawnNode(NodeAbjadPeneliti, size);
            }
            listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiAbjad");
            foreach(GameObject node in listPeneliti)
            {
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void PrintDebug(string textLog){
        debugText.text = textLog;
    }

    public void getPenelitiInisialITS(string inisial)
    {
        flushNode();
        requestPeneliti.URL = URL + "/peneliti?abjad=" + inisial;
        StartCoroutine(requestPeneliti.RequestData((result) => {
            foreach (var data in result.data[0].nama_peneliti)
            {
                GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                Debug.Log(data.nama + " " + data.kode_dosen + " " + data.jumlah);
                NodeAbjadPeneliti.name = data.nama;
                int jumlah = data.jumlah;
                float size = jumlah * sizeCoef * 2;
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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void getPenelitiFakultasITS()
    {
        flushNode();

        requestPeneliti.URL = URL + "/peneliti?fakultas=none";
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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));   
    }

    public void getPenelitiDepartemenITS(string kode_fakultas)
    {
        flushNode();
        requestPeneliti.URL = URL + "/peneliti?fakultas=" + kode_fakultas.ToString();
        StartCoroutine(requestPeneliti.RequestData((result) => {
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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
            
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void getPenelitiDepartemenDetailITS(string kode_departemen)
    {
        flushNode();

        requestPeneliti.URL = URL + "/peneliti?departemen=" + kode_departemen.ToString();
        StartCoroutine(requestPeneliti.RequestData((result) => {
            foreach (var data in result.data[0].nama_peneliti)
            {
                GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                NodeAbjadPeneliti.name = data.nama;
                NodeAbjadPeneliti.tag = "ListPenelitiDepartemenDetail";

                int jumlah = data.jumlah;
                float size = jumlah * sizeCoef * 2;

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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void getGelarPenelitiITS()
    {
        flushNode();

        requestPeneliti.URL = URL + "/gelar?kode=none";
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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void getGelarPenelitiDetail(string kode)
    {
        flushNode();

        requestPeneliti.URL = URL + "/gelar?kode="+kode;
        StartCoroutine(requestPeneliti.RequestData((result) => {
            foreach (var data in result.data[0].nama_peneliti)
            {

                GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                NodeAbjadPeneliti.name = data.nama;
                NodeAbjadPeneliti.tag = "ListGelarDetail";

                int jumlah = data.jumlah;
                float size = jumlah * sizeCoef * 2;

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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void getPublikasiFakultas()
    {
        flushNode();

        requestPeneliti.URL = URL + "/publikasi?fakultas=none";
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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void getPublikasiKataKunci(string kode)
    {
        flushNode();

        requestPeneliti.URL = URL + "/publikasi?fakultas=" + kode;
        StartCoroutine(requestPeneliti.RequestData((result) => {
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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void getKataKunciPeneliti(string fakultas, string katakunci)
    {
        flushNode();

        requestPeneliti.URL = URL + "/publikasi?fakultas=" + fakultas + "&katakunci=" + katakunci;
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
                animate = animateNode(node, node.GetComponent<NodeVariable>().ukuran2);
                StartCoroutine(animate);
            }
        }, error => {
            if (error != "")
            {
                ClientSend.SendErrorMessage(error);
            }
        }
        ));
    }

    public void spawnNode(GameObject node, float size)
    {
        node.transform.SetParent(parentPenelitiScatter.transform, false);
        node.transform.localPosition = new Vector3(Random.Range(-nodeArea, nodeArea), 0, Random.Range(-nodeArea, nodeArea));
        node.transform.localScale = new Vector3(0f, 0f, 0f);

        node.GetComponentInChildren<TextMeshProUGUI>().text = node.name;

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

    public IEnumerator animateNode(GameObject node, Vector3 nodeScale)
    {
        float timeElapsed = 0f;
        float waitTime = 2f;
        while (node.transform.localScale.x < nodeScale.x){
            node.transform.localScale = Vector3.Lerp(node.transform.localScale, nodeScale, (timeElapsed / waitTime));

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        node.transform.localScale = nodeScale;
        yield return null;
    }

    public void resizeNode(float zoom)
    {
        sizeCoef = zoom / 3000;
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

    public void GetNode(string _nodeId, string _nodeId2, string _tagName){
        if (_tagName == "ListPenelitiAbjad"){
            Manager.instance.getPenelitiInisialITS(_nodeId);
        } 
        else if(_tagName == "ListPenelitiFakultas"){
            Manager.instance.getPenelitiDepartemenITS(_nodeId);
        }
        else if(_tagName == "ListPenelitiDepartemen"){
            Manager.instance.getPenelitiDepartemenDetailITS(_nodeId);
        }    
        else if(_tagName == "ListGelar"){
            Manager.instance.getGelarPenelitiDetail(_nodeId);
        }
        else if(_tagName == "ListPublikasiFakultas"){
            Manager.instance.getPublikasiKataKunci(_nodeId);
        }
        else if (_tagName == "ListPublikasiKataKunci"){
            Manager.instance.getKataKunciPeneliti(_nodeId, _nodeId2);
        }
    }
}   