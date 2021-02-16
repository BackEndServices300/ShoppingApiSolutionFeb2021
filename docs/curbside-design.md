# Curbside Ordering

In order to keep people safe, we are going to allow them to create curbside orders for later pickup.

## POST /curbsideorders


### Request
```
POST /curbsideorders
Content-Type: application/json
Authorization: bearer xxxxxxx

{
    "pickupPerson": "Bob Smith",
    "items": "1,2,3,4,5"
}

```

### Response

```
201 Created
Content-Type: application/json

{
    "id": 99,
    "pickupPerson": "Bob Smith",
    "items": "1,2,3,4,5",
    "pickupTimeAssigned": "some date time in the future"
}

```


## GET /curbsideorders

Return a list of all the orders that have been placed


## GET /curbsideorders/{id}

Return a curbside order or a 404
