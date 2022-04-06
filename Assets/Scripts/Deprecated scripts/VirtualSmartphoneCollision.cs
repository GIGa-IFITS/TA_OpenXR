using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSmartphoneCollision : MonoBehaviour
{
    // knock smartphone to node
    void OnTriggerEnter(Collider collider){
        //Debug.Log("collide with " + collider.gameObject.name);
        NodeVariable nodeObject = collider.gameObject.GetComponent<NodeVariable>();
        if (nodeObject != null){
            if (nodeObject.CompareTag("ListPenelitiAbjad"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- NodeVariable");
                Manager.instance.getPenelitiInisialITS(id);
                
                // SEND DATA FROM VR TO SMARTPHONE
                string nama = nodeObject.nama;
                int jumlah = nodeObject.jumlah;
                string tag = "ListPenelitiAbjad";
                string filterName = "Researcher Name";
                ClientSend.SendFilterSummary(nama, jumlah, tag, id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenSummary(nama, jumlah, tag, id, filterName);
            }
            else if(nodeObject.CompareTag("ListPenelitiInisial"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- NodeVariable");

                string filterName = "Researcher Name";

                // SEND DATA FROM VR TO SMARTPHONE
                ClientSend.SendResearcherId(id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenResearcherDetail(id, filterName);
            }
            
            else if(nodeObject.CompareTag("ListPenelitiFakultas"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- fakultaspeneliti");
                Manager.instance.getPenelitiDepartemenITS(id);

                // SEND DATA FROM VR TO SMARTPHONE
                string nama = nodeObject.nama;
                int jumlah = nodeObject.jumlah;
                string tag = "ListPenelitiFakultas";
                string filterName = "Institution Unit";
                ClientSend.SendFilterSummary(nama, jumlah, tag, id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenSummary(nama, jumlah, tag, id, filterName);
            }
            else if(nodeObject.CompareTag("ListPenelitiDepartemen"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- NodeVariable");
                Manager.instance.getPenelitiDepartemenDetailITS(id);

                // SEND DATA FROM VR TO SMARTPHONE
                string nama = nodeObject.nama;
                int jumlah = nodeObject.jumlah;
                string tag = "ListPenelitiDepartemen";
                string filterName = "Institution Unit";
                ClientSend.SendFilterSummary(nama, jumlah, tag, id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenSummary(nama, jumlah, tag, id, filterName);
            }
            else if(nodeObject.CompareTag("ListPenelitiDepartemenDetail"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- NodeVariable");

                string filterName = "Institution Unit";

                // SEND DATA FROM VR TO SMARTPHONE
                ClientSend.SendResearcherId(id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenResearcherDetail(id, filterName);
            }
            
            else if(nodeObject.CompareTag("ListGelar"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- NodeVariable");
                Manager.instance.getGelarPenelitiDetail(id);

                // SEND DATA FROM VR TO SMARTPHONE
                string nama = nodeObject.nama;
                int jumlah = nodeObject.jumlah;
                string tag = "ListGelar";
                string filterName = "Academic Degree";
                ClientSend.SendFilterSummary(nama, jumlah, tag, id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenSummary(nama, jumlah, tag, id, filterName);
            }
            else if (nodeObject.CompareTag("ListGelarDetail"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- NodeVariable");

                string filterName = "Academic Degree";

                // SEND DATA FROM VR TO SMARTPHONE
                ClientSend.SendResearcherId(id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenResearcherDetail(id, filterName);
            }


            else if(nodeObject.CompareTag("ListPublikasiFakultas"))
            {
                string id = nodeObject.kode_peneliti;
                Debug.Log(id + " <- NodeVariable");
                Manager.instance.getPublikasiKataKunci(id);

                // SEND DATA FROM VR TO SMARTPHONE
                string nama = nodeObject.nama;
                int jumlah = nodeObject.jumlah;
                string tag = "ListPublikasiFakultas";
                string filterName = "Research Keyword";
                ClientSend.SendFilterSummary(nama, jumlah, tag, id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenSummary(nama, jumlah, tag, id, filterName);
            }
            else if (nodeObject.CompareTag("ListPublikasiKataKunci"))
            {
                string id = nodeObject.kode_peneliti;
                string nama = nodeObject.nama;
                Manager.instance.getKataKunciPeneliti(id, nama);

                // SEND DATA FROM VR TO SMARTPHONE
                int jumlah = nodeObject.jumlah;
                string tag = "ListPublikasiKataKunci";
                string filterName = "Research Keyword";
                ClientSend.SendFilterSummary(nama, jumlah, tag, id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenSummary(nama, jumlah, tag, id, filterName);
            }
            else if (nodeObject.CompareTag("ListKataKunciPeneliti"))
            {
                string id = nodeObject.kode_peneliti;

                string filterName = "Research Keyword";

                // SEND DATA FROM VR TO SMARTPHONE
                ClientSend.SendResearcherId(id, filterName);

                // FOR CANVAS
                VirtualSmartphoneCanvas.instance.UpdateScreenResearcherDetail(id, filterName);
            }
        }
    }
}
