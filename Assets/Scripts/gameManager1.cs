using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager1 : MonoBehaviour
{
    public GameObject descriptionMenu;
    public string currentTower = "6th Grade";
    public GameObject[] focalP;

    public Button gb;
    public Button gb1;
    public Button gb2;

    // Start is called before the first frame update
    void Start()
    {
        gb = GameObject.Find("GradeBtn-1").GetComponent<Button>();
        gb1 = GameObject.Find("GradeBtn-2").GetComponent<Button>();
        gb2 = GameObject.Find("GradeBtn-3").GetComponent<Button>();
        gb.interactable = false;
        descriptionMenu = GameObject.FindGameObjectWithTag("descriptionUI");
        descriptionMenu.transform.localScale = new Vector3(0,0,0);
        focalP = GameObject.FindGameObjectsWithTag("focalPoint");
    }

    public void TestMyStack(){
        Debug.Log("Wotking 1");
        var myBl = FindObjectsOfType<myBlock>();
        foreach(myBlock b in myBl){
            if (b.grade == currentTower){
                if (b.mastery == 0){
                    Debug.Log("Wotking 2");
                    b.BreakBlock();
                }
            }
        }
    }

    public void closeMenu(){
        descriptionMenu.transform.localScale = new Vector3(0,0,0);
    }

    public void resetGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void changeGrade(int g){
        myCamera myCam = FindObjectOfType<myCamera>();
        switch (g) {
            case 1:
                myCam.target = focalP[1];
                gb.interactable = true;
                gb1.interactable = false;
                gb2.interactable = true;
                currentTower = "7th Grade";
                break;
            case 0:
                myCam.target = focalP[2];
                gb.interactable = false;
                gb1.interactable = true;
                gb2.interactable = true;
                currentTower = "6th Grade";
                break;
            case 2:
                myCam.target = focalP[0];
                
                gb.interactable = true;
                gb1.interactable = true;
                gb2.interactable = false;
                currentTower = "8th Grade";
                break;

        }
    }
    
}
