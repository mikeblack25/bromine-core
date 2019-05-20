# Appium

Appium is a server that enables automation of mobile devices (Android and iOS).

Information about Appium can be found https://appium.io/.

## Installation
There are a few installation options available for Appium.

### Appium desktop client   
  This option provides an easy installation and a user interface with basic configuration options.
  * The desktop client can be downloaded from  https://appium.io/.     
### Appium command line   
  Follow the following steps to install Appium using Node.
  * Download and install Node.js from https://nodejs.org/en/.
  * Launch the Node.js command prompt. This can be done from the start menu on Windows.
  * Appium can be installed via the following command: ```$ npm install -g appium ```


## Appium Command Line
Appium command line provides more advanced configuration options.
  * Start Appium with default settings: ```$ appium ```
  * Start Appium with default settings: ```$ appium -p <port to start the server on>```
  * Start Appium with a specific port and specific settings: ```$ appium -p <port to start the server on> --nodeconfig <absolute path to a configuration.json> ```

## Configuration
A configuration file can be configured and provided as a parameter when starting Appium with ``` --nodeconfig <absolute path to a configuration.json> ```.

The following will explain what the configuration options mean.

### Capabilities
Provide information about what devices are supported.

#### ``` deviceName ```
Unique identifier for the type of device supported. For example the phone name "iPhone X".

#### ``` browserName ```
The following browsers are supported.
* ``` browserName: "chrome" ```
* ``` browserName: "firefox" ```
* ``` browserName: "safari" ```
* ``` browserName: "internet explorer" ```
* ``` browserName: "MicrosoftEdge" ```

#### ``` version ```
Useful to provide information about the given device.
The following are examples, anything can be put here.
* ``` "version": "10" ```
* ``` "version": "8.1" ```
* ``` "version": "Mojave" ```

#### ``` maxInstances ```
The number of browsers that can be ran at the same time for the given device.

Note:
* The Internet Explorer driver does not reliably support more than one max instance.

#### ``` platform ```
Useful to provide information about the given device.
The following are examples, anything can be put here.
* ``` "platform": "Windows" ```
* ``` "platform": "Android" ```
* ``` "platform": "iOS" ```
* ``` "platform": "Mac" ```

### Configuration
The following properties will need to be updated for this configuration to work. The other properties can be updated or the defaults provided can be used.

#### ``` "<Appium IP address>" ```
The following configuration items must be updated for the configuration to work.
* ``` "url": "http://192.168.1.100:4723/wd/hub" ```
* ``` "host": "192.168.1.100" ```

#### ``` <Appium port> ```
The default Appium port is 4723. If the value is changed the following properties need to be updated.
* ``` "url": "http://192.168.1.100:4723/wd/hub" ```
* ``` "port": 4723 ```

#### ``` "maxSession" ```
This determines the maximum currency across all supported devices.

#### ``` <Selenium Grid port> ```
Port where the Selenium Grid is hosted. The default Selenium Grid port is 4444.
* ``` "hubPort": 4723 ```

#### ``` "<Selenium Grid IP address>" ```
IP Address where the Selenium Grid is hosted.
* ``` "hubHost": "192.168.1.100" ```

## Configuration Example
The following can be copied and modified for intended use. Save the file as a *.json extension and update as appropriate and per suggestions above.

```
{
  "capabilities": [
    {
      "deviceName": "<Name of device>",
      "browserName": "<browser>",
      "version": "<Device OS version>",
      "maxInstances": 2,
      "platform": "<Device platform>"
    }
  ],
  "configuration": {
    "cleanUpCycle": 3000,
    "timeout": 30000,
    "proxy": "org.openqa.grid.selenium.proxy.DefaultRemoteProxy",
    "url": "http://<Appium IP address>:<Appium port>/wd/hub",
    "host": "<Appium IP address>",
    "port": <Appium port>,
    "maxSession": 2,
    "register": true,
    "registerCycle": 5000,
    "hubPort": <Selenium Grid port>,
    "hubHost": "<Selenium Grid IP address>"
  }
}

```

[Example Configuration File](./appium.json)
