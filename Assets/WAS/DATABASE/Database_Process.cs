using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

public static class Database_Process
{
    private const  string       firebase_database_url = "https://was-hci-3d.firebaseio.com/";
    private static HttpClient   db_manager            = new HttpClient();

    // Static DB function
    public static void Add_Target_Into_Database(Survaillence_Target survaillence_target)
    {
        Task.Run(() => { Push_Database(survaillence_target); });
    }

    private static async Task Push_Database(Survaillence_Target survaillence_target)
    {
        var json     = JsonConvert.SerializeObject(survaillence_target);
        var data     = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await db_manager.PostAsync(firebase_database_url, data);
        var result   = await response.Content.ReadAsStringAsync();
    }

}
