package com.example.user.imageserviceweb;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.widget.Toast;

public class WiFiBroadCastReceiver extends BroadcastReceiver {
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

