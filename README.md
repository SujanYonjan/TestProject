#.Net Core Web Api With JWT Authentication,Store Procedure and ADO.NET for every HTTP GET method,CRUD for Student and Teacher.

Steps to be Followed For Project (url: https://github.com/SujanYonjan/TestProject.git)
1.Clone the Project From the Github to your Desired Location using above url.
2.Open the TestProject.sln file From the Cloned Project.
3.Configure the ConnectionString Only the Server, UID and Password within the appsettings.json File Not the Database .
4.Open Package Manager Console and Enter the Following command in command Line Interface
	i. update-migration Initial
5.Open the StoreProcedureSQL Folder from the Cloned Project and Run the StoreProcedure.Sql file within the Newly Created Database Environment (i.e DatabaseName: TestProject).
6.Run the Project and It will Automatically Seed the User If not Exist.

Steps to be Followed For PostMan Api (url: https://www.postman.com/crimson-eclipse-858003/workspace/testproject/globals)
1.Open the Link for the Postman using above url.
2.Export Collection and Import the Collection json File In Postman's Desktop Agent.
3.Setup the Global Environment Variable as base_URL, Type as default and Initial Value and Current value with your Localhost Url (eg: https://localhost:44377/api).
4.Open Authenticate Collection and use the following Seeded credential to Login and It'll Provide you with the jwt Token.
	-UserName: User01
	-Password: Password@123
							OR
Create a New User Using Register Api From the Authenticate Collection.
5.open the Collection For Student and Follow the Below steps
	i. Select Authorization
	ii. Choose Authentication Type to Bearer Token
	iii. Copy the Jwt Token Provided while Login and Enter the Token in the Token Field.
6.Same Steps as 2 For Teacher Colletion.
7.Open One of the Api and Send the Request.

