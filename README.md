# WebAPIWithUnitTest
Restful API MVC to manage Artists and Videos.
Includes a Web API MVC project with Validation, Controllers, ADO interface, Unit Test

API Coding Exercise
-------------------
This ZIP file has two items

1)VevoTest Folder
It holds the WEB API Application

2)VevoDB_FullScript.sql
This script will create the data base objects (Schema and data).

INSTALLATION
------------

Create DB
---------
The steps to create the db are:

1)Login TO SQLEXPRESS
1)Create the Vevo database
2)Run the VevoDB_FullScript.sql in this DB.

Change DB Connection
--------------------
Open the app and change the database connection in the Web.config:
1) Go to line 13 in the file Web.config
2) Replace the following values with the values that describe your environment:
    server=LPT6W74YZ1\SQLEXPRESS;
    database=Vevo;
    uid=sa;
    password=DevPassword!


APIs ENDPOINTS SAMPLES
----------------------

Artists
-------
GET

Get all Artists

http://localhost:1111/api/Artists

GET One

Will retrieve Artist and Video info (if present) as shown below

http://localhost:1111/api/Artists/2


{
  
"Videos": [
    
    {
      
    "video_id": 1,
      
    "artist_id": 1,
      
    "isrc": "USSM21200785",
      
    "urlSafeTitle": "windows-down",
      
    "VideoTitle": "Windows Down"
    
    }
  
],
   
"ArtistId": 1,
  
"SafeName": "big-time-rush",
  
"ArtistName": "Big Time Rush"

}

POST

"artist_id": 0 will trigger an add

http://localhost:1111/api/Artists/


{
    "artist_id": 0,
    "urlSafeName": "leona",
    "name": "Leaona"
}

PUT

a non zero "artist_id"  will trigger an update

http://localhost:1111/api/Artists/


{
    "artist_id": 23,
    "urlSafeName": "leona",
    "name": "Leona"
}

DELETE

Will delete the Artist and Video Information if present

http://localhost:1111/api/Artist/1


Videos
------

GET

Get all Videos

http://localhost:1111/api/Videos/

GET One

http://localhost:1111/api/Videos/1

POST

"video_id": 0 will trigger an add

http://localhost:1111/api/Videos/


{
    "video_id": 0,
    "artist_id": 23,
    "isrc": "USRV222222222",
    "urlSafeTitle": "23plus",
    "VideoTitle": "23 Plus"
}

PUT

a non zero "video_id"  will trigger an update

http://localhost:1111/api/Videos/


{
    "video_id": 1013,
    "artist_id": 23,
    "isrc": "USRV222222222",
    "urlSafeTitle": "24plus",
    "VideoTitle": "24 Plus"
}

DELETE

http://localhost:1111/api/Videos/21


THIRD PARTY PACKAGES INCLUDED
-----------------------------

Ninject (and dependencies),
WebApiContrib.IoC.Ninject,
Moq


FURTHER DEVELOPMENT
-------------------

1)Ability to use natural or primary key.
2)Basic unit tests are included, but further unit and integration test should be developed.
3)Additional Validation rules.
4)Code optimization EG: Develop code to merge similar controller's logic in a single class.
5)Further simplification of the Data Access functions.



