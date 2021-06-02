using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;



class ObjectsByOneType
{
    private string type;
    private List<GameObject> objects;
    private List<String> strings;


    public ObjectsByOneType(string new_type, GameObject[] allObjects)
    {
        type = new_type;
        objects = new List<GameObject>();
        int k = 0;
        Debug.Log("Попытка создать массив объектов заданного типа");
        for (int i = 0; i < allObjects.Length; i++)
        {
           if (allObjects[i].tag == type)
           {
                Debug.Log("Добавляю " + k.ToString() + " объект с тэгом " + type);
                k++;
                Debug.Log(allObjects[i].name);
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
        path = Path.Combine(Application.streamingAssetsPath, "prefabs");
        currentLoadedAssetBundle = LoadAssetBundle(path);
        allObjects = LoadObjectsFromBandle(); //имеет вид:префаб/спрайт

        string first_type = CreateAndInsertTypesButtons(allObjects);

      //  ObjectsByOneType objectsByOneType = new ObjectsByOneType("other",allObjects);
        CreateAndInsertButtons(first_type);

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
        Debug.Log("Копирую префаб кнопки");
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
            Debug.Log("НАЗВАНИЕ ТИПА  = "  + allObjects[i].tag);
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
                Debug.Log("Добавляю " + i.ToString() + " кнопку");
                Debug.Log("Загрузка данных в итем");
                GameObject current_item = LoadDataToButton(objectsByOneType, i);

                //add sprite and prefab
                Debug.Log("Добавление кнопки в контент");
                //Adding to content
                current_item.transform.SetParent(itemContent.transform);
                current_item.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private GameObject LoadDataToButton(ObjectsByOneType objectsByOneType, int indexItem)
    {
        //создаем пустую кнопку
        Debug.Log("Копирую префаб кнопки");
        GameObject current_item = Instantiate(prefabItemButton);

        //получаем нужный спрайт
        Debug.Log("Получаю спрайт");
        Sprite current_sprite = objectsByOneType.GetObjects()[indexItem].transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;

        //присваиваем спрайт текущей кнопке
        Debug.Log("Устанавливаю спрайт кнопки");
        current_item.transform.Find("Button").gameObject.GetComponent<UnityEngine.UI.Image>().sprite = current_sprite;
        
        //задаем название
        string name_object = objectsByOneType.GetObjects()[indexItem].name;
        current_item.transform.Find("Text").gameObject.GetComponent<Text>().text = name_object;

        //прикрепляем скрипт, для обработки кнопки.
        ChooseObject chooseObjectScript = current_item.AddComponent<ChooseObject>();

        Debug.Log("Устанавливаю параметры скрипта кнопки");
      //  Debug.Log("передаю " + current_item.transform.Find("Object").gameObject.na)
        chooseObjectScript.SetParams(current_item.transform.Find("Button").gameObject,
                                     objectsByOneType.GetObjects()[indexItem].transform.Find("Object").gameObject,
                                     selectItemPanel,workPanel);
        return current_item;   
        
    }

    private GameObject[] LoadObjectsFromBandle()
    {
        GameObject[] loadedObjects = currentLoadedAssetBundle.LoadAllAssets<GameObject>();    
        
        for (int i = 0; i < loadedObjects.Length; i++)
        {
            Debug.Log("Закачал объект: " + loadedObjects[i].name);
        }

        return loadedObjects;
    }

    private AssetBundle LoadAssetBundle(string bundleUrl)
    {
        AssetBundle loadedAssetBundle = AssetBundle.LoadFromFile(bundleUrl);

        if (loadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
        }
        else
        {
           Debug.Log("AssetBundle succesfully loaded from" + bundleUrl);
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