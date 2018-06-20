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
/*
This class is used by ImageTransferer in order to perform the file transfer in a separate thread.
 */
public class ImageTransfererWEB extends AsyncTask {
    private ProgressBar ImageTransferProgressBar;
    private File dcim;
    private Context context;
    /*
    Constructor, takes the context the code in running in (MainActivity) and the File path to the image folder.
     */
    public ImageTransfererWEB(Context context, File dcimm) {
        this.context = context;
        this.dcim = dcimm;
    }
    /*
    The code that is run in the background when this class is executed.
    Transfers images from the DCIM folder to the server one by one
    while displaying the progress via a progress bar notification.
     */
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
            //InetAddress serverIPAddress = InetAddress.getByName("10.0.2.2"); TODO: return to this
            InetAddress serverIPAddress = InetAddress.getByName("5.29.216.162");
            // create a ServerSocket to make the connection with the server
            Socket ServerSocket = new Socket(serverIPAddress, 8000);
            OutputStream Output = ServerSocket.getOutputStream();
            for (File Picture : Pics) {
                //create an input stream of the Picture
                FileInputStream ImageFileInputStream;
                try { ImageFileInputStream = new FileInputStream(Picture); } catch (FileNotFoundException e) { break; }
                //convert image into byte array
                Bitmap bm = BitmapFactory.decodeStream(ImageFileInputStream);
                byte[] ImageAsByteStream = GetBytesFromBitmap(bm);
                //send to server
                Output.write(ImageAsByteStream.length);
                Output.write(ImageAsByteStream);
                Output.flush();
                //TODO: if shit is down add this
                //Thread.sleep(100);
                p.ProgressUpdate(++progress);
            }
        } catch(Exception e) { Toast.makeText(context, e.getMessage(), Toast.LENGTH_SHORT).show();//TODO delete this
        return null; }
        p.PostExecute();
        return null;
    }
    /*
    Converts a bitmap to a byte array for transfer.
     */
    public byte[] GetBytesFromBitmap(Bitmap bitmap) {
        ByteArrayOutputStream ImageStream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG, 70, ImageStream);
        return ImageStream.toByteArray();
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
