using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonReader : MonoBehaviour
{
    public MembersList MemberList = new MembersList();
  
    [Header("Table design")]
    public Text Title;
    public Text[] header;
    [Header("Members values")]
    public GameObject prefab;
    public List<GameObject> prefabs = new List<GameObject>();
    GameObject go;
    string path;
    string jsonString;

    void Start()
    {
        AddToTable();
    }

    //If you want it to reload automatically, uncomment this method
    //void Update()
    //{
    //    ReloadTable();
    //}

    public void ReloadTable()
    {
        //Clear Table and instantiate Json values.
        foreach (GameObject button in prefabs)
        {
            Destroy(button);
        }
        prefabs.Clear();
        AddToTable();
    }
    #region Logic
    void AddToTable()
    {
        //Find Json in project
        path = Application.streamingAssetsPath + "/JsonChallenge.json";
        jsonString = File.ReadAllText(path);
        //Check if Json is null
        if (jsonString != null)
        {

            MemberList = JsonUtility.FromJson<MembersList>(jsonString);
            Table table = JsonUtility.FromJson<Table>(jsonString);
            //Set Title and Headers
            Title.text = table.Title;
            header[0].text = table.ColumnHeaders[0];
            header[1].text = table.ColumnHeaders[1];
            header[2].text = table.ColumnHeaders[2];
            header[3].text = table.ColumnHeaders[3];
            //Instantiate members into table.
            foreach (Members members in MemberList.Members)
            {
                go = Instantiate(prefab, new Vector3(0, 0, 90f), Quaternion.identity) as GameObject;
                go.transform.parent = GameObject.Find("Content").transform;
                prefabs.Add(go);
                go.transform.localScale = new Vector3(1f, 1f, 1f);
                go.transform.GetChild(0).GetComponent<Text>().text = members.ID;
                go.transform.GetChild(1).GetComponent<Text>().text = members.Name;
                go.transform.GetChild(2).GetComponent<Text>().text = members.Role;
                go.transform.GetChild(3).GetComponent<Text>().text = members.Nickname;
            }
        }
        else
        {
            print("Json is null, add Json first into StreamingAssets");
        }
    }
    #endregion


}

    [System.Serializable]
    public class Table
    {
        public string Title;
        public string[] ColumnHeaders;
    }