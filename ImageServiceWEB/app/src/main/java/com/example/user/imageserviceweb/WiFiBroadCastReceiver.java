package com.example.user.imageserviceweb;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.widget.Toast;
/*
A broadcast receiver that listens for changes in the wifi.
When it detects a wifi connection is available it calls the image transferring functions
in the ImageTransferer class.
 */
public class WiFiBroadCastReceiver extends BroadcastReceiver {
    /*
    A function that is called when a broadcast the receiver is programed to listen to is made.
    Calls the image transferring functions in the ImageTransferer class if WIFI is available.
     */
    @Override
    public void onReceive(Context context, Intent intent) {
        WifiManager Wifimanager = (WifiManager) context.getApplicationContext().getSystemService(Context.WIFI_SERVICE);
        NetworkInfo Info = intent.getParcelableExtra(Wifimanager.EXTRA_NETWORK_INFO);
        if (Info != null) {
            if (Info.getType() == ConnectivityManager.TYPE_WIFI) {
                if (Info.getState() == NetworkInfo.State.CONNECTED) {
                    ImageTransferer.GetInstance().TransferImages(context);
                }
            }
        }
    }



}

