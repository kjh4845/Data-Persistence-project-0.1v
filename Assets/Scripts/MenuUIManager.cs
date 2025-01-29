using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance;
    public TMP_InputField InputName;
    public TMP_Text MenuBestScore;
    public string Name;
    public int Score;
    [System.Serializable]
    class SaveData{
        public string BestName;
        public int BestScore;
    }

    public void Awake(){
        if(Instance!=null){
            Destroy(gameObject);
            return;
        }
        LoadScore();
        MenuBestScore.text = $"BestScore:{Name}:{Score}";
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartNew(){
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    public void Exit(){
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    public void LoadScore(){
        string path = Application.persistentDataPath+"/savefile.json";
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Name=data.BestName;
            Score=data.BestScore;
        }
    }
    public void ActiveButton(){
        gameObject.SetActive(true);
    }
}