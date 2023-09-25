﻿# UpsChallengeCode

 
To start the project, we should include an API key for our HTTP API request. Dotnet Secret is an easy and safe method to save and control secrets in our application. After adding the API key, we can obtain the necessary data and proceed with the project.

for installation
````
dotnet tool install -g dotnet-user-secrets

````

to set api key for use
`````
dotnet user-secrets set "UpsApiService:ApiKey" "Api-key-in-document"
``````

I'm working on a project that involves three distinct sections: Ups.App for the UI, Ups.Application for the business logic, and Ups.Common for utilities and helpers. Maintaining separation between these sections is crucial for an organized and efficient workflow. 



# Reseliancy

this app is built to withstand unexpected server issues and remain reliable. I've implemented a retry policy using Polly to ensure our users have constant access to the resources they need. If there's ever a disruption in our server, our app will automatically try again until it successfully connects, minimizing downtime for our users. You can always count on our app to be there when you need it.


#MediatR

My codebase now incorporates the CQRS pattern, which has significantly improved clarity and organization. This approach enables me to differentiate between various components and functionalities, leading to a more streamlined and effective codebase.



#FluentValidation

I have integrated FluentValidation into My code to separate validation logic from business logic. This not only enhances the readability and maintainability of our code but also enables us to handle validation errors more efficiently and offer improved user feedback. In general, it has been a valuable addition to My development process.









