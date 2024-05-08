# ToDoApi

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