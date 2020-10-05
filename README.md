<!--
*** Credit to https://github.com/othneildrew/Best-README-Template for basic markup template.
-->


<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
<!--
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]
-->



<!-- PROJECT LOGO -->
<br />
<p align="center">
 <!-- 
  <a href="https://github.com/lwsmith35/SimpleWebCrawler">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>
  -->

  <h3 align="center">Simple WebCrawler</h3>

  <p align="center">
	A simple web crawler in C# microservices. 
    <br />
    <a href="https://github.com/lwsmith35/SimpleWebCrawler"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <!-- <a href="https://github.com/lwsmith35/SimpleWebCrawler">View Demo</a> -->
    ·
    <a href="https://github.com/lwsmith35/SimpleWebCrawler/issues">Report Bug</a>
    ·
    <a href="https://github.com/lwsmith35/SimpleWebCrawler/issues">Request Feature</a>
  </p>
</p>


<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Usage](#usage)
* [Roadmap](#roadmap)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)



<!-- ABOUT THE PROJECT -->
## About The Project
A simple web crawler in C# as microservices. 

Requirements:
	Crawl a single domain, do not follow external links.
	Output should present a Site Outline / Map.
	System should be able to be built, tested, and executable.

Time constraint decisions:
	Uses REST services for intra node communication, in liu of event or message.
	Uses in memory DB for simplicity.



### Built With
* [Docker](https://www.docker.com/)
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites
This is an example of how to list things you need to use the software and how to install them.
* Docker environment e.g Docker Desktop ( [MAC](https://docs.docker.com/docker-for-mac/install/} | [Windows](https://docs.docker.com/docker-for-windows/install/))

### Installation
1. Clone the repo
```sh
git clone https://github.com/lwsmith35/SimpleWebCrawler.git
```
2. Docker Compose the application, build and load app services into Docker
```sh
cd <Your Cloned Directory>
docker-compose  -f "docker-compose.yml" -f "docker-compose.override.yml" -p SWC --no-ansi up
```
3. Using Postman import collection: https://github.com/lwsmith35/SimpleWebCrawler/blob/master/Documents/Postman%20API%20Support/SimpleWebCrawler.postman_collection.json


<!-- USAGE EXAMPLES -->
## Usage
Simplest use case requires two REST API calls. These can be found in the Postman collection OR built in any REST client tool.

1. Issue request to crawl site:
```
    POST: http://localhost:3500/api/ProcessUrl
    Body: 
        {
            "url" : "http://SiteToCrawl.com"
        }
```

2. Review pages found and crawled within domain
```
    GET: http://localhost:3501/api/pages?domain=SiteToCrawl.com
```



<!-- ROADMAP -->
## Feature Roadmap / Considerations
- Implement tracking for a domain crawl
- Enhanced transient handling when fetching public pages
    * Retries, SSL Cert handling, Rejected or cut off requests
- Implement a Spam contol layer so app does not DOS target site
- Replace Orchastration layer with Pub/Sub Event layer
- Implement external Logger
- Enhancement for pages that require rendering, e.g. React pages
- Persistant storage solution, GraphDB seems like a good candidate for maping
	- Seperate Data layer (CQRS) Collectors into Command service
	- Seperate Data layer (CQRS) Providers into Query service

See the [open issues](https://github.com/lwsmith35/SimpleWebCrawler/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

This was a project built for entertainment purposes, Any contributions may ... but not likely... be reviewed. In case you find yourself bored: 

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


<!-- LICENSE -->
## License
Distributed under The Unlicense. See `LICENSE` for more information.


<!-- CONTACT -->
## Contact

<!-- Your Name - [@twitter_handle](https://twitter.com/twitter_handle) - email -->

Project Link: [https://github.com/lwsmith35/SimpleWebCrawler](https://github.com/lwsmith35/SimpleWebCrawler)



<!-- ACKNOWLEDGEMENTS -->
<!--
## Acknowledgements
* []()
* []()
* []()
--> 

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links --> 
<!--[contributors-shield]: https://img.shields.io/github/contributors/lwsmith35/repo.svg?style=flat-square 
[contributors-url]: https://github.com/lwsmith35/repo/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/lwsmith35/repo.svg?style=flat-square
[forks-url]: https://github.com/lwsmith35/repo/network/members
[stars-shield]: https://img.shields.io/github/stars/lwsmith35/repo.svg?style=flat-square
[stars-url]: https://github.com/lwsmith35/repo/stargazers
[issues-shield]: https://img.shields.io/github/issues/lwsmith35/repo.svg?style=flat-square
[issues-url]: https://github.com/lwsmith35/repo/issues
[license-shield]: https://img.shields.io/github/license/lwsmith35/repo.svg?style=flat-square
[license-url]: https://github.com/lwsmith35/repo/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/lawrence-w-smith
[product-screenshot]: images/screenshot.png
-->
