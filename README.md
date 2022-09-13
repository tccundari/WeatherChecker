# Description
A webCrawler that get wheather information of Australian cities from the Australian Government WebSite.

This project was made as an example to show how to build a simple webcrawler, to shows how to struct data on the solution and how pro provide a service with the information that you get.

*This project was made only to share code skills and teach about programming bots, there is no lucrative porpose on this project and can not be used in any business interests.

## Tecnologies
- Build in Visual Studio 2019
- .NET Core 5 (C#)
- Angular 6

* [X] Create solution and base crawler
* [X] Add a API layer provide the information as services
* [X] Add a Web Interface and consume our APIs
* [ ] Add a Unit Test Layer
* [ ] Improve interface resources
* [ ] Add Data Exportation
* [ ] Generate Dashborads and Reports

### Instructions
The API service was configured to run in your localhost whitout the need to be published in some virtual directory, just build the API project in the solution then go to compiled files and run the .exe to start up the API local server.
```
...WeatherChecker\WeatherChecker.API\bin\Debug\net5.0\WeatherChecker.API.exe
```
After this you can change the base URL at the file main.ts
```
export function getBaseUrl() {
  //return document.getElementsByTagName('base')[0].href;
  return "http://localhost:5000/";
}
```
