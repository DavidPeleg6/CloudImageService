package com.example.user.imageserviceweb;

import android.annotation.TargetApi;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.support.v4.app.NotificationCompat;
import android.support.v4.app.NotificationManagerCompat;
import android.util.Log;

/*
This class displays the progress bar.
Since quit a few lines of code were needed to display and update the progress bar I decided to
make it a separate class.
 */
@TargetApi(28)
public class ProgressBar {
    NotificationCompat.Builder builder;
    NotificationManagerCompat notificationManager;
    public static final String NOTIFICATION_CHANNEL_ID = "4655";
    public static final String NOTIFICATION_CHANNEL_NAME = "PROGBAR";
    int Count = 0;
    int NotificationID = 1;
    /*
    Constructor, makes a new progress bar that will be displayed in 'context'
    and will count up to 'count'.
     */
    public ProgressBar(Context context, int count) {
        Count = count;
        int importance = NotificationManager.IMPORTANCE_LOW;
        NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, NOTIFICATION_CHANNEL_NAME, importance);
        notificationChannel.enableLights(true);
        notificationChannel.setLightColor(Color.GREEN);
        notificationChannel.enableVibration(true);
        notificationChannel.setVibrationPattern(new long[]{100, 200, 300, 400, 500, 400, 300, 200, 400});

        NotificationManager notificationManagerr = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
        notificationManagerr.createNotificationChannel(notificationChannel);
        notificationManager = NotificationManagerCompat.from(context);
        builder = new NotificationCompat.Builder(context, NOTIFICATION_CHANNEL_ID);
        builder.setContentTitle("Image Transfer")
                .setContentText("0/" + Count)
                .setSmallIcon(R.drawable.ic_launcher_background)
                .setProgress(Count,0,false)
                .setAutoCancel(true)
                .setPriority(NotificationCompat.PRIORITY_LOW);
    }
    /*
    Updates the current value of the progress bar.
     */
    public void ProgressUpdate(Integer progress) {
        builder.setContentText(progress + "/" + Count)
                .setProgress(Count, progress, false);

        notificationManager.notify(NotificationID, builder.build());
    }
    /*
    A function that should be called after the progress bar has reached its end.
    Sets the text of the progress bar to indicate that the process has completed.
     */
    public void PostExecute() {
        builder.setContentText("Transfer complete")
                .setProgress(0, 0, false);
        notificationManager.notify(NotificationID, builder.build());
    }
}
