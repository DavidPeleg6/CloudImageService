package com.example.user.imageserviceweb;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.NotificationCompat;

/*
This class displays the progress bar.
 */
public class ProgressBar {

    NotificationCompat.Builder NotificationBuilder;
    NotificationManager NotificationManager;
    int id = 10;
    int Count = 0;

    public ProgressBar(Context context, int count) {
        Count = count;
        NotificationManager = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
        NotificationBuilder = new NotificationCompat.Builder(context);
        NotificationBuilder.setSmallIcon(R.drawable.ic_launcher_background)
                .setContentTitle("Progress bar in progress") //TODO: put text describing the process here
                .setProgress(Count,0,false)
                .setAutoCancel(true)
                .setWhen(System.currentTimeMillis());
        Intent startIntent = new Intent(context.getApplicationContext(),MainActivity.class);
        PendingIntent contentIntent = PendingIntent.getActivity(context, 1, startIntent, 0);
        NotificationBuilder.setContentIntent(contentIntent);

        NotificationManager.notify(id, NotificationBuilder.build());
    }
    protected void ProgressUpdate(Integer progress) {
        NotificationBuilder.setContentText(""+progress+"%");
        NotificationBuilder.setProgress(Count, progress,false);
        NotificationManager.notify(id, NotificationBuilder.build());
    }

    public void PostExecute() {
        NotificationBuilder.setContentText("Progress completed"); //TODO: put text describing the process here
        NotificationManager.notify(id, NotificationBuilder.build());
    }
}
