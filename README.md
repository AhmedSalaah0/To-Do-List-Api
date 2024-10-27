# To-Do-List-Api
To-Do List Api Using Asp.Net Core Api and Asp.Net Identity

## API Reference

#### For login and registration, take a look at ASP.NET Identity.
https://learn.microsoft.com/en-us/previous-versions/aspnet/mt173608(v=vs.108)

#### Get all items
```!!! Don't forget To Set your Db Connection string In appsettings.json file```

```http
  GET /api/tasks
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `access Token` | `OAuth` | **Required**. user OAuth |

#### Get ToDoList

```http
  GET /api/gettodolist/${id}
```

| Parameter     | Type      | Description                       |
| :--------     | :-------  | :-------------------------------- |
| `id`          | `int`     | **Required**. Id of item to fetch |
| `access Token`| `OAuth`   | **Required**                      |


===============================


#### Add A New Group


``` post
  POST /api/tasks
```

| Parameter     | Type      | Description                       |
| :--------     | :-------  | :-------------------------------- |
| `Title`       | `string`  | Group Title                       |
| `Description` | `string`  | Tasks Group Description           |
| `access Token`| `OAuth`   | **Required**                      |


#### Add New Item To the Group
```post
  POST /api/addtodolist
```
| Parameter     | Type      | Description                       |
| :--------     | :-------  | :-------------------------------- |
| `taskId`      | `int`     | Group Id                          |
| `Content`     | `string`  | To-Do Content                     |
| `access Token`| `OAuth`   | **Required**                      |




===============================



#### set Complete or in Complete

**For All Group**
```put
  PUT /api/tasks/{taskId}
```

| Parameter     | Type      | Description                       |
| :--------     | :-------  | :-------------------------------- |
| `taskId`      | `int`     | Group Id                          |
| `access Token`| `OAuth`   | **Required**                      |


**For A ToDo List Element**

```put
  PUT /api/Completed/{ToDoId}
```

| Parameter     | Type      | Description                       |
| :--------     | :-------  | :-------------------------------- |
| `ToDoId`      | `int`     | Element Id                        |
| `access Token`| `OAuth`   | **Required**                      |

!!Note: Each Request Will Reverse The Current State

===============================
