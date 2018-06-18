package com.example.user.imageserviceweb;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.support.v4.app.NotificationCompat;
import android.widget.Toast;

public class ImageTranferService extends Service {

    NotificationCompat.Builder builder;
    NotificationManager manager;
    int id = 10;

    public ImageTranferService() {
    }

    @Override
    public void onCreate() {
        super.onCreate();
        Toast.makeText(this, "service onCreate", Toast.LENGTH_SHORT).show(); //TODO: delete this
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this, "service onStartCommand", Toast.LENGTH_SHORT).show(); //TODO: delete this


        manager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        builder = new NotificationCompat.Builder(this);
        builder.setSmallIcon(R.drawable.ic_launcher_background)
                .setContentTitle("Progress bar in progress") //TODO: put text describing the process here
                .setProgress(100,0,false)
                .setAutoCancel(true)
                .setWhen(System.currentTimeMillis());
        Intent startIntent = new Intent(getApplicationContext(),MainActivity.class);
        PendingIntent contentIntent = PendingIntent.getActivity(this, 1, startIntent, 0);
        builder.setContentIntent(contentIntent);

        manager.notify(id, builder.build());

        //TODO: delete this shit
        for (int i = 0; i < 100; i++) {
            onProgressUpdate(i);
            try {
                Thread.sleep(50); //TODO: replace this with something actually useful
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        onPostExecute(null);
        //TODO: deletezone end

        return super.onStartCommand(intent, flags, startId);
    }

    protected void onProgressUpdate(Integer progress) {
        builder.setContentText(""+progress+"%");
        builder.setProgress(100, progress,false);
        manager.notify(id, builder.build());
    }

    protected void onPostExecute(Void result) {
        builder.setContentText("Progress completed"); //TODO: put text describing the process here
        manager.notify(id, builder.build());
    }

    @Override
    public void onDestroy() {
        Toast.makeText(this, "service onDestroy", Toast.LENGTH_SHORT).show();
        //TODO: make shit get deleted
        super.onDestroy();
    }

    @Override
    public IBinder onBind(Intent intent) {
        // TODO: Return the communication channel to the service.
        throw new UnsupportedOperationException("Not yet implemented");
    }
}
