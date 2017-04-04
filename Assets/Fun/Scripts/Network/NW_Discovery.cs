using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// NW_Discovery is a simple unet hlapi broadcast receiver script
/// Mode
///     Both: -start listening
///             -if a server suitable is broadcasting and send "found" event
///             -if not - start broadcasting and send "start" event
///     Server:     -start broadcasting and sedn "start" event
///     Client: -start listening and send "found" if a suitable server is broadcasting
/// </summary>
public enum DiscoveryMode {
        Both, Server, Client
    }
public class NW_Discovery : NetworkDiscovery {
    public delegate void NWDiscoverDelegateString(string ip, string data);
    public delegate void NWDiscoverDelegateSimple();
    public static event NWDiscoverDelegateString EventFoundServer;
    public static event NWDiscoverDelegateSimple EventStartServer;

    private static NW_Discovery instance;

    private string gameName = "sobgob";
    private IEnumerator initiateServerStartCoroutine;
    #region unity callbacks
    private void Awake() {
        instance = this;
    }
    void Start() {
        //NetworkServer.Reset();
        // initialize discovery
        Initialize();
        //// set data
        broadcastData = gameName;

        StartCoroutine(StartAsClientDelayed());

        //// set broadcast coroutine
        initiateServerStartCoroutine = InitiateServerStart(5.0f);
        //// start broadcast coroutine
        StartCoroutine(initiateServerStartCoroutine);
    }

    public override void OnReceivedBroadcast(string fromAddress, string data) {
        //base.OnReceivedBroadcast(fromAddress, data);
        print("R " + fromAddress);

        // check if data is matching
        if (!data.Contains(gameName))
            return;
        // prevent broadcasting (Server) start
        if (initiateServerStartCoroutine != null)
            StopCoroutine(initiateServerStartCoroutine);
        // send found server event
        // !!! NetworManager must handle/not handle furhter events (when it's allready connected)!!!!
        if (EventFoundServer != null)
            EventFoundServer(fromAddress, data);

        if (NetworkManager.singleton.IsClientConnected())
            return;

        // set ip and start client
        NetworkManager.singleton.networkAddress = fromAddress;
        NetworkManager.singleton.StartClient();
    }

    #endregion
    // TODO: this is only needed becaus of a likely unity bug
    IEnumerator StartAsClientDelayed() {
        yield return 0;
        Initialize(); // second init is necessary!
        StartAsClient();
    }

    private IEnumerator InitiateServerStart(float delay) {
        yield return new WaitForSeconds(delay);
        print("PENG");

        // stop listening
        if (isClient)
            StopBroadcast();
        else
            Initialize();

        // send start server event
        if (EventStartServer != null)
            EventStartServer();

        // start server
        NetworkManager.singleton.StartHost();
        // start broadcasting
        StartAsServer();
    }

    public static void StopDiscovery() {
        // check if running
        if (!instance.isClient && !instance.isServer)
            return;
        print(MethodBase.GetCurrentMethod().Name);
        instance.StopBroadcast();
    }
}