# **Prerequisites**

You should have installed the MS SQL Server.
Run the database_sql.sql script to create the database and add data.
Update the connection string in the IntersectionFinder.API app.

## **Explanations**
I've implemented two methods. The first one uses an algorithm implemented in the SQL function CheckSegmentIntersection. Since the Entity Framework does not generate such queries very well, I have placed the code in an SQL function.

For the second approach, I've used the db type Geometry to store rectangles and find intersections

Please take a look at the GeometricService.

## **Unit tests**
I've added simple unit tests in the IntersectionFinder.UnitTests project.  
You can also use the IntersectionFinder.API to test the API.