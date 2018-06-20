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
    /*
    Constructor, doesn't do anything but
    needs to be declared to be private since this class is a singleton.
     */
    private ImageTransferer() {

    }
    /*
    Returns an instance of the ImageTransferer class.
    Acts as the constructor for a singleton class.
     */
    public static ImageTransferer GetInstance() {
        if (!Instanced) {
            Self = new ImageTransferer();
            Instanced = true;
        }
        return Self;
    }
    /*
    Transfers the contents of the DCIM folder to the server using TransferImagesInternal.
    This function mostly exists to logically separate the mechanisms built to prevent multiple
    simultaneous file transfers from the actual file transferring.
     */
    public void TransferImages(Context context) {
        //Only one image transfer is allowed at a time
        if (Transferring)
            return;
        Transferring = true;
        TransferImagesInternal(context);
        Transferring = false;
    }
    /*
    Transfers the contents of the DCIM folder to the server using ImageTransfererWEB.
     */
    private void TransferImagesInternal(Context context) {
        //this needs a flag because broadcast receiver might be invoked multiple times because things
        File DCIM = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM + "/Camera");
        if (DCIM == null) return;
        ImageTransfererWEB AsyncFilePasser = new ImageTransfererWEB(context, DCIM);
        //AsyncFilePasser.doInBackground(null);
        AsyncFilePasser.execute(null, null, null);
    }
}