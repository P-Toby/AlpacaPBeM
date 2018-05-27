# AlpacaPBeM for Dominions 5

Alpaca is a small PBeM manager written in C# for Dominions 5 for Windows. It is very early and it is only capable of receiving a turn based on the name of the game and sending turns to a server.
There is no UI at the moment and the user has to fill out the configuration file manually before starting the program. There will hopefully be a UI further down the line.
I'm primarily a C/C++ programmer and this is my first C# program so it is bound to have some spaghetti so any feedback and suggestions are welcome.


The fields in the config file are pretty straight forward.

Email: Your email

Password: Your password (surprise)

UsrEmailServer: Server address for the email provider that you use

UsrEmailServerPort: Server port for the email provider that you use

GameExecutable: Not used yet

Savedgames: Path to your Dominions 5 savedgames folder, usually found in C:\Users\[YourUserName]\AppData\Roaming\Dominions5\savedgames

TurnEmail: The email you wish to send your turns to, should most likely be turns[at]llamaserver.net


Please note that there are probably (most certainly) bugs.
