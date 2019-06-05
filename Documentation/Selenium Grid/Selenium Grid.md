# Selenium Grid
The Selenium Grid provides a way automate browsers across a distributed remote network.

## Installation

### Selenium Standalone Server
This is the Java application that runs the Selenium Grid.

* Downloaded is available from the Selenium Standalone Serve section of https://docs.seleniumhq.org/download/.
* It can also be installed using package managers such as NPM https://www.npmjs.com/package/selenium-server-standalone-jar.

### Client Drivers
Platform specific browser drivers can be downloaded https://www.seleniumhq.org/download/, or by using a package manager.
The supported browsers are configurable by device registered to the Grid.

## Hub
The Hub is the software that organizes and manages the remote devices (Node) registered on the Selenium Grid.

A Hub can be started with the following command ``` java -jar selenium-server-standalone-<Server Version>.jar -role hub ```
* Note: The ``` <Server Version> ``` must match the version on the downloaded jar file. Or the file can be renamed without the version and this is not needed.

## Node
A Node provides the ability to automate on one or more browsers for a given computer or device.

A Node can register its self to a Hub. The Hub will then be able to distribute automation tasks if the Hub supports the requested device configurations.

``` java -Dwebdriver.ie.driver=IEDriverServer.exe -Dwebdriver.chrome.driver=chromedriver.exe -Dwebdriver.gecko.driver=geckodriver.exe -jar selenium-server-standalone-3.141.59.jar -role node -nodeConfig <Node Config File Name>.json ```

### Node Configuration
The following is an example of registering a node on the same machine as the Hub.

Note: To register the Node with a Hub on a different machine, update ``` "hub": "http://<Selenium Grid IP Address>:<Selenium Grid Port>" ```.
```
{
  "capabilities":
  [
    {
      "browserName": "internet explorer",
      "maxInstances": 1,
      "seleniumProtocol": "WebDriver"
    },
    {
      "browserName": "chrome",
      "maxInstances": 6,
      "seleniumProtocol": "WebDriver"
    },
    {
      "browserName": "firefox",
      "maxInstances": 6,
      "seleniumProtocol": "WebDriver"
    }
  ],
  "proxy": "org.openqa.grid.selenium.proxy.DefaultRemoteProxy",
  "maxSession": 8,
  "port": 5555,
  "register": true,
  "registerCycle": 5000,
  "hub": "http://localhost:4444",
  "nodeStatusCheckTimeout": 5000,
  "nodePolling": 5000,
  "role": "node",
  "unregisterIfStillDownAfter": 60000,
  "downPollingLimit": 2,
  "debug": false,
  "servlets" : [],
  "withoutServlets": [],
  "custom": {}
}

```

[Example Configuration File](./grid.json)

