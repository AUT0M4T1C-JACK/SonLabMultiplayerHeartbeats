using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private HeartBeatController HeartBeatController;
    [SerializeField] private PriceManager PriceManager;
    [SerializeField] private SceneLoader SceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("parthandpriyankaarebullies", roomOptions, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        Debug.Log("event recieved");
        //calls function based on raised event code from PUN
        switch (eventCode)
        {
            // Heartbeat controls
            case Utility.StartHeartbeatEventCode:
                if (HeartBeatController != null)
                {
                    HeartBeatController.StartHeartbeat();
                    Debug.Log("Heartbeat started");
                }
                break;
            case Utility.StopHeartbeatEventCode:
                if (HeartBeatController != null)
                {
                    HeartBeatController.StopHeartbeat();
                    Debug.Log("Heartbeat stopped");
                }
                break;
            // Price reveal controls
            case Utility.RevealPrice1EventCode:
                if (PriceManager != null)
                {
                    PriceManager.RevealPrice(0);
                }
                break;
            case Utility.RevealPrice2EventCode:
                if (PriceManager != null)
                {
                    PriceManager.RevealPrice(1);
                }
                break;
            case Utility.RevealPrice3EventCode:
                if (PriceManager != null)
                {
                    PriceManager.RevealPrice(2);
                }
                break;
            case Utility.RevealPrice4EventCode:
                if (PriceManager != null)
                {
                    PriceManager.RevealPrice(3);
                }
                break;
            case Utility.RevealPrice5EventCode:
                if (PriceManager != null)
                {
                    PriceManager.RevealPrice(4);
                }
                break;
            case Utility.HideAllPricesEventCode:
                if (PriceManager != null)
                {
                    PriceManager.HideAllPrices();
                }
                break;
            // Scene switching
            case Utility.LoadScene1EventCode:
                if (SceneLoader != null)
                {
                    SceneLoader.LoadScene1();
                    Debug.Log("Loading Scene 1");
                }
                break;
            case Utility.LoadScene2EventCode:
                if (SceneLoader != null)
                {
                    SceneLoader.LoadScene2();
                    Debug.Log("Loading Scene 2");
                }
                break;
            case Utility.LoadScene3EventCode:
                if (SceneLoader != null)
                {
                    SceneLoader.LoadScene3();
                    Debug.Log("Loading Scene 3");
                }
                break;
        }
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
