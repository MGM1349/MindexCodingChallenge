# Mindex Coding Challenge
## What's Provided
A simple [.NetCore 2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1) web application has been created and bootstrapped 
with data. The application contains information about all employees at a company. On application start-up, an in-memory 
database is bootstrapped with a serialized snapshot of the database. While the application runs, the data may be
accessed and mutated in the database without impacting the snapshot.

### How to Run
You can run this by executing `dotnet run` on the command line or in [Visual Studio Community Edition](https://www.visualstudio.com/downloads/).


### How to Use
The following endpoints are available to use:
```

* CREATE
    * HTTP Method: POST 
    * URL: localhost:8080/api/employee
    * PAYLOAD: Employee
    * RESPONSE: Employee
* READ
    * HTTP Method: GET 
    * URL: localhost:8080/api/employee/{id}
    * RESPONSE: Employee
* UPDATE
    * HTTP Method: PUT 
    * URL: localhost:8080/api/employee/{id}
    * PAYLOAD: Employee
    * RESPONSE: Employee
```
The Employee has a JSON schema of:
```json
{
  "type":"Employee",
  "properties": {
    "employeeId": {
      "type": "string"
    },
    "firstName": {
      "type": "string"
    },
    "lastName": {
          "type": "string"
    },
    "position": {
          "type": "string"
    },
    "department": {
          "type": "string"
    },
    "directReports": {
      "type": "array",
      "items" : "string"
    }
  }
}
```
For all endpoints that require an "id" in the URL, this is the "employeeId" field.

## What to Implement
Clone or download the repository, do not fork it.

### Task 1
Create a new type, ReportingStructure, that has two properties: employee and numberOfReports.

For the field "numberOfReports", this should equal the total number of reports under a given employee. The number of 
reports is determined to be the number of directReports for an employee and all of their direct reports. For example, 
given the following employee structure:
```
                    John Lennon
                /               \
         Paul McCartney         Ringo Starr
                               /        \
                          Pete Best     George Harrison
```
The numberOfReports for employee John Lennon (employeeId: 16a596ae-edd3-4847-99fe-c4518e82c86f) would be equal to 4. 

This new type should have a new REST endpoint created for it. This new endpoint should accept an employeeId and return 
the fully filled out ReportingStructure for the specified employeeId. The values should be computed on the fly and will 
not be persisted.

### Task 2
Create a new type, Compensation. A Compensation has the following fields: employee, salary, and effectiveDate. Create 
two new Compensation REST endpoints. One to create and one to read by employeeId. These should persist and query the 
Compensation from the persistence layer.

## Delivery
Please upload your results to a publicly accessible Git repo. Free ones are provided by Github and Bitbucket.


## Marc Molnar Changes
Added 4 new endpoints

HTTP Method: GET
reporting/{id}

  Returns the reporting structure of a specific employee. This will display all the direct reports and also the total number of reports

HTTP Method: POST
compensation

  Create a compensation by adding in an employee, their salary, their start date, and an id. These will be added to a compensation database.

HTTP Method: GET
compensation/{id}

  Returns a specific compensation based on the id recieved. This will display all the information on the employee then return their compensation information.

HTTP Method: GET
compensation/create/{id}

  This is a test endpoint as I wasn't able to finish any test methods in EmployeeControllerTests.cs. I started creating one but ran into some issues and I need to submit this coding challenge to move forward with my class work. 

  This endpoint will create a compensation based on an id of an employee within the employee database. Then this endpoint will go find the compensation within the database before displaying the information. 


http://localhost:8080/api/employee/16a596ae-edd3-4847-99fe-c4518e82c86f
http://localhost:8080/api/employee/reporting/16a596ae-edd3-4847-99fe-c4518e82c86f

## How to use updated code

http://localhost:8080/api/employee/reporting/16a596ae-edd3-4847-99fe-c4518e82c86f

  This link will return the reporting structure for John Lennon. It will display all the employees underneath him and also display the number of reports

http://localhost:8080/api/employee/compensation/create/16a596ae-edd3-4847-99fe-c4518e82c86f
  
  This link will create a hard coded compensation (except employee, that is based on the id inputted) object for John Lennon. 

  This data does persist and can be checked using:
  http://localhost:8080/api/employee/compensation/16a596ae-edd3-4847-99fe-c4518e82c86f

  Sometimes this page will display an error on first run. I did not have a chance to figure out why however a simple refresh seemed to fix that issue for me.

## Additional Comments

http://localhost:8080/api/employee/compensation

  Since my test methods were running into issues loading the compensation database I did not get to test this endpoint. At this time I do not know if it will work but all the code within this enpoint is also within the test compensation endpoint. 