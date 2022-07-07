using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ************************************************************************************************************************************************************************************************
// Simple implementation of dependency injection design pattern
// ************************************************************************************************************************************************************************************************

// Interface that defines the different services (methods) to be implemented (item and bot objects)
public interface IObject
{
    GameObject CreateObject();
}

// Class to instantiate an item that implements IObject
public class ItemObject : IObject
{
    // Instantiate an item object in a random position
    public GameObject CreateObject()
    {
        var itemLimits = new Vector3(Random.Range(-20f, 20f), Random.Range(-2f, 2f), Random.Range(-20f, 20f));
        return GameObject.Instantiate(Resources.Load("Item") as GameObject, new Vector3(itemLimits.x, itemLimits.y, itemLimits.z), Quaternion.identity);
    }
}

// Class to instatiate a bot that implements IObject
public class BotObject : IObject
{
    // Instantiate a bot object in a random position
    public GameObject CreateObject()
    {
        var botLimits = new Vector3(Random.Range(-20f, 20f), Random.Range(-2f, 2f), Random.Range(-20f, 20f));
        return GameObject.Instantiate(Resources.Load("Bot") as GameObject, new Vector3(botLimits.x, botLimits.y, botLimits.z), Quaternion.identity);
    }
}

// Central class to manage items and bots 
public class GeneralObject 
{
    private IObject _obj;

    public GeneralObject(IObject obj)
    {
        _obj = obj;
    }

    public GameObject AddObject()
    {
        return _obj.CreateObject();
    }
}

// ************************************************************************************************************************************************************************************************
// Game Manager class
// ************************************************************************************************************************************************************************************************
public class GameManager : MonoBehaviour
{
    [Tooltip("Object that represents the player in the scene")]
    public GameObject player;

    [Header("Items Properties")]
    [Tooltip("Material for the item base color")]
    public Material itemBaseMaterial;
    [Tooltip("Material for the item color when is highlighted")]
    public Material itemHighlightMaterial;

    // variables to pick the color of the items when they are highlighted or not highlighted.
    public Color itemBaseColor;
    public Color itemHighlightColor;

    [Header("Bots Properties")]
    [Tooltip("Material for the bot base color")]
    public Material botBaseMaterial;
    [Tooltip("Material for the bot color when is highlighted")]
    public Material botHighlightMaterial;

    // variables to pick the color of the bots when they are highlighted or not highlighted.
    public Color botBaseColor;
    public Color botHighlightColor;

    // List that saves all the items and bots in the scene
    List<GameObject> objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeObjects();
        InitializeProperties();
    }

    // Update is called once per frame
    void Update()
    {
        CheckClosestDistance();
        InstantiateOnKeyPressed();
    }

    // Method that instantiates the first 5 items and 5 bots in the scene (used in the start method)
    void InitializeObjects()
    {
        for (int i = 1; i <= 5; i++)
        {
            // Instatntiate first 5 items automatically
            SpawnObject("item");

            // Instatntiate first 5 bots automatically
            SpawnObject("bot");
        }
    }

    // Method that sets the properties of the materials and colors that each object (item and bot) will get
    void InitializeProperties()
    {
        itemBaseMaterial.color = itemBaseColor;
        itemHighlightMaterial.color = itemHighlightColor;
        botBaseMaterial.color = botBaseColor;
        botHighlightMaterial.color = botHighlightColor;
    }

    // Method that checks the closest object (item or bot) to the player 
    public void CheckClosestDistance()
    {
        // variable that keeps track of the closest distance between an object (item or bot) and the player
        float closeDistance = float.MaxValue;

        // variable that sets which gameobject (item or bot) is the closest one to the player
        GameObject closestObject = null;

        // Traverse the list 
        foreach (GameObject obj in objects)
        {
            //Assign base materials to items and bots
            if (obj.tag == "Item")
                obj.GetComponent<Renderer>().material = itemBaseMaterial;
            else if (obj.tag == "Bot")
                obj.GetComponent<Renderer>().material = botBaseMaterial;

            if (Vector3.Distance(player.transform.position, obj.transform.position) <= closeDistance)
            {
                // Get closest object
                closeDistance = Vector3.Distance(player.transform.position, obj.transform.position);
                closestObject = obj;
            }
        }
        // Assign highlighted material to either the closest item or closest bot
        if (closestObject.tag == "Item")
            closestObject.GetComponent<Renderer>().material = itemHighlightMaterial;
        else if (closestObject.tag == "Bot")
            closestObject.GetComponent<Renderer>().material = botHighlightMaterial;
    }

    // Method to instantiate an item or a bot when pressing a key
    void InstantiateOnKeyPressed()
    {
        // I key to spawn items
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnObject("item");
        }
        // B key to spawn bots
        else if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnObject("bot");
        }
    }

    // Method that spawns a new object, either an item or a bot
    void SpawnObject(string objectType)
    {
        switch (objectType)
        {
            case "item":
                GeneralObject itemObject = new GeneralObject(new ItemObject());
                GameObject item = itemObject.AddObject();
                objects.Add(item);
                break;
            case "bot":
                GeneralObject botObject = new GeneralObject(new BotObject());
                GameObject bot = botObject.AddObject();
                objects.Add(bot);
                break;
        }
    }
}
