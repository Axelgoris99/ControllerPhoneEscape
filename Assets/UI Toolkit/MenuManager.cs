using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Mirror.Discovery;
public class MenuManager : MonoBehaviour
{
    public Button findServer;
    public NetworkDiscovery networkDiscovery;
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    List<string> discoveredServersList = new List<string>();
    // Button serverIpName;
    public VisualTreeAsset listEntryTemplate;
    ListView serverList;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        findServer = root.Q<Button>("FindServerBtn");
        findServer.clicked += FindServerButtonPressed;

        // serverIpName = root.Q<Button>("Ip");
        serverList = root.Q<ListView>("ListServer");
    }

    void FillServerList()
    {
        // Set up a make item function for a list entry
        serverList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = listEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new serverListEntryController();

            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            return newListEntry;
        };
        serverList.bindItem = (item, index) =>
        {
            (item.userData as serverListEntryController).SetServerData(discoveredServersList[index]);
        };

        serverList.fixedItemHeight = 45;
        serverList.itemsSource = discoveredServersList;
    }

    void FindServerButtonPressed()
    {
        if (findServer.text == "Stop Searching")
        {
            networkDiscovery.StopDiscovery();
            discoveredServers.Clear();
            findServer.text = "Search for servers";
        }
        else
        {
            findServer.text = "Stop Searching";
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
        }
    }
    public void OnDiscoveredServer(ServerResponse info)
    {
        // Note that you can check the versioning to decide if you can connect to the server or not using this method
        discoveredServers[info.serverId] = info;
        if (!discoveredServersList.Contains(info.EndPoint.Address.ToString()))
        {
            discoveredServersList.Add(info.EndPoint.Address.ToString());
        }
        FillServerList();
        // Debug.Log(info.EndPoint.Address.ToString());
        // serverIpName.text = info.EndPoint.Address.ToString();
        // Debug.Log(info);
    }
}
