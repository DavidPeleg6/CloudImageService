package com.example.user.imageserviceweb;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.widget.Toast;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;

public class ImageTransfererWEB extends AsyncTask {
    private ProgressBar p;
    private File dcim;
    private Context context;
    public ImageTransfererWEB(Context context, File dcim) {
        this.context = context;
        this.dcim = dcim;
    }

    @Override
    protected Object doInBackground(Object[] objects) {
        //TODO : check if casting doesnt fuck everything up
        //File[] Pics = (File[])getAllImages(dcim).toArray();
        File[] Pics = dcim.listFiles();
        if(Pics == null) return null;
        //Create a progress bar
        ProgressBar p = new ProgressBar(context, Pics.length);
        int progress = 0;
        try {
            //InetAddress serverAddr = InetAddress.getByName("10.0.2.2"); TODO: return to this
            InetAddress serverAddr = InetAddress.getByName("5.29.216.162");
            // create a socket to make the connection with the server
            Socket socket = new Socket(serverAddr, 8000);
            OutputStream output = socket.getOutputStream();
            for (File pic : Pics) {
                //create an input stream of the pic
                FileInputStream fis;
                try { fis = new FileInputStream(pic); } catch (FileNotFoundException e) { break; }
                //convert image into byte array
                Bitmap bm = BitmapFactory.decodeStream(fis);
                byte[] imgbyte = GetBytesFromBitmap(bm);
                //send shit to server
                output.write(imgbyte.length);
                output.write(imgbyte);
                output.flush();
                //TODO: if shit is down add this
                //Thread.sleep(100);
                p.ProgressUpdate(++progress);
            }
        } catch(Exception e) { Toast.makeText(context, e.getMessage(), Toast.LENGTH_SHORT).show();//TODO this line is bad
        return null; }
        p.PostExecute();
        return null;
    }

    public byte[] GetBytesFromBitmap(Bitmap bitmap) {
        ByteArrayOutputStream stream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG, 70, stream);
        return stream.toByteArray();
    }

/*
    public List<File> getAllImages(File root) {
        //get a list of files in the current directory
        List<File> images = Arrays.asList(root.listFiles());
        //TODO : check whether this needs to be added
        //if(images == null) return null;
        for(File file : images) {
            if(file.isDirectory()) {
                images.addAll(getAllImages(file));
            } else {
                images.add(file);
            }
        }
        return images;
    }
*/
}
