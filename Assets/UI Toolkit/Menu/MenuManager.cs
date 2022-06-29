using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Mirror.Discovery;
using Mirror;

public class MenuManager : MonoBehaviour
{
    public Button findServer;
    public NetworkDiscovery networkDiscovery;
    // ====== Discovered Server PART
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    Dictionary<string, long> discoveredKey = new Dictionary<string, long>();
    List<string> discoveredServersList = new List<string>();
    // Button serverIpName;
    public VisualTreeAsset listEntryTemplate;
    ListView serverList;
    public string originFindServerText = "";
    public string inSearchFindServertext = "";
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        findServer = root.Q<Button>("FindServerBtn");
        findServer.clicked += FindServerButtonPressed;
        if (originFindServerText == "")
        {
            originFindServerText = findServer.text;
        }
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
        serverList.onSelectionChange += OnServerSelected;
    }

    void FindServerButtonPressed()
    {
        if (findServer.text == inSearchFindServertext)
        {
            networkDiscovery.StopDiscovery();
            discoveredServers.Clear();
            findServer.text = originFindServerText;
        }
        else
        {
            findServer.text = inSearchFindServertext;
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
        }
    }

    private void OnServerSelected(IEnumerable<object> selectedServers)
    {
        var selectedServer = serverList.selectedItem as string;
        Connect(discoveredServers[discoveredKey[selectedServer]]);
    }
    void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.StartClient(info.uri);
        SceneManager.LoadScene("Mk - Controller");
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        // Note that you can check the versioning to decide if you can connect to the server or not using this method
        discoveredServers[info.serverId] = info;
        if (!discoveredServersList.Contains(info.EndPoint.Address.ToString()))
        {
            discoveredServersList.Add(info.EndPoint.Address.ToString());
            discoveredKey.Add(info.EndPoint.Address.ToString(), info.serverId);
        }
        FillServerList();
    }
}
