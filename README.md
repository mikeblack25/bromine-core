# Bromine Core
The goal of Bromine Core is to provide an easy way to automate, scrape, and / or test websites.

Bromine Core is a wrapper around [Selenium WebDriver](https://www.seleniumhq.org/projects/webdriver/).
Selenium WebDriver is an open source project which provides the ability to interact with web elements.
Specific drivers are developed for the Selenium IDriver interface to allow Selenium to control supported browsers.

Selenium specific documentation can be found [here](https://www.seleniumhq.org/projects/webdriver/).

## Uses
- Scraping information from websites.
- Automating repetitive website flows.
- Testing website behavior.

## Setup

This project was developed using Visual Studio. Visual Studio can be downloaded [here](https://visualstudio.microsoft.com/downloads/).

To download Bromine Core navigate to https://github.com/mikeblack25/bromine-core/releases.

Expand the Assets list for the selected release.

![](Documentation\General\Download%20Bromine%20Core.JPG)

### Use Cases
Bromine Core can be integrated and extended depending on how you and your project want to use it.

#### Clone Repo / Fork / Submodule
Cloning the repo will allow you to easily stay up to date with the latest changes on the project.

Bromine Core can also be added as a submodule to provide an easy way to stay up to date.
If you plan to extend Bromine Core for your own use it is probably a better idea to Clone or Fork.

**NOTE**: If you would like to contribute to the project you will need to Clone the repo.

![](Documentation\General\Clone%20Bromine%20Core.JPG)

#### Add Manually

All artifacts have been provided but may not be required for your use.
If you plan to manage Selenium WebDriver dependencies yourself or with a package manager like NuGet Bromine.dll is all you need.
The other artifacts have been provided and should work out of the box provided your desired web browsers are in sync with the provided drivers.

Add a reference in your solution to Bromine.dll and any other provided artifacts you need.

- Right click on References in your project and select Add Reference...
![](Documentation\General\Add%20Reference.JPG)

- Click the Browse... button and select and add Bromine.dll.
![](Documentation\General\Browse%20Reference.JPG)

I suggest adding your desired browser drivers using Nuget.
- Right click on your project or solution and select Manage NuGet Packages...
![](Documentation\General\Manage%20NuGet.JPG)

The desired WebDriver can now be added.
- Selenium.WebDriver.ChromeDriver for the **Chrome** browser.
- Selenium.Firefox.WebDriver for the **Firefox** browser.
- Selenium.WebDriver.MicrosoftDriver for the **Microsoft Edge** browser.

**NOTE:** I would suggest avoiding using the Internet Explorer browser as it is difficult to use and setup the driver.

### Drivers
Bromine Core provides the following drivers.
Additional drivers can be added to your project as needed.

- Chrome
- Edge
- Firefox
