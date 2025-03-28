# Movie website
This code was made as a part of Wexo's code challenge. I choose to make it in MVC that I've worked with once before. 

## How to open the file
Open the code in Visual Studio and run it in https. This should open the website.

# Screenshot of website frontpage


# Documentation
Architecture
MVC - Controller, View, Model, Service layer

API connection
Connection through appsettings.json so it can be used by more than one servicelayer. It makes it easier to vedligeholde so if the api connection ever needs to be changed, I just have to change it one place instead of multiple service-layers.

## Http connection
To reuse the http client. Earlier made the mistake of using "using http" but that doesn't close it up the correct way.
To prevent socket exhaustion. First time using it. Know you can use a factory pattern instead also.

## Interfaces
To make low coupling between the layers. So you can change something from the servicelayer without affection the other code.

## Dependency Injection
TO make objects. Gives low coupling.

## Response-classes in the model layer
Alternative is to handle the json in the servicelayer.

## ModelView in the model layer

## Things that could be changed
- I could have added a BusinessLogicLayer between the Controller and ServiceLayer. That would be better for separations of concerns.
