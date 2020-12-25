### What is this repository for? ###
This repo if for Wooliex X Technical Test.

### How do I get set up? ###
This is a ASP.NET core application hosted in AWS Lambda.Take latest and Run the app locally using visual studio.
Make sure the file properties/launchsettings.json has empty profiles like below.
{
  "profiles": {
    
  }
}


### Routes ###
#Local#
* BaseUrl : http://localhost:5002/api
* Product: http://localhost:5002/api/product/sort?sortoption=low
* User: http://localhost:5002/api/User

#AWS#
* Swagger: https://4h2qkzq3kh.execute-api.ap-southeast-2.amazonaws.com/Test/swagger/index.html

* BaseUrl : https://4h2qkzq3kh.execute-api.ap-southeast-2.amazonaws.com/Test/api
* Product: https://4h2qkzq3kh.execute-api.ap-southeast-2.amazonaws.com/Test/api/product/sort?sortoption=low
* User: https://4h2qkzq3kh.execute-api.ap-southeast-2.amazonaws.com/Test/api/User
