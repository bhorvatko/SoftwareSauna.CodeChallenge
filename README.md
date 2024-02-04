# Software Sauna Code Challenge Solution

The solution is designed to solve maps in three steps:
 - Transform the string input into a matrix map
 - Transform the matrix map into a vector map
 - Transform the vector map into a linear map

### Matrix maps
Matrix maps are a 2 dimensional representation of the problem. When a matrix map is created, the map forms relationships between the fields; each field is aware of its' adjacent fields (up, right, down and left).

### Vector maps
Matrix maps are transformed into vector maps, which represent the problem in the form of a sequence of vectors, defined by their origin field, orientation and length. First, the origin of the vector is defined (for the first vector this is going to be the start field '@'). Then, the orientation is determined by observing the adjacent fields as potential paths. The length and fields of the vector are determined by traversing the field in the defined orientation until a vector terminator field is reached (which can be either the intersection '+', letter or end 'x' fields). The process is then repeated, using the previous vector's end field as the next vector's origin, until the end 'x 'field is reached.

### Linear maps
Vector maps are transformed into linear maps, which represent the problem in the form of a linear sequence of fields. This is done by simply concatenating each vector's fields. At this point, the matter of getting the path as letters and collecting the map letters is trivial.

## Automated tests
The test suite is split into

 - Integration tests - these cover the assignment specification and acceptance criteria
 - Unit tests - these cover units of behaviour of the solution
	
## WebAPI
The project includes an endpoint for resolving maps. Note that requests should be made using `\n` or `\r\n` as line breaks, e.g. `"mapString": "  @---A---+\n          |\n  x-B-+   C\n      |   |\n      +---+\n"`

OpenAPI documentation is available @ `/swagger`.