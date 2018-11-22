# .NET Core Microservice UI Composition
![Summary](https://raw.githubusercontent.com/selcukusta/net-core-microservice-ui-composition/master/summary.png)
## To create a new product;
```bash
curl -X POST \
  http://localhost:5003/create \
  -H 'Content-Type: application/json' \
  -H 'cache-control: no-cache' \
  -d '{
  "Product": {
    "Id": 8,
    "Price": 15999.99,
    "ProductName": "Lenovo Thinkpad Intel Core i5 16 GB RAM",
    "CategoryId": 1
  },
  "Star": {
    "Id": 8,
    "ProductId": 8,
    "StarCount": 4
  }
}'
```