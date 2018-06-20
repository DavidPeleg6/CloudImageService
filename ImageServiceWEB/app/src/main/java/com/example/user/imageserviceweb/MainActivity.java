package com.example.user.imageserviceweb;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;
/*
The main activity of the program (and also the only activity).
Contains two buttons, one to start the service and another to stop it.
 */
public class MainActivity extends AppCompatActivity {

    Button ButtonStart;
    Button ButtonStop;
    Intent ServiceIntent;
    boolean ServiceRunning = false;
    /*
    Constructor, sets the actions of the two buttons.
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ServiceIntent = new Intent(MainActivity.this, ImageTransferService.class);

        ButtonStart = (Button)findViewById(R.id.buttonStart);
        ButtonStop = (Button)findViewById(R.id.buttonStop);

        /*
        The action that the start button will execute when pressed.
        Only does anything if the service isn't currently already running.
        If it's not already running, it starts it
        and displays a toast message stating that it has done so.
         */
        ButtonStart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (ServiceRunning)
                    return;
                Toast.makeText(MainActivity.this, "Service started", Toast.LENGTH_SHORT).show(); //TODO: maybe put this below startService
                startService(ServiceIntent);
                ServiceRunning = true;
            }
        });
        /*
        The action that the stop button will execute when pressed.
        Only does anything if the service is currently running.
        If it's already running, it stops it
        and displays a toast message stating that it has done so.
         */
        ButtonStop.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!ServiceRunning)
                    return;
                Toast.makeText(MainActivity.this, "Service stopped", Toast.LENGTH_SHORT).show(); //TODO: maybe put this below stopService
                stopService(ServiceIntent);
                ServiceRunning = false;
            }
        });
    }
}
