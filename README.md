# ADHD-Project-Client
 
This project helps refocus users who get distracted while completing their work. It does this by tracking the user's attentiveness using a combination of computer usage (is the user interacting with the KB + Mouse), gaze (where the user is looking), and position (has the user left the workspace). If the user is not paying attention according to any of these metrics, a sound will be played from the Hololens 2 telling them to focus. These metrics are all visible to the user in realtime with a UI canvas that displays them in the virtual environment.


# How to use it

This is the client portion of the software. In order to run it, the server software must be launched and running on the computer the user will work on. Afterwards, the software should be launched on a hololens, at which point on the hololens a screen will ask to enter the IP of the server using the virtual keyboard. The user should enter the IP and press enter, at which point the virtual reality metrics board should pop up.



# Troubleshooting

There may be an issue with the client failing to connect to the server when the correct server IP is entered. In this case, it may be due to Windows Firewall rules preventing the TCP connection between the client and server. You can create a rule to allow the connection on port 32401.


# Plugins used:
