using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    private static InternetChecker _instance;
    public static InternetChecker instance
    {
        get { return _instance; }
    }

    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.0f;

    private Ping ping;
    private float pingStartTime;

    public bool isInternetAvailable;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        CheckInternet();
    }

    public void CheckInternet()
    {
        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = allowCarrierDataNetwork;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }

    private void Update()
    {
        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
            {
                if (ping.time >= 0)
                    InternetAvailable();
                else
                    InternetIsNotAvailable();
            }
            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;
            else
                InternetIsNotAvailable();
            if (stopCheck)
                ping = null;
        }
    }

    private void InternetIsNotAvailable()
    {
        Debug.Log("Internet : false");
        isInternetAvailable = false;
    }

    private void InternetAvailable()
    {
        Debug.Log("Internet : true");
        isInternetAvailable = true;
    }
}