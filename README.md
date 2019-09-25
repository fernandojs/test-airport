Project Test -  Distance between airports
=====================
This project has tools, good practices and patterns to increase the quality of your architecture, code, resilience and integrity.

## Frameworks / Tools:

- .Net Core 2.2 / ASP.Net Core 2.2
  - API
  - Application (Business Layer)
  - Rest 
  - Tests
- .Net Standard
  - Domain
- Docker  
  - API (Docker support - Linux)
- Polly  
  - Increase the confiability to get data from 3rd party API
- SeriLog
  - Logging the 3rd API calls fails
- XUnit
  - Runners
  - Collection Fixture
  - Good use of decorators for better recognizing
- MOQ
  - Use of Verify Assertion

## How to test:
  - Run the API project and call like the example below:
    - /api/airport/AMS/CWB

## Future:
  - Redis
    - Redis in the Business Logic and Rest Service to cache information.
  - System.Runtime.Caching/MemoryCache  
    - Use in the API to cache requests
   

## About:
This project was developed by [Fernando Jose Santa Rosa](https://fernandojs.com) under the [MIT license](LICENSE).
