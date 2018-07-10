# Image Service
in this project we implemented a windows service that tracks folders and whenever a new image item (limited to jpg, bmp, gif) arrives to one of the folders,
the service will create a backup image in a predefined folder (defined in the app settings).
We then created three platforms for using this service:

1. WPF: a GUI window with two tabs:
  * Logs- whenever the service has issued a new system log (for example, in case of a new image) it would also be updated
  * Config- shows a list of all the Info the service is using to run (for example, the output directory and active handlers). also has the ability to create a request to stop tracking a folder from the given list.

2. WEB: a web application with four tabs:
  * Home- contains the name and basic info (not hard coded, recieves data from server)
  * Logs- a web version of the WPF logs tab
  * Config- another web version of a WPF tab
  * View- an interface through which you can view the images in the output directory as well as erase them
  
3. Android: a basic android application with one main use- every time the phone connects to WIFI the app will begin backing all of its photos up to the given windows service
