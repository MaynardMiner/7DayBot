# 7DayBot

This is a simple discord bot to restart my private server via discord. It will also do daily restarts at 4am and 4pm local time.

Anyone is welcome to use.


Json Config

```
{
  "Token": "",
  "Prefix": "!",
  "Application Name": "7DaysToDieServer",
  "Tap Enter To Close" : true,
  "Ip Address": "127.0.0.1",
  "Port": "26900",
  "Application Shortcut Path": "C:\\Program Files (x86)\\Steam\\steamapps\\common\\7 Days to Die Dedicated Server\\start.lnk"
}
```

Token: your bot token

Prefix: Command Prefix

Application Name: Name of the server process

Tap Enter To Close: 7dtd windows server requires enter pressed to close.

IP Address: Ip Address of the server for checking connection

Port: Port of the server for checking connection

Application Shortcut Path: Shortcut to restart the server (Suggest using the shortcut so that it runs in new environment)

## Features

- Will notate if server is online

- `restart` command to restart the server

## Limitations

Currently only works for Windows due to having System.Windows.Forms feature to close application windows. Removing that should make it work for linux.
