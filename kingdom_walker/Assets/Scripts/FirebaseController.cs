using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase;
//using Firebase.Database;
//using Firebase.Unity.Editor;

public class FirebaseController : MonoBehaviour
{
    //private string DATA_URL = "https://kingdomx-efa6a.firebaseio.com/";

    //public DatabaseReference db;
    //private string stat = "statistics";

    // Start is called before the first frame update
    void Start()
    {

        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);

        

        //Debug.Log(db);
    }

    public void SaveStatistics(string deviceUID, string dt, string lvl, string type, string tN, string gameTime)
    {
        // what statisics we need to save
        // User ID (device id)
        // Device Date
        // Current level
        // Type of save On load/ on End level/ On exist from application
        // Number of turns
        //db = FirebaseDatabase.DefaultInstance.RootReference;

        string data = deviceUID + "," + dt + "," + lvl + "," + type + "," + tN + "," + gameTime;

        //Debug.Log(UID);

        //string data = JsonUtility.ToJson(toJSON);

        //Debug.Log(db);

        //db.Child("stats").Push().SetValueAsync(data);
        //db.Child("stats").Push();
        //db.Child("stats").SetValueAsync(UID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
