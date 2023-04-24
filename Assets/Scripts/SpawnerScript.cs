using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using System;

public class SpawnerScript : MonoBehaviour
{
    private string URL = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
    public Text Type;
    public string grade;
    public int towerPosition = 20;

    public GameObject blockPrefab;
    public int numberOfDomains = 0;
    public int spawned = 0;

    public List<Stack> blockS = new List<Stack>();
    public float spawnZ = 0f;
    private float blockHgt = 1.5f;
    private float rot = 0f;



    // Start is called before the first frame update
    void Start()
    {
        //DescriptionBox.SetActive(false);
        StartCoroutine(GetDatas());
        
    }
    
    private void SpawnBlocks(int dt, int v1, int v2, int v3){
        GameObject bl = Instantiate(blockPrefab) as GameObject;
        bl.transform.SetParent(this.transform);
        bl.transform.position =  new Vector3(towerPosition, 1 *spawnZ,  0);
        bl.transform.Rotate(new Vector3(0f,rot,0f));
        spawnZ += blockHgt;
        if (rot == 0){
            rot += 91;
        } else {
            rot = 0;
        }
        Transform parentTransform = bl.GetComponent<Transform>();
        myBlock child = parentTransform.Find("Block1").gameObject.GetComponent<myBlock>();
        child.mastery = blockS[v1].mastery;
        child.domain = blockS[v1].domain;
        child.cluster = blockS[v1].cluster;
        child.description = blockS[v1].standarddescription;
        child.grade = grade;
        child.changeType(blockS[v1].mastery);
        child.originalMaterialSelection = child.GetComponent<MeshRenderer>().material;

        myBlock child2 = parentTransform.Find("Block2").gameObject.GetComponent<myBlock>();
        child2.mastery = blockS[v2].mastery;
        child2.domain = blockS[v2].domain;
        child2.cluster = blockS[v2].cluster;
        child2.description = blockS[v2].standarddescription;
        child2.grade = grade;
        child2.changeType(blockS[v2].mastery);
        child2.originalMaterialSelection = child2.GetComponent<MeshRenderer>().material;

        myBlock child3 = parentTransform.Find("Block3").gameObject.GetComponent<myBlock>();
        child3.mastery = blockS[v3].mastery;
        child3.domain = blockS[v3].domain;
        child3.cluster = blockS[v3].cluster;
        child3.description = blockS[v3].standarddescription;
        child3.changeType(blockS[v3].mastery);
        child3.grade = grade;
        child3.originalMaterialSelection = child3.GetComponent<MeshRenderer>().material;

        if (dt >= 1){
            Transform childTransform = parentTransform.Find("Block1");
            Destroy(childTransform.gameObject);
        }
        if (dt == 2){
            Transform childTransform = parentTransform.Find("Block2");
            Destroy(childTransform.gameObject);
        }
    }

   

    // Update is called once per frame
     IEnumerator GetDatas(){
        using(UnityWebRequest request= UnityWebRequest.Get(URL)){
            yield return request.SendWebRequest();
            if(request.result==UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else{
                JSONNode stackInfo = JSON.Parse(request.downloadHandler.text);

                foreach (JSONNode node in stackInfo) {
                    if (node["grade"] == grade) {
                        Stack b = new Stack();
                        b.id = node["id"].AsInt;
                        b.subject = node["subject"].Value;
                        b.grade = node["grade"].Value;
                        b.mastery = node["mastery"].AsInt;
                        b.domainid = node["domainid"].Value;
                        b.domain = node["domain"].Value;
                        b.cluster = node["cluster"].Value;
                        b.standardId = node["standardid"].Value;
                        b.standarddescription = node["standarddescription"].Value;
                        blockS.Add(b);
                    }
                    
                }
            }
        }
        //blockS.Sort();

        for (int i = 0; i <= blockS.Count; i++){
            
            if(spawned < blockS.Count){
                spawned++;
                if(spawned % 3 == 0){
                    SpawnBlocks(0, i-2, i-1, i);
                }
            } 
            if (spawned == numberOfDomains)
                if (spawned % 3 != 0){
                    int difference = (spawned%3);
                    SpawnBlocks(difference, i-2, i-1, i);
                    Debug.Log(difference);
                    break;

            }
           
            
        }
    }

    [Serializable]
    public class Stack : IComparable<Stack>
    {
            public int id;
            public string subject;
            public string grade;
            public int mastery;
            public string domainid;
            public string domain;
            public string cluster;
            public string standardId;
            public string standarddescription;
            public float value;

            public int CompareTo(Stack other){
                if (other == null) {
                    return 1;
                }
                return value.CompareTo(other.value);
            }


    }

}
