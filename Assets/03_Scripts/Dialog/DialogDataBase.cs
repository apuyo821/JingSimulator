using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class DialogDataBase : MonoBehaviour
{
    // 스프레드시트 URL (공개 설정 필요)
    string googleSheetURL = "https://docs.google.com/spreadsheets/d/1gYlGd9_E6pMzsQJdEC3KscSP63KA1T7ncnkOV7wOgAU/export?format=tsv";
    private DialogSystem dialogSystem;

    private void Start()
    {
        dialogSystem = GetComponent<DialogSystem>();
        StartCoroutine(GetSheetData());
    }

    IEnumerator GetSheetData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(googleSheetURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                string sheetData = www.downloadHandler.text;
                Debug.Log("Data successfully retrieved!");
                ProcessData(sheetData);
            }
        }
    }

    // 구글 스프레드 시트의 데이터를 행별로 분류
    private void ProcessData(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            Debug.LogError("데이터 없음.");
            return;
        }

        // 첫 번째 행을 제외하고 데이터 추출
        string[] rows = data.Split('\n');
        DialogData[] dialogs = new DialogData[rows.Length-1];

        // 첫 번째 행을 제외
        for (int i = 1; i < rows.Length; i++) 
        {
            if (!string.IsNullOrEmpty(rows[i]))
            {
                string[] columns = rows[i].Split('\t');
                dialogs[i-1] = new DialogData { speakerIndex = int.Parse(columns[0]), name = columns[1], dialogSentence = columns[2] };
            }
        }

        dialogSystem.dialogs = dialogs;
    }
}
