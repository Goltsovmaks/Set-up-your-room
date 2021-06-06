using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

using System.Diagnostics;
using System.Threading;
//using System;

class ObjectsByOneType
{
    private string type;
    private List<GameObject> objects;
    private List<String> strings;


    public ObjectsByOneType(string new_type, GameObject[] allObjects)
    {
        type = new_type;
        objects = new List<GameObject>();
     //   int k = 0;
     //   UnityEngine.Debug.Log("Попытка создать массив объектов заданного типа");
        for (int i = 0; i < allObjects.Length; i++)
        {
           if (allObjects[i].tag == type)
           {
             //   UnityEngine.Debug.Log("Добавляю " + k.ToString() + " объект с тэгом " + type);
             //   k++;
             //   UnityEngine.Debug.Log(allObjects[i].name);
                objects.Add(allObjects[i]);
           }
        }
    }
    public List<GameObject> GetObjects() {
        return objects;
    }
    public string GetCurrentType()
    {
        return type;
    }

}



public class LoadFromFIle : MonoBehaviour
{
    public GameObject prefabItemButton;
    public GameObject prefabItemTypeButton;
    public GameObject typeItemContent;
    public GameObject itemContent;

    AssetBundle currentLoadedAssetBundle;
    GameObject[] allObjects;
    string path;
    string assetName;

    public GameObject selectItemPanel;
    public GameObject workPanel;

    private string current_type ="";
    void Start()
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
       // currentLoadedAssetBundle = LoadAssetBundle(path);

       // path = Path.Combine(Application.streamingAssetsPath, "prefabs");
       // currentLoadedAssetBundle = LoadAssetBundle(path);

        allObjects = FindObjectOfType<BundleLoader>().GetLoadedBundle().LoadAllAssets<GameObject>(); //имеет вид:префаб/спрайт
        string first_type = CreateAndInsertTypesButtons(allObjects);
        CreateAndInsertButtons(first_type);
        
        stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                            ts.Hours, ts.Minutes, ts.Seconds,
                                            ts.Milliseconds / 10);
        UnityEngine.Debug.Log("RunTime " + elapsedTime);

        
    }


    private string CreateAndInsertTypesButtons(GameObject[] allObjects)
    {

        List<String> name_types = FindItemTypes(allObjects);

        foreach (string type in name_types)
        {
            GameObject current_type_button = LoadDataToTypeButton(type);

            current_type_button.transform.SetParent(typeItemContent.transform);
            current_type_button.transform.localScale = new Vector3(1, 1, 1);
        }
        string ans = "";
        if (name_types.Count != 0)
        {
            ans = name_types[0];
        }
        return ans;

    }

    private GameObject LoadDataToTypeButton(string type)
    {
        //создаем пустую кнопку
      //  UnityEngine.Debug.Log("Копирую префаб кнопки");
        GameObject current_item_type = Instantiate(prefabItemTypeButton);

        //задаем название
        current_item_type.transform.Find("Text").gameObject.GetComponent<Text>().text = type;

        //ПРИКРЕПИТЬ ОБРАБОТЧИК ДЛЯ КНОПКИ
        current_item_type.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { CreateAndInsertButtons(type); });
        return current_item_type;
    }

    private List<String> FindItemTypes(GameObject[] allObjects)
    {
        List<String> ans = new List<String>();
        for (int i = 0; i < allObjects.Length; ++i)
        {
       //     UnityEngine.Debug.Log("НАЗВАНИЕ ТИПА  = "  + allObjects[i].tag);
            if (!ans.Contains(allObjects[i].tag))
            {
                ans.Add(allObjects[i].tag);
            }
        }
        return ans;
    }

    private void CreateAndInsertButtons(string new_type)
    {
        if (current_type != new_type)
        {
            current_type = new_type;


            ObjectsByOneType objectsByOneType = new ObjectsByOneType(current_type, allObjects);

         /*   foreach (Transform child in itemContent.transform)
            {
                Destroy(child);
            }
            */
            var children = new List<GameObject>();
            foreach (Transform child in itemContent.transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => Destroy(child));




            for (int i = 0; i < objectsByOneType.GetObjects().Count; i++)
            {
           //     UnityEngine.Debug.Log("Добавляю " + i.ToString() + " кнопку");
           //     UnityEngine.Debug.Log("Загрузка данных в итем");
                GameObject current_item = LoadDataToButton(objectsByOneType, i);

                //add sprite and prefab
             //   UnityEngine.Debug.Log("Добавление кнопки в контент");
                //Adding to content
                current_item.transform.SetParent(itemContent.transform);
                current_item.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private GameObject LoadDataToButton(ObjectsByOneType objectsByOneType, int indexItem)
    {
        //создаем пустую кнопку
      //  UnityEngine.Debug.Log("Копирую префаб кнопки");
        GameObject current_item = Instantiate(prefabItemButton);

        //получаем нужный спрайт
      //  UnityEngine.Debug.Log("Получаю спрайт");
        Sprite current_sprite = objectsByOneType.GetObjects()[indexItem].transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;

        //присваиваем спрайт текущей кнопке
      //  UnityEngine.Debug.Log("Устанавливаю спрайт кнопки");
        current_item.transform.Find("Button").gameObject.GetComponent<UnityEngine.UI.Image>().sprite = current_sprite;
        
        //задаем название
        string name_object = objectsByOneType.GetObjects()[indexItem].name;
        current_item.transform.Find("Text").gameObject.GetComponent<Text>().text = name_object;

        //прикрепляем скрипт, для обработки кнопки.
        ChooseObject chooseObjectScript = current_item.AddComponent<ChooseObject>();

     //   UnityEngine.Debug.Log("Устанавливаю параметры скрипта кнопки");
        chooseObjectScript.SetParams(current_item.transform.Find("Button").gameObject,
                                     objectsByOneType.GetObjects()[indexItem].transform.Find("Object").gameObject,
                                     selectItemPanel,workPanel);
        return current_item;   
        
    }

    private GameObject[] LoadObjectsFromBandle()
    {
        GameObject[] loadedObjects = FindObjectOfType<BundleLoader>().GetLoadedBundle().LoadAllAssets<GameObject>();    
        for (int i = 0; i < loadedObjects.Length; i++)
        {
            UnityEngine.Debug.Log("Закачал объект: " + loadedObjects[i].name);
        }

        return loadedObjects;
    }

    private AssetBundle LoadAssetBundle(string bundleUrl)
    {
        AssetBundle loadedAssetBundle = AssetBundle.LoadFromFile(bundleUrl);

        if (loadedAssetBundle == null)
        {
            UnityEngine.Debug.Log("Failed to load AssetBundle!");
        }
        else
        {
            UnityEngine.Debug.Log("AssetBundle succesfully loaded from" + bundleUrl);
        }
        return loadedAssetBundle;
    }
    string GetContents(UnityEngine.Object[] obj)
    {
        string ans ="";
        for (int i =0; i < obj.Length; i++)
        {
            ans += obj[i].name + " with type " + obj[i].GetType() +" \n";
        }
        return ans;
    }

}