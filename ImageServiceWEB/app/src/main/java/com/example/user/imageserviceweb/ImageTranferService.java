package com.example.user.imageserviceweb;

import android.app.Service;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.IBinder;

public class ImageTranferService extends Service {

    WiFiBroadCastReceiver broadcastReceiver;

    public ImageTranferService() {
    }

    @Override
    public void onCreate() {
        super.onCreate();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        broadcastReceiver = new WiFiBroadCastReceiver();
        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        intentFilter.addAction("android.net.wifi.STATE_CHANGE");
        registerReceiver(broadcastReceiver, intentFilter);
        return super.onStartCommand(intent, flags, startId);
    }



    @Override
    public void onDestroy() {
        unregisterReceiver(broadcastReceiver);
        super.onDestroy();
    }

    @Override
    public IBinder onBind(Intent intent) {
        // TODO: Return the communication channel to the service.
        //TODO: what the fuck is this function
        throw new UnsupportedOperationException("Not yet implemented");
    }
}
