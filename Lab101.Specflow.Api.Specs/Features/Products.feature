Feature: GetProducts

A short summary of the feature

@products
Scenario: GetAllProducts
	Given product Details :
		| id | name     | description  | stock |
		| 1  | Product1 | Description1 | 11    |
		| 2  | Product2 | Description2 | 22    |
		| 3  | Product3 | Description3 | 33    |
	When a call is made to '/products'
	Then display list of products :
		| id | name     | description  | stock |
		| 1  | Product1 | Description1 | 11    |
		| 2  | Product2 | Description2 | 22    |
		| 3  | Product3 | Description3 | 33    |