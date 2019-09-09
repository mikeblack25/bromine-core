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

## Features
### Cross Browser Support
Bromine Core provides out of the box support the following drivers.
Additional drivers can be added to your project as needed.

- Chrome
- Edge
- Firefox
- Internet Explorer

### Element
Elements are components that are used to structure a web page and provide a way to interact with it.
It is safe to say that anything you see or do on a website is part of an element.

#### Element Calling Information
After an element has been located using a Browser.Find or Browser.SeleniumFind method,
information about how it was found can be found in the Information property.

![](Documentation\Features\CallingInformation\ElementByClass.JPG)
![](Documentation\Features\CallingInformation\ElementByCss.JPG)
![](Documentation\Features\CallingInformation\ElementById.JPG)
![](Documentation\Features\CallingInformation\ElementByPartialText.JPG)
![](Documentation\Features\CallingInformation\ElementByText.JPG)

#### CSS Format Extensions
The following help build CSS formatted strings.

- ``` "some_class_locator".Class() ``` will build ``` ".some_class_locator" ```
- ``` "some_id_locator".Id() ``` will build ``` "#some_id_locator" ```

#### Element Style
Approaches to styling elements is provided by the ElementStyle class.
The following style options are currently supported.

``` C#
Browser.ElementStyle.AddBorder(LocatorStrategy locatorStrategy, string locator, string color)
Browser.ElementStyle.AddBorder(Element element, string color)
Browser.ElementStyle.AddBorders(LocatorStrategy locatorStrategy, string locator, string color)
Browser.ElementStyle.AddBorders(Element element, string color)
```


### Element Locators
Under the covers Selenium is used to locate elements.

The following methods are supported by the framework.
- Id
- Class
- Css
- Js
- Tag
- Text
- PartialText

**Note:** All location strategies are not applicable in all cases.

This framework provides two main classes to locate elements Find and SeleniumFind.

#### Find
Find makes it easy to locate elements with helper methods that are built to make using CSS Selector syntax easier.

``` C#
Browser.Find.Element(string locator)
Browser.Find.Elements(string locators)
```

These methods will attempt to locate an element or elements by CSS Selector, Id, Class, Text, Partial Text, and Tag.
Any valid string in the DOM should be found by this call.

**NOTE:** Loose matches may find unexpected elements.

``` C#
        /// <summary>
        /// Find element by all supported element location strategies.
        /// </summary>
        /// <param name="locator"></param>
        [InlineData(".gNO89b")] // CSS Selector
        [InlineData("gbqfbb")] // Id
        [InlineData("gNO89b")] // Class
        [InlineData("Gmail")] // Text
        [InlineData("Gmai")] // Partial Text
        [InlineData("input")] // Tag
        [Theory]
        public void FindElement(string locator)
        {
            var element = Browser.Find.Element(locator);

            Browser.Verify.True(element.IsInitialized);
        }
```

``` C#
Browser.Find.ElementByClasses(string classes)
Browser.Find.ElementsByClasses(string classes)
```

``` C#
        /// <summary>
        /// Find element with all the following classes.
        /// gb_Oa gb_Fg gb_g gb_Eg gb_Jg gb_Wf
        /// </summary>
        [Fact]
        public void FindElementByClassesTest()
        {
            var classes = "gb_Oa gb_Fg gb_g gb_Eg gb_Jg gb_Wf";

            Browser.Wait.For.DisplayedElement(Browser.Find.ElementByClasses(classes), 5);

            Browser.Verify.True(Browser.Find.ElementByClasses(classes).Displayed);
        }
```

Child elements can be located with the following methods.

``` C#
Browser.Find.ChildElement(string parentLocator, string childLocator)
Browser.Find.ChildElement(Element parentElement, string childLocator)
Browser.Find.ChildElements(string parentLocator, string childLocator)
Browser.Find.ChildElements(Element parentElement, string childLocator)
```

To find descendent elements the following methods are provided.

``` C#
Browser.Find.ElementByDescendentCss(string classes)
Browser.Find.ElementsByDescendentCss(string classes)
```

``` C#
        /// <summary>
        /// Find element in the DOM by descendent CSS selection. Each element is separated by a space and is a CSS selector.
        /// The first element is the parent.
        /// If additional selectors are added they are expected under the previous element in the DOM structure.
        /// Id -> gbw
        ///   class -> gb_fe
        ///     tag -> div
        ///        attribute -> data-pid=23
        /// </summary>
        [Fact]
        public void FindElementByDescendentCssTest()
        {
            const string gmailString = "Gmail";

            var element = Browser.Find.ElementByDescendentCss("#gbw .gb_fe div [data-pid='23']");

            Browser.Verify.Equal(gmailString, element.Text);
        }
```
