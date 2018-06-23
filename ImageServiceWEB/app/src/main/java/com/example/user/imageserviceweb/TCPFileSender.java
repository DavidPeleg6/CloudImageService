package com.example.user.imageserviceweb;

import android.annotation.TargetApi;
import android.util.Log;

import java.io.*;
import java.net.InetAddress;
import java.net.Socket;
import org.json.JSONObject;
import java.nio.file.Files;
import java.util.Base64;

/*
A class that is used specifically to handle to sending of images to the server.
 */
public class TCPFileSender {
    private Socket m_socket;
    /*
    Connects to a server using the given ip and port.
     */
    public void ConnectToServer(String ip, int port) {
        try {
            InetAddress ServerAddress = InetAddress.getByName(ip); // set IP
            this.m_socket = new Socket(ServerAddress, port); // create m_socket
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    /*
    Sends a file to the server.
    In our app, the file in question in an image.
     */
    @TargetApi(28)
    public void SendImageFile(File ImageFile) {
        try {
            JSONObject ImageFileJSON = new JSONObject();
            ImageFileJSON.put("name", ImageFile.getName());
            byte[] ImageFileBYTES = Files.readAllBytes(ImageFile.toPath());
            String ImageFileBase64 = Base64.getEncoder().encodeToString(ImageFileBYTES);
            ImageFileJSON.put("bytes", ImageFileBase64);
            PrintWriter OutputWriter = new PrintWriter(this.m_socket.getOutputStream(), true);
            OutputWriter.println(ImageFileJSON.toString());
            OutputWriter.flush();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    /*
    Disconnects from the server.
     */
    public void Disconnect() {
        try {
            this.m_socket.close(); // close m_socket
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
