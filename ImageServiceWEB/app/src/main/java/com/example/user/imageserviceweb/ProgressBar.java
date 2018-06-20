package com.example.user.imageserviceweb;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.NotificationCompat;

/*
This class displays the progress bar.
Since quit a few lines of code were needed to display and update the progress bar I decided to
make it a separate class.
 */
public class ProgressBar {
    NotificationCompat.Builder NotificationBuilder;
    NotificationManager NotificationManager;
    int id = 10;
    int Count = 0;
    /*
    Constructor, makes a new progress bar that will be displayed in 'context'
    and will count up to 'count'.
     */
    public ProgressBar(Context context, int count) {
        Count = count;
        NotificationManager = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
        NotificationBuilder = new NotificationCompat.Builder(context);
        NotificationBuilder.setSmallIcon(R.drawable.ic_launcher_background)
                .setContentTitle("Progress bar in progress") //TODO: put text describing the process here
                .setProgress(Count,0,false)
                .setAutoCancel(true)
                .setWhen(System.currentTimeMillis());
        Intent StartIntent = new Intent(context.getApplicationContext(),MainActivity.class);
        PendingIntent ContentIntent = PendingIntent.getActivity(context, 1, StartIntent, 0);
        NotificationBuilder.setContentIntent(ContentIntent);

        NotificationManager.notify(id, NotificationBuilder.build());
    }
    /*
    Updates the current value of the progress bar.
     */
    protected void ProgressUpdate(Integer progress) {
        NotificationBuilder.setContentText(""+progress+"");
        NotificationBuilder.setProgress(Count, progress,false);
        NotificationManager.notify(id, NotificationBuilder.build());
    }
    /*
    A function that should be called after the progress bar has reached its end.
    Sets the text of the progress bar to indicate that the process has completed.
     */
    public void PostExecute() {
        NotificationBuilder.setContentText("Progress completed"); //TODO: put text describing the process here
        NotificationManager.notify(id, NotificationBuilder.build());
    }
}
