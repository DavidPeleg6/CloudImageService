package com.example.user.imageserviceweb;

import android.content.Context;
import android.os.Environment;
import java.io.File;

/*
The class that handles actually transferring the images.
Is a singleton to prevent multiple file transfers from happening at once.
 */
public class ImageTransferer {
    static boolean Instanced = false;
    static ImageTransferer Self;

    boolean Transferring = false;

    private ImageTransferer() {

    }

    public static ImageTransferer GetInstance() {
        if (!Instanced) {
            Self = new ImageTransferer();
            Instanced = true;
        }
        return Self;
    }

    public void TransferImages(Context context) {
        //Only one image transfer is allowed at a time
        if (Transferring)
            return;
        Transferring = true;
        TransferImagesInternal(context);
        Transferring = false;
    }

    private void TransferImagesInternal(Context context) {
        //this needs a flag because broadcast receiver might be invoked multiple times because things
        File dcim = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM + "/Camera");
        if (dcim == null) return;
        ImageTransfererWEB AsyncFilePasser = new ImageTransfererWEB(context, dcim);
        //AsyncFilePasser.doInBackground(null);
        AsyncFilePasser.execute(null, null, null);
    }
}