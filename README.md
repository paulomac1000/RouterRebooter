# RouterRebooter

Windows service which restart OpenWRT routers using telnet when unable to connect via SSH.

## Installation

- Make sure that you have NET Framework installed
- Create folder `RouterRebooter` on disk
- Download release package and paste it into this folder
- Open App.config file and specify router IP, router login and password and checking interval
- Run `install.bat` file to install service