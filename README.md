# DB Test project 

## Intro 

This project is a microservice for handling simple CRUD operations in a retail banking setup. The purpose of this solution is to show how a potential replacement to an existing monolith structure could look like. 

For simplicities sake the solution features no database connection, and instead I have worked with a test-driven approach and used mock data to show develop my features via Unit Testing. 

## Features 

The system is a REST API based on the .NET 8 framework, and it exposed the following endpoints 

- **Create account** (POST) creating a new savings account based on a customer ID. If customer does not exist, the endpoints return error 404. 

- **Deposit money to an account** (POST) Checks that the targeted account exists and increases its balance, while also creating an entry in the transactions list. 

- **Withdraw money from an account** (POST) Same as above, but instead it decreases the balance. System allows a savings account to go into the negative. 

- **Read account balance** (GET) Get the current balance for a given account. 

- **Get latest 10 transactions** (GET) Gets a given accounts 10 latest transactions (withdrawals and deposits) ordering them by a timestamp.  

 ## Organization and design of micro service setup: 

This implementation is a simple setup currently and there is still some work needed to have a deployable solution. My design principle was to look at the 5 features and decide where to split it up. For me it made sense to separate Transactions and Accounts into its own individual services. Thats because Transactions regarding banking has a lot of security requirements and properly also a lot of traffic as well as more data. 

Potential FE clients of these 2 services would be created in a separate project, and ideally only communicate to the services via an API gateway. This way we have a unified entry point for our services and can handle any security, load balancing from that place instead of individually. 

 ## Technologies used 

- .NET 8.0 
- NUnit 4 
- Swagger / OpenAPI 
