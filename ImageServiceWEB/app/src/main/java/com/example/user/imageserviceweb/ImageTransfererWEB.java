package com.example.user.imageserviceweb;

import android.content.Context;
import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.util.Log;

import java.io.ByteArrayOutputStream;
import java.io.File;

/*
This class is used by ImageTransferer in order to perform the file transfer in a separate thread.
 */
public class ImageTransfererWEB extends AsyncTask {
    private ProgressBar ImageTransferProgressBar;
    private File dcim;
    private TCPFileSender Sender;
    private Context context;

    /*
    Constructor, takes the context the code in running in (MainActivity) and the File path to the image folder.
     */
    public ImageTransfererWEB(Context context, File dcimm) {
        this.context = context;
        this.dcim = dcimm;
        this.Sender = new TCPFileSender();
    }
    /*
    The code that is run in the background when this class is executed.
    Transfers images from the DCIM folder to the server one by one
    while displaying the progress via a progress bar notification.
     */
    @Override
    protected Object doInBackground(Object[] objects) {
        //File[] Pics = (File[])getAllImages(dcim).toArray();
        File[] Pics = dcim.listFiles();
        if(Pics == null) return null;
        //Create a progress bar
        ImageTransferProgressBar = new ProgressBar(context, Pics.length);
        ImageTransferProgressBar.ProgressUpdate(0);
        int progress = 0;
        try {
            Sender.ConnectToServer("10.0.2.2", 9100);
            for (File Picture : Pics) {
                Sender.SendImageFile(Picture);
                Thread.sleep(1000);
                ImageTransferProgressBar.ProgressUpdate(++progress);
            }
        } catch(Exception e) {
        return null; }
        Sender.Disconnect();
        ImageTransferProgressBar.PostExecute();
        return null;
    }
}
