package com.example.user.imageserviceweb;

import android.app.Service;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.IBinder;
/*
The service that runs in the background once started using the buttons in the main activity.
Does everything that the assignment instructions said it should do.
 */
public class ImageTransferService extends Service {

    WiFiBroadCastReceiver BroadcastReceiverWiFi;
    /*
    Constructor, does nothing.
     */
    public ImageTransferService() {
    }
    /*
    A function that is called when the service is created.
    Doesn't differ from the parent Service class.
     */
    @Override
    public void onCreate() {
        super.onCreate();
    }
    /*
    A function that is called when the service is started.
    Make a new WiFiBroadCastReceiver and sets it to listen to events relating to changes
    in the wifi connectivity status.
     */
    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        BroadcastReceiverWiFi = new WiFiBroadCastReceiver();
        IntentFilter IntentFilterWiFi = new IntentFilter();
        IntentFilterWiFi.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        IntentFilterWiFi.addAction("android.net.wifi.STATE_CHANGE");
        registerReceiver(BroadcastReceiverWiFi, IntentFilterWiFi);
        return super.onStartCommand(intent, flags, startId);
    }


    /*
    A function that is called when the service is being destroyed.
    unregisters the listener which was registered in the onStartCommand function.
     */
    @Override
    public void onDestroy() {
        unregisterReceiver(BroadcastReceiverWiFi);
        super.onDestroy();
    }
    /*
    Not used.
     */
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }
}
