# ToDoApi

## About
This is a CRUD application for creating, updating, deleting and getting todo items.
* .net8
* mongodb - to manage the todo items
* swagger - to document the apis

## Tech Debt / TODO
* Logging - there is no logging included in the project currently
* Error handling - again there's no error handling done currently
* POST returning wrong status code - this should return 201 but currently returns 200
* PUT and POST request body contains Id - Arguably the body for the PUT and POST should not allow Id to passed through in the body (PUT would still require this in the uri). Mongodb would create the Id for you and it allows for the customer to pass through bad values. Maybe can be acheived with a second model object.

## Decisions made
* Async method calls - All calls are async which is potentially overkill for the given scenario but I decided to go with it
* Xunit and Moq testing frameworks - these were chosen to match the AJ Bell tech stack
* PUT return - the PUT controller method returns the relevant todo item. This is not strictly REST as it should return no content forcing the user to do a GET. 

## Running locally
### 1. Run MongoDB 

The ToDoApi currently runs against a locally running mongo db instance.
To get this working you will need to install mongodb and the mongodb shell `mongosh`. 
You start up your mongodb:
```
mongod --dbpath /usr/local/var/mongodb/
```
Where `/usr/local/var/mongodb/` is your path to mongodb

To put some data in your db you can run the following
### 2. Populate your database with some data
`mongosh` opens the mongodb shell 

`use ToDoItems` switches to the ToDoItems database (and creates it if it doesn't exist)

`db.createCollection('ToDoItems')`  Creates the named collection

`db.ToDoItems.insertMany([{ "name": "Make dinner" }, { "name": "Pick up the kids from school" }])` Insert some into the created collection

`db.ToDoItems.find().pretty()` View the data in the collection

### 3. Run the ToDoApi project
You can now run the project and you will see the swagger documentation open in your browser
```angular2html
http://localhost:5236/swagger/index.html
```