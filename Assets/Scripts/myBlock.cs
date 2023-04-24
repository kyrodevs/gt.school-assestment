using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class myBlock : MonoBehaviour
{
    public Camera mainCamera;
    public int mastery = 0;
    public string domain = "";
    public string cluster = "";
    public string description = "";
    public string grade = "";
    public Material[] newMaterial;
    public Renderer objectRenderer;
    public Material originalMaterialSelection;
    public Material selectionMaterial;
    public GameObject descriptionMenu;
    public GameObject[] menuTxt;

    
    // Start is called before the first frame update

    void Start()
    {
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>();
        menuTxt = GameObject.FindGameObjectsWithTag("txtUI");   
        descriptionMenu = GameObject.FindGameObjectWithTag("descriptionUI");
    }
    void OnMouseUp()
    {
        if (descriptionMenu != null) {
            Debug.Log("Grade level: " + mastery.ToString() + " - " + domain);
            descriptionMenu.transform.localScale = new Vector3(0.79f,0.79f,0.79f);
            menuTxt[0].GetComponent<TextMeshProUGUI>().text = mastery.ToString();
            menuTxt[1].GetComponent<TextMeshProUGUI>().text = domain;
            menuTxt[2].GetComponent<TextMeshProUGUI>().text = cluster;
            menuTxt[3].GetComponent<TextMeshProUGUI>().text = description;
        }
        

    }

    void OnMouseEnter(){
        GetComponent<MeshRenderer>().material = selectionMaterial;
        

    }


    void OnMouseExit() {
        GetComponent<MeshRenderer>().material = originalMaterialSelection;

    }

    public void BreakBlock(){
        Destroy(gameObject);
    }
    public void changeType(int mastery){
        switch (mastery)
        {
            case 0:
                objectRenderer.material = newMaterial[0];
                break;
            
            case 1:
                objectRenderer.material = newMaterial[1];
                break;
            
            case 2:
                objectRenderer.material = newMaterial[2];
                break;
            
        }

    }
}
