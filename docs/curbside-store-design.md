
# Modeling the Curbside resource as a store


## Client adding an item to the store

```
 PUT /mycart/13
 Authorization: "jeff"

 {
     "id": 13,
     "name": "Running Shoes",
     "price": 29.99,
     "qty": 1
 }

 PUT /mycart/12
 Authorization: "jeff"

 {
     "id": 18,
     "name": "Socks",
     "price": 9.99,
     "qty": 3
 }
```

## Client decides to Place The Order

### Request 
```
POST /mycart
Authorization: "JEff"
Content-Type: application/json

{
    "creditCardNumber: "999999.99999.9999",
    "etc. etc. etc."
}
```

### Response

400 - soemthing is bad.

```
201 Created
Location: /orders/235

{
    "id": 235,
    "for": "Jeff",
    "items": [
      {
     "id": 13,
     "name": "Running Shoes",
     "price": 29.99,
     "qty": 1
 } 
    ],
    "status": "Delivered"
}
```


